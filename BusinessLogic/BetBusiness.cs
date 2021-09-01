using CommonInterfaces.Entities;
using CommonInterfaces.Enumerations;
using CommonInterfaces.Interfaces;
using Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic
{
    public class BetBusiness : IBet
    {
        #region variables
        BetData data = new BetData();
        UserBusiness userBusiness = new UserBusiness();
        #endregion

        #region Public Methods
        public bool CreateBet(Bet bet)
        {
            bool result = false;
            if(bet == null)
                throw new Exception("Datos de apuesta no validos");
            if(bet.idRoulette <= 0)
                throw new Exception("Id de ruleta no valida");
            if (bet.idUser == null)
                throw new Exception("Identificación de usuario no valida");
            if (bet.idUser.Length < Convert.ToInt16(Environment.GetEnvironmentVariable("MinIdentificationLength")) || bet.idUser.Length > Convert.ToInt16(Environment.GetEnvironmentVariable("MaxIdentificationLength")))
                throw new Exception("Longitud de identificación de usuario no permitida");
            if (bet.betValue <= 0 || bet.betValue > 10000)
                throw new Exception("Valor de apuesta no permitido");
            if (bet.betNumber == null && bet.betColor == null)
                throw new Exception("Se debe apostar a un numero o a un color");
            if (bet.betNumber != null && bet.betColor != null)
                throw new Exception("Solamente se puede apostar a un item (número o color)");
            if (bet.betNumber != null && (bet.betNumber < 0 || bet.betNumber > 36))
                throw new Exception("Número de apuesta no permitido");
            if(bet.betColor != null && bet.betColor != (int)ColorBetEnum.Red && bet.betColor != (int)ColorBetEnum.Black)
                throw new Exception("Color de apuesta no permitido");
            Roulette rouletteById = new RouletteBusiness().GetRouletteXId(new Roulette() { id = bet.idRoulette });
            if(rouletteById == null || rouletteById.creationDate == DateTime.MinValue)
                throw new Exception("Ruleta no existente");
            if(rouletteById.state != (int)StateRouletteEnum.Open)
                throw new Exception("Estado de ruleta no valido para hacer apuestas");
            User userById = new UserBusiness().GetUserXId(new User() { identification = bet.idUser });
            if(userById == null || userById.identification == null)
                throw new Exception("Usuario no existente");
            if (userById.balance < bet.betValue)
                throw new Exception("Saldo del usuario insuficiente para la apuesta");
            
            bool resultCreate = data.CreateBet(bet);
            if (resultCreate)
            {
                result = userBusiness.UpdateBalanceUser(new User()
                {
                    identification = bet.idUser,
                    balance = (userById.balance - bet.betValue)
                });
            }

            return result;
        }

        public List<Bet> GetListBetsByRoulette(Bet bet)
        {
            if (bet == null)
                throw new Exception("Datos de apuesta no validos");
            if (bet.idRoulette <= 0)
                throw new Exception("Id de ruleta no valida");

            List<Bet> listBets = data.GetListBetsByRoulette(bet);
            listBets.ForEach(r =>
            {
                r.nameState = r.state switch
                {
                    0 => StateBetEnum.Open.ToString(),
                    1 => StateBetEnum.Winner.ToString(),
                    2 => StateBetEnum.Losser.ToString(),
                    _ => "Error",
                };
                r.nameBetColor = r.betColor switch
                {
                    0 => ColorBetEnum.Black.ToString(),
                    1 => ColorBetEnum.Red.ToString(),
                    _ => "Error",
                };
            });

            return listBets;
        }

        public bool UpdateWinnerBetsXNumber(Bet bet)
        {
            if (bet == null)
                throw new Exception("Datos de apuesta no validos");
            if (bet.idRoulette <= 0)
                throw new Exception("Id de ruleta no valida");
            if (bet.betNumber == null)
                throw new Exception("Número de apuesta no valido");
            if (bet.betNumber < 0 || bet.betNumber > 36)
                throw new Exception("Número de apuesta no valido");

            return data.UpdateWinnerBetsXNumber(bet);
        }

        public bool UpdateWinnerBetsXColor(Bet bet)
        {
            if (bet == null)
                throw new Exception("Datos de apuesta no validos");
            if (bet.idRoulette <= 0)
                throw new Exception("Id de ruleta no valida");
            if (bet.betColor != (int)ColorBetEnum.Black && bet.betColor != (int)ColorBetEnum.Red)
                throw new Exception("Color de apuesta no valido");

            return data.UpdateWinnerBetsXColor(bet);
        }

        public bool UpdateLosserBetsXNumber(Bet bet)
        {
            if (bet == null)
                throw new Exception("Datos de apuesta no validos");
            if (bet.idRoulette <= 0)
                throw new Exception("Id de ruleta no valida");
            if (bet.betNumber == null)
                throw new Exception("Número de apuesta no valido");
            if (bet.betNumber < 0 || bet.betNumber > 36)
                throw new Exception("Número de apuesta no valido");

            return data.UpdateLosserBetsXNumber(bet);
        }

        public bool UpdateLosserBetsXColor(Bet bet)
        {
            if (bet == null)
                throw new Exception("Datos de apuesta no validos");
            if (bet.idRoulette <= 0)
                throw new Exception("Id de ruleta no valida");
            if (bet.betColor != (int)ColorBetEnum.Black && bet.betColor != (int)ColorBetEnum.Red)
                throw new Exception("Color de apuesta no valido");

            return data.UpdateLosserBetsXColor(bet);
        }
        #endregion
    }
}
