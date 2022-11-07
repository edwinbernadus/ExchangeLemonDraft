using System;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace BlueLight.Main
{
    public class RemittanceIncomingTransaction
    {
        public int Id { get; set; }
        [JsonIgnore]
        public virtual UserProfileDetail UserProfileDetail { get; set; }
        public DateTimeOffset CreatedDate { get; set; } = DateTime.Now;
        public string TransactionId { get; set; }

        [Column(TypeName = "decimal(38, 8)")]
        public decimal Amount { get; set; }

    }

}
