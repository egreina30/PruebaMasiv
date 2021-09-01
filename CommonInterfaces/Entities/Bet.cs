using System;
using System.Collections.Generic;
using System.Text;

namespace CommonInterfaces.Entities
{
    public class Bet
    {
        public int id { get; set; }
        public DateTime creationDate { get; set; }
        public int state { get; set; }
        public string nameState { get; set; }
        public int idRoulette { get; set; }
        public string idUser { get; set; }
        public decimal betValue { get; set; }
        public int? betNumber { get; set; }
        public int? betColor { get; set; }
        public string nameBetColor { get; set; }
    }
}