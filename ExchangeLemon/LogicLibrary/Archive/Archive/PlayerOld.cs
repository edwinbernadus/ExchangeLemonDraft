//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;

//namespace ExchangeLemonNet.Models
//{
//    public class Player
//    { 
//        public string Id { get; set; } 
//        public string UserName { get; set; }
//        //public string Password { get; set; }
//        public string Name { get; set; }
//        public string SocialNumber { get; set; }
//        public string BirthCity { get; set; }
//        public string Gender { get; set; }
//        public DateTime BirthDate { get; set; }
//        public virtual MCountry Citizen { get; set; }
//        public string Address { get; set; }
//        public virtual MCity City {get;set;}
//        public virtual MRegion Region { get; set; }
//        public int PostalCode { get; set; }
//        public virtual ICollection<PlayerAccount> PlayerAccounts { get; set; } = new List<PlayerAccount>();

//        public Player()
//        { 
//        }
//        public Player(string _Id, string _UserName, MCurrency idrCurrency)
//        {
//            this.PlayerAccounts = new List<PlayerAccount>();
//            //Id = _Id;
//            UserName = _UserName;
//            BirthDate = System.DateTime.Now;

//            var cIDR = idrCurrency; 
//            Models.PlayerAccount idrAcc = new Models.PlayerAccount()
//            {
//                Balance = 0,
//                Currency = cIDR,
//                Parent = this
//            };
//            this.PlayerAccounts.Add(idrAcc);
//        }
//        public static Player GeneratePlayer (string _Id, string _UserName, MCurrency idrCurrency)
//        {
//            return new Player(_Id, _UserName, idrCurrency);
//        }
//    }
//}