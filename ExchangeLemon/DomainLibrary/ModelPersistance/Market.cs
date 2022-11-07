using System.Collections.Generic;

namespace BlueLight.Main
{

    public class Market
    {
        //public int Id { get; set; }
        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
        //public double LastPrice { get; set; }


    }
}
