//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
////using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
////using Microsoft.EntityFrameworkCore;
////using ExchangeLemonCore.Models;

//namespace ExchangeLemonCore.Data {
//    public class StudentFive {
//        public int Id { get; set; }
//        public string StudentName { get; set; }
//        //public virtual AddressFive Address { get; set; }
//        public virtual ICollection<AddressFive> Address { get; set; }

//        public string GetAddress () {
//            var addr = this.Address?.FirstOrDefault ();
//            var output = addr?.AddressName ?? "NoAddr";
//            return output;
//        }
//    }

//}