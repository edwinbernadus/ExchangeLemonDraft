using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace BlueLight.Main
{
    public class OrderTransaction
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public int TransactionId { get; set; }
        public Transaction Transaction { get; set; }
        
    }
}


