using System.Collections.Generic;
using System.Linq;

namespace BlueLight.Main {

    public class PagingClassTwo<T> {

        public int queryRecordCount { get; set; }
        public int totalRecordCount { get; set; }

        public List<T> records { get; set; }

        public void Populate (IQueryable<T> input, int current, int offset, int itemPerPage = 3) {

            var indicator = current - 1;
            var total = input.Count ();
            var output = input.Skip (offset).Take (itemPerPage).ToList ();

            this.totalRecordCount = total;
            this.queryRecordCount = total;

            this.records = output;
        }

    }
}