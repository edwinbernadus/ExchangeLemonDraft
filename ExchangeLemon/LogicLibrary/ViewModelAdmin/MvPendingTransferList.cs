// ;
//using BlueLight.Main.ViewModel;
// using Microsoft.AspNetCore.Mvc;

using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlueLight.Main
{
    public class MvPendingTransferList
    {
        public MvPendingTransferList()
        {
        }

        public MvPendingTransferList(PendingTransferList x)
        {
            
                Amount = x.Amount;
                CreatedDate = x.CreatedDate;
                CurrencyCode = x.HoldTransaction?.CurrencyCode;
                Id = x.Id;
                UserName = x.UserProfileDetail?.UserProfile?.username;
                IsApprove = x.IsApprove;
                AddressDestination = x.AddressDestination;
            
        }
        public int Id { get; set; }
        public string UserName { get; set; }
        public string CurrencyCode { get; set; }

        [Column(TypeName = "decimal(38, 8)")]
        public decimal Amount { get; set; }
        public DateTimeOffset CreatedDate { get; set; }

        public bool IsApprove { get; set; }
        public string AddressDestination { get; }
    }




}