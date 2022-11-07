// ;
//using BlueLight.Main.ViewModel;
// using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlueLight.Main
{

    public class PendingTransferList
    {
        public int Id { get; set; }

        public DateTimeOffset LastCheckTransferDate { get; set; } = DateTime.Now;
        public virtual ICollection<PendingTransferListHistory> PendingTransferListHistories { get; set; }
        public int ConfirmTransfer { get; set; }
        [Column(TypeName = "decimal(38, 8)")]
        public decimal Amount { get; set; }
        public virtual UserProfileDetail UserProfileDetail { get; set; }
        public virtual AccountTransaction AccountTransaction { get; set; }

        public string AddressDestination { get; set; }

        public bool IsApprove { get; set; }
        //public bool IsExecute { get; set; }
        public DateTimeOffset CreatedDate { get; set; } = DateTime.Now;

        public virtual PendingBulkTransfer PendingBulkTransfer { get; set; }
        public virtual HoldTransaction HoldTransaction { get; set; }

        public string StatusTransfer { get; set; }
        public string TransactionId { get; set; }
    }




}