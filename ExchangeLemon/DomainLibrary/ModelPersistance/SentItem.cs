using System.ComponentModel.DataAnnotations.Schema;

namespace BlueLight.Main
{
    public class SentItem
    {
        public int Id { get; set; }
        public string Address { get; set; }

        [Column(TypeName = "decimal(38, 8)")]
        public decimal Amount { get; set; }
        public int From { get; set; }

        public virtual PendingTransferList PendingTransferList { get; set; }
    }

}