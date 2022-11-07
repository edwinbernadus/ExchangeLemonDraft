//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;

//namespace BlueLight.Main
//{

//    public class PlayerVerOne
//    { 
//        public string Id { get; set; } 
//        public string UserName { get; set; }
//        //public string Password { get; set; }
//        public string Name { get; set; }
//        public string SocialNumber { get; set; }
//        public string BirthCity { get; set; }
//        public string Gender { get; set; }
//        public DateTime BirthDate { get; set; }
//        public virtual Country Citizen { get; set; }
//        public string Address { get; set; }
//        public virtual City City {get;set;}
//        public virtual Region Region { get; set; }
//        public int PostalCode { get; set; }
//        public virtual ICollection<PlayerAccount> PlayerAccounts { get; set; }

//        public PlayerVerOne(string _Id, string _UserName, DBContext db)
//        {
//            this.PlayerAccounts = new List<PlayerAccount>();
//            Id = _Id;
//            UserName = _UserName;
//            BirthDate = System.DateTime.Now;

//            //var cIDR = db.Currencies.Where(x => x.Code == "IDR").First();
//            //Models.PlayerAccount idrAcc = new Models.PlayerAccount()
//            //{
//            //    Balance = 0,
//            //    Currency = cIDR,
//            //    Parent = this
//            //};
//            //this.PlayerAccounts.Add(idrAcc);
//        } 
//    }
//}