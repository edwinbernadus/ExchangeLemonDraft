// ;
//using BlueLight.Main.ViewModel;
// using Microsoft.AspNetCore.Mvc;

using System;

namespace BlueLight.Main
{
    public class PendingTransferListHistory {
        public int Id { get; set; }
        public string Content { get; set; }
        public int ConfirmTransfer { get; set; }
        public bool IsError { get; set; }
        public DateTimeOffset CreatedDate { get; set; } = DateTime.Now;

        public virtual PendingTransferList PendingTransferList { get; set; }
        public bool IsManual { get; set; }
    }




}