//using System;
//using System.Collections.Generic;
//using System.Diagnostics;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Mvc;
//using ExchangeLemonSyncBotCore.Models;
//using BackEndStandard;
//using ExchangeLemonCore.Data;
//using Microsoft.EntityFrameworkCore;

//namespace ExchangeLemonSyncBotCore.Controllers
//{

//    public class DebugController : Controller
//    {
//        public DebugController(ApplicationDbContext applicationDbContext)
//        {
//            _context = applicationDbContext;
//        }

//        public ApplicationDbContext _context { get; }

//        // https://localhost:44343/debug/testgroupby
//        public async Task<string> GetData()
//        {
//            var student = await _context.Students.FirstAsync();
//            var output = student.GetAddress();
//            return output;
//        }

//            public async Task<string> GetDataLazy()
//        {
//            var student = await _context.Students
//            .Include(x => x.Address)
//            .FirstAsync();
//            var output = student.GetAddress();
//            return output;
//        }

//        public async Task<StudentFive> GetDataTwo()
//        {
//            var student = await _context.Students.FirstAsync();
//            return student;
//        }

//        public async Task<StudentFive> GetDataTwoLazy()
//        {
//            var student = await _context.Students
//            .Include(x => x.Address)
//            .FirstAsync();
//            return student;
//        }

//        public async Task InsertEager()
//        {

//            var student = await _context.Students.FirstAsync();

//            var address = new AddressFive()
//            {
//                AddressName = "addr one"
//            };

//            student.Address = new List<AddressFive>();
//            student.Address.Add(address);

//            await _context.SaveChangesAsync();

//        }

//        public async Task InsertLazy()
//        {

//            var student = await _context.Students
//                .Include(x => x.Address)
//                .FirstAsync();

//            var address = new AddressFive()
//            {
//                AddressName = "lazy one"
//            };
//            student.Address.Add(address);

//            await _context.SaveChangesAsync();

//        }
//    }
//}