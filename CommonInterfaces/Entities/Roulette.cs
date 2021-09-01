using System;
using System.Collections.Generic;
using System.Text;

namespace CommonInterfaces.Entities
{
    public class Roulette
    {
        public int id { get; set; }
        public DateTime creationDate { get; set; }
        public int state { get; set; }
        public DateTime? openingDate { get; set; }
        public string nameState { get; set; }
        public int? winningNumber { get; set; }
        public DateTime? closingDate { get; set; }
    }
}
