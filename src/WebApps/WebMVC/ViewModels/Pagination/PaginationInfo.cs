﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMVC.ViewModels.Pagination
{
    public class PaginationInfo
    {
        public int ActualPage { get; set; }
        public long TotalItemsCount { get; set; }
        public int MaxPageNumber { get; set; }
        public int ItemsPerPage { get; set; }
        public string Previous { get; set; }
        public string Next { get; set; }
    }
}