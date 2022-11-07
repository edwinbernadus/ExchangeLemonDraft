using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlueLight.Main
{
    public class UserProfileDetail
    {
        public int Id { get; set; }

        public virtual ICollection<RemittanceIncomingTransaction> RemittanceIncomingTransactions { get; set; }
        public string CurrencyCode { get; set; }

        [Column(TypeName = "decimal(38, 8)")]
        public decimal Balance { get; set; }

        [Column(TypeName = "decimal(38, 8)")]
        public decimal HoldBalance { get; set; }

        [Column(TypeName = "decimal(38, 8)")]
        public decimal OutgoingRemittance { get; set; }


        [Column(TypeName = "decimal(38, 8)")]
        public decimal IncomingRemittance { get; set; }

        [ConcurrencyCheck]
        public long Version { get; set; }

        [ConcurrencyCheck]
        public long HoldVersion { get; set; }

        [Timestamp]
        public byte[] Timestamp { get; set; }

        [JsonIgnore]
        public virtual UserProfile UserProfile { get; set; }


        [JsonIgnore]
        public virtual ICollection<HoldTransaction> HoldTransactions { get; set; }

        [JsonIgnore]

        public virtual ICollection<AccountTransaction> AccountTransactions { get; set; }


        public string PrivateKey { get; set; }
        public string PublicAddress { get; set; }



        public virtual ICollection<RemittanceTransaction> RemittanceTransactions { get; set; }
        public virtual ICollection<AdjustmentTransaction> AdjustmentTransactions { get; set; }
        //public int LastPosition { get; set; }
        

    }

}
