using CommonInterfaces.Entities;
using CommonInterfaces.Enumerations;
using CommonInterfaces.Interfaces;
using Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic
{
    public class RouletteBusiness : IRoulette
    {
        #region Variables
        RouletteData data = new RouletteData();
        UserBusiness userBusiness = new UserBusiness();
        BetBusiness betBusiness = new BetBusiness();
        #endregion

        #region Public Methods
        public int CreateRoulette()
        {
            return data.CreateRoulette();
        }

        public List<Roulette> GetRoulettes()
        {
            List<Roulette> listRoulette = data.GetRoulettes();
            listRoulette.ForEach(r =>
            {
                r.nameState = r.state switch
                {
                    0 => StateRouletteEnum.Create.ToString(),
                    1 => StateRouletteEnum.Open.ToString(),
                    2 => StateRouletteEnum.Close.ToString(),
                    _ => "Error",
                };
            });

            return listRoulette;
        }

        public bool OpeningRoulette(Roulette roulette)
        {
            if (roulette == null)
                throw new Exception("Datos de ruleta no validos");
            if (roulette.id <= 0)
                throw new Exception("Id de ruleta no valido");
            Roulette existingRoulette = GetRouletteXId(roulette);
            if (existingRoulette == null || existingRoulette.creationDate == DateTime.MinValue)
                throw new Exception("Ruleta no existente");
            if (existingRoulette.state != (int)StateRouletteEnum.Create)
                throw new Exception("La ruleta debe estar en estado creada para poder abrirla");

            return data.OpeningRoulette(roulette);
        }

        public Roulette GetRouletteXId(Roulette roulette)
        {
            if (roulette == null)
                throw new Exception("Datos de ruleta no validos");
            if (roulette.id <= 0)
                throw new Exception("Id de ruleta no valido");

            return data.GetRouletteXId(roulette);
        }

        public List<Bet> ClosingRoulette(Roulette roulette)
        {
            if (roulette == null)
                throw new Exception("Datos de ruleta no validos");
            if (roulette.id <= 0)
                throw new Exception("Id de ruleta no valido");
            Roulette existingRoulette = GetRouletteXId(roulette);
            if (existingRoulette == null || existingRoulette.creationDate == DateTime.MinValue)
                throw new Exception("Ruleta no existente");
            if (existingRoulette.state != (int)StateRouletteEnum.Open)
                throw new Exception("La ruleta debe estar en estado abierta para poder cerrarla");

            List<Bet> result = new List<Bet>();
            roulette.winningNumber = GetWinningNumber();
            int winningColor = IsEvenNumber(roulette.winningNumber) ? (int)ColorBetEnum.Red : (int)ColorBetEnum.Black;
            if (SetClosingRoulette(roulette))
            {
                if(UpdateWinnerAndLosserBets(new Bet() { idRoulette = roulette.id, betNumber = roulette.winningNumber , betColor = winningColor }))
                {
                    List<Bet> bets = betBusiness.GetListBetsByRoulette(new Bet() { idRoulette = roulette.id });
                    UpdateBalanceWinningBets(bets, roulette.winningNumber, winningColor);
                }    
            }
            result = betBusiness.GetListBetsByRoulette(new Bet() { idRoulette = roulette.id });

            return result;
        }

        public bool SetClosingRoulette(Roulette roulette)
        {
            return data.SetClosingRoulette(roulette);
        }
        #endregion

        #region Private Methods
        private int GetWinningNumber()
        {
            var seed = Environment.TickCount;
            var random = new Random(seed);
            return random.Next(0, 36);
        }

        private bool IsEvenNumber(int? number)
        {
            bool result = false;
            if (number % 2 == 0)
                result = true;

            return result;
        }

        private bool UpdateWinnerAndLosserBets(Bet bet)
        {
            bool result = false;
            try
            {
                result = betBusiness.UpdateWinnerBetsXNumber(bet) && betBusiness.UpdateWinnerBetsXColor(bet) 
                    && betBusiness.UpdateLosserBetsXNumber(bet) && betBusiness.UpdateLosserBetsXColor(bet);
            }
            catch(Exception ex)
            {

            }
            
            return result;
        }

        private void UpdateBalanceWinningBets(List<Bet> bets, int? winningNumber, int winningColor)
        {
            try
            {
                bets.ForEach(b =>
                {
                    if (b.state == (int)StateBetEnum.Winner)
                    {
                        decimal addBalance = 0;
                        if (b.betNumber.Equals(winningNumber) && b.betColor.Equals(null))
                            addBalance = 5 * b.betValue;
                        if (b.betNumber.Equals(null) && b.betColor.Equals(winningColor))
                            addBalance = (decimal)1.8 * b.betValue;
                        User userBet = userBusiness.GetUserXId(new User() { identification = b.idUser });
                        if (userBet != null)
                        {
                            decimal newBalance = addBalance + userBet.balance;
                            userBusiness.UpdateBalanceUser(new User() { identification = b.idUser, balance = newBalance });
                        }
                    }
                });
            }
            catch(Exception ex)
            {

            }
        }
        #endregion
    }
}
