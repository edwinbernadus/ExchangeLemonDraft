using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BlueLight.Main;
using System.Collections.Generic;
using System;

namespace BackEndClassLibrary
{
    public class PendingTransferListsRepo
    {
        public PendingTransferListsRepo(ApplicationContext applicationContext)
        {
            _context = applicationContext;
        }

        public ApplicationContext _context { get; }

        public async Task<List<PendingTransferList>> GetPendingListAsync()
        {
            List<PendingTransferList> output = await _context.PendingTransferLists
                .Include(x => x.UserProfileDetail)
                .ThenInclude(x => x.UserProfile)
                .Include(x => x.HoldTransaction)
                //.Where(x => x.PendingBulkTransfer == null && x.IsApprove)
                .Where(x => x.PendingBulkTransfer == null )
                .ToListAsync();
            return output;
        }

        public async Task<List<PendingTransferList>> GetHistory(long id)
        {
            List<PendingTransferList> output = await _context.PendingTransferLists
                .Include(x => x.UserProfileDetail)
                .ThenInclude(x => x.UserProfile)
                .Include(x => x.HoldTransaction)
                .Where(x => x.PendingBulkTransfer.Id == id)
                .ToListAsync();
            return output;
        }

        public async Task<List<PendingTransferList>> GetFailedSendListAsync()
        {
            List<PendingTransferList> output = await _context.PendingTransferLists
           .Include(x => x.UserProfileDetail)
           .ThenInclude(x => x.UserProfile)
           .Include(x => x.HoldTransaction)
           .Where(x => x.PendingBulkTransfer != null && x.StatusTransfer == "ongoing")
           .ToListAsync();
            return output;
            
        }
    }
}
