using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlueLight.Main
{
    public class PulseInsertService
    {
        public PulseInsertService(DashboardContext dashboardContext)
        {
            DashboardContext = dashboardContext;
        }

        public DashboardContext DashboardContext { get; }

        public async Task Reset()
        {
            var items= await this.DashboardContext.Pulses.ToListAsync();
            this.DashboardContext.Pulses.RemoveRange(items);
            await this.DashboardContext.SaveChangesAsync();
        }

        public async Task<List<Pulse>> InquiryAllItems()
        {
            List<Pulse> items = await this.DashboardContext.Pulses.ToListAsync();
            return items;
        }

        public async Task<Pulse> InsertOrUpdate(string moduleName)
        {
            Pulse pulse = await this.DashboardContext.Pulses
                .FirstOrDefaultAsync(x => x.ModuleName == moduleName);
            if (pulse == null)
            {
                pulse = new Pulse()
                {
                    ModuleName = moduleName,
                    CreatedDate = DateTime.Now
                };

                this.DashboardContext.Pulses.Add(pulse);
            }
            else
            {
                pulse.Counter++;
                pulse.CreatedDate = DateTime.Now;
            }

            await this.DashboardContext.SaveChangesAsync();
            return pulse;
        }
    }
}

//public async Task Send(int blockChainId,List<string> transactionLists, int confirmations)
//{
//    await WatcherService.SaveLogSendAsync(blockChainId, transactionLists, confirmations);
//    await WatcherService.CompareSend(transactionLists, confirmations);
//}

//public async Task Receive(int blockChainId,List<string> addresses)
//{
//    await WatcherService.SaveLogReceiveAsync(blockChainId, addresses);
//    await WatcherService.CompareReceive(addresses);
//}

//public async Task Test(string input1)
//{

//}