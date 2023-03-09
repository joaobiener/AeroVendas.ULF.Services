﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.RequestFeatures;

    public abstract class RequestParameters
    {
        const int maxPageSize = 30000;
        public int PageNumber { get; set; } = 1;
        private int _pageSize = 50;
        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize = (value > maxPageSize) ? maxPageSize : value;
            }

        }

        public string? OrderBy { get; set; }

    }
