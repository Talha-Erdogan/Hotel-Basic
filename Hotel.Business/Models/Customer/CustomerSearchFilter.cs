using System;
using System.Collections.Generic;
using System.Text;

namespace Hotel.Business.Models.Customer
{
    public class CustomerSearchFilter
    {
        // paging
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }

        // sorting
        public string SortOn { get; set; }
        public string SortDirection { get; set; }

        // filters        
        public string Filter_Name { get; set; }
        public string Filter_LastName { get; set; }
    }
}
