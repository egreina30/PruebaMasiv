using CommonInterfaces.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommonInterfaces.Interfaces
{
    public interface IRoulette
    {
        int CreateRoulette();

        List<Roulette> GetRoulettes();

        bool OpeningRoulette(Roulette roulette);

        Roulette GetRouletteXId(Roulette roulette);

        bool SetClosingRoulette(Roulette roulette);
    }
}
