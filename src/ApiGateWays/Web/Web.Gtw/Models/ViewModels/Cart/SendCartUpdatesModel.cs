﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Gtw.Models.ViewModels.Cart
{
    public class SendCartUpdatesModel<T>
    {
        public string UserID { get; set; }
        public T Model { get; set; }
    }
}