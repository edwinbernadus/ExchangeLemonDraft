using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlueLight.Main
{
    public class PagingClass
    {
        public static void Copy<T1, T2>(PagingClass<T1> result, PagingClass<T2> result2)
        {
            result2.next_page_url = result.next_page_url;
            result2.prev_page_url = result.prev_page_url;
            result2.last_page = result.last_page;
            result2.current_page = result.current_page;
        }
    }

    public class PagingClass<T>
    {
        public int current_page { get; set; }
        public int last_page { get; set; }

        public string next_page_url { get; set; }
        public string prev_page_url { get; set; }

        public List<T> data { get; set; }

        public int GetLastPage(int total, int itemPerPage)
        {
            var lastPage = (total / itemPerPage) + 1;
            if (total % itemPerPage == 0)
            {
                lastPage--;
            }

            if (lastPage <= 0)
            {
                lastPage = 1;
            }
            return lastPage;
        }

        public async Task PopulateQuery(IQueryable<T> input, int current, string url, int itemPerPage = 3)
        {
            //var url = "";
            //var itemPerPage= 3;

            var indicator = current - 1;
            var skipNumber = itemPerPage * indicator;
            if (skipNumber < 0)
            {
                skipNumber = 0;
            }

            var input2 = input.Skip(skipNumber).Take(itemPerPage);


            List<T> output = new List<T>();
            if (input2 is IAsyncEnumerable<T>)
            {
                output = await input2.ToListAsync();
            }
            else
            {
                output = input2.ToList();
            }



            var total = input.Count();
            this.last_page = GetLastPage(total, itemPerPage);

            this.current_page = skipNumber / itemPerPage;

            indicator = current;
            var nextIndicator = indicator + 1;
            var nextUrl = url + "?counter=" + nextIndicator;

            var prevIndicator = indicator - 1;
            var prevUrl = url + "?counter=" + prevIndicator;

            this.next_page_url = nextUrl;
            this.prev_page_url = prevUrl;

            this.current_page = indicator;

            this.data = output;
        }

        //[Obsolete]

        //public void Populate (IEnumerable<T> input, int current, string url, int itemPerPage = 3) {
        //    //var url = "";
        //    //var itemPerPage= 3;

        //    var indicator = current - 1;
        //    var skipNumber = itemPerPage * indicator;
        //    if (skipNumber < 0) {
        //        skipNumber = 0;
        //    }

        //    var total = input.Count ();
        //    var output = input.Skip (skipNumber).Take (itemPerPage).ToList ();

        //    this.last_page = GetLastPage (total, itemPerPage);

        //    this.current_page = skipNumber / itemPerPage;

        //    indicator = current;
        //    var nextIndicator = indicator + 1;
        //    var nextUrl = url + "?counter=" + nextIndicator;

        //    var prevIndicator = indicator - 1;
        //    var prevUrl = url + "?counter=" + prevIndicator;

        //    this.next_page_url = nextUrl;
        //    this.prev_page_url = prevUrl;

        //    this.current_page = indicator;

        //    this.data = output;
        //}

    }
}

//    {
//  "current_page": 1,
//  "last_page": 3,
//  "next_page_url": "https://hootlex.github.io/vuejs-paginator/samples/animals2.json",
//  "prev_page_url": null,
//  "data": [
//    {
//      "id": 1,
//      "name": "Crocodile"
//    },
//    {
//      "id": 2,
//      "name": "Bobcat"
//    },
//    {
//      "id": 3,
//      "name": "Elephant"
//    },
//    {
//      "id": 4,
//      "name": "bandicoot"
//    },
//    {
//      "id": 5,
//      "name": "barracuda"
//    }
//  ]
//}