using CommonInterfaces.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommonInterfaces.Interfaces
{
    public interface IBet
    {
        bool CreateBet(Bet bet);

        List<Bet> GetListBetsByRoulette(Bet bet);

        bool UpdateWinnerBetsXNumber(Bet bet);

        bool UpdateWinnerBetsXColor(Bet bet);

        bool UpdateLosserBetsXNumber(Bet bet);

        bool UpdateLosserBetsXColor(Bet bet);
    }
}
