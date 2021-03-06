using Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class CarImage : IEntity
    {
        public int ID { get; set; }
        public int CarID { get; set; }
        public string ImagePath { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
    }
}
