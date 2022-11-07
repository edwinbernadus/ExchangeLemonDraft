using System;
using System.Collections.Generic;
using BlueLight.Main;

namespace ExchangeLemonCore.Controllers.Admin
{
    public class PendingBulkTransferDummyFactory
    {

        public List<PendingBulkTransfer> Generate()
        {
            var d = new PendingTransferListsDummyFactory();
            var d1 = d.GenerateTwo();

            var output = new List<PendingBulkTransfer>();
            output.Add(new PendingBulkTransfer()
            {
                Id = 1,
                Status = "Ready to send",
                Collection = d1,
                CreatedDate = DateTime.Now
            });


            output.Add(new PendingBulkTransfer()
            {
                Id = 1,
                Status = "Sent",
                Collection = d1,
                CreatedDate = DateTime.Now
            });


            return output;

        }
    }
}
