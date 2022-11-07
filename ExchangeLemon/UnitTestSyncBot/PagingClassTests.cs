using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlueLight.Main;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xunit;
namespace BlueLight.Main.Tests {

    public class PagingClassTests {
        [Fact]
        public async  Task PopulateTest () {
            var p = new PagingClass<int> ();

            var inputs = Enumerable.Range(0, 10).AsQueryable();

            
            await  p.PopulateQuery (inputs, 3, "");
            //0 1 2
            //3 4 5
            //6 7 8
            //9 10

            var total = p.data.Sum (x => x);
            var total2 = 6 + 7 + 8;
            var count = p.data.Count;
            Assert.Equal (3, count);
            Assert.Equal (total2, total);
            Assert.Equal (3, p.current_page);
            Assert.Equal (4, p.last_page);
        }

        [Fact]
        public async Task PopulateTestTwo () {
            var p = new PagingClass<int> ();

            var inputs = Enumerable.Range (0, 10).AsQueryable ();;
            await p.PopulateQuery (inputs, 1, "");
            //0 1 2
            //3 4 5
            //6 7 8
            //9 10

            var total = p.data.Sum (x => x);
            var total2 = 0 + 1 + 2;
            var count = p.data.Count;
            Assert.Equal (3, count);
            Assert.Equal (total2, total);
            Assert.Equal (1, p.current_page);
            Assert.Equal (4, p.last_page);
        }

        [Fact]
        public async Task PopulateTestThree () {
            var p = new PagingClass<int> ();

            var inputs = Enumerable.Range (0, 10).AsQueryable ();;
            await p.PopulateQuery (inputs, 2, "");
            //0 1 2
            //3 4 5
            //6 7 8
            //9 10

            var total = p.data.Sum (x => x);
            var total2 = 3 + 4 + 5;
            var count = p.data.Count;
            Assert.Equal (3, count);
            Assert.Equal (total2, total);
            Assert.Equal (2, p.current_page);
            Assert.Equal (4, p.last_page);
        }

        [Fact]
        public async Task PopulateTestFour () {
            var p = new PagingClass<int> ();

            var inputs = Enumerable.Range (0, 11).AsQueryable ();;
            await  p.PopulateQuery (inputs, 4, "");
            //0 1 2
            //3 4 5
            //6 7 8
            //9 10

            var total = p.data.Sum (x => x);
            var total2 = 9 + 10;
            var count = p.data.Count;
            Assert.Equal (2, count);
            Assert.Equal (total2, total);
            Assert.Equal (4, p.current_page);
            Assert.Equal (4, p.last_page);
        }

        [Fact]
        public void GetLastPageTest () {

            var p = new PagingClass<string> ();
            var total = 3;
            int itemPerPage = 3;
            var result = p.GetLastPage (total, itemPerPage);
            Assert.Equal (1, result);
        }

        [Fact]
        public void GetLastPageTestTwo () {

            var p = new PagingClass<string> ();
            var total = 4;
            int itemPerPage = 3;
            var result = p.GetLastPage (total, itemPerPage);
            Assert.Equal (2, result);
        }

        [Fact]
        public void GetLastPageTestThree () {

            var p = new PagingClass<string> ();
            var total = 2;
            int itemPerPage = 3;
            var result = p.GetLastPage (total, itemPerPage);
            Assert.Equal (1, result);
        }

        [Fact]
        public void GetLastPageTestFour () {

            var p = new PagingClass<string> ();
            var total = 6;
            int itemPerPage = 3;
            var result = p.GetLastPage (total, itemPerPage);
            Assert.Equal (2, result);
        }

        [Fact]
        public void GetLastPageTestFive () {

            var p = new PagingClass<string> ();
            var total = 0;
            int itemPerPage = 3;
            var result = p.GetLastPage (total, itemPerPage);
            Assert.Equal (1, result);
        }
    }
}