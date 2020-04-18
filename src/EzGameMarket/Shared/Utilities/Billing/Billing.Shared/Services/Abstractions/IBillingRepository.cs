﻿using Shared.Utilities.Billing.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Utilities.Billing.Shared.Services.Abstractions
{
    public interface IBillingRepository
    {
        Task Bill(BillViewModel model);

        Task<IEnumerable<BillViewModel>> GetAll();
        Task<BillViewModel> GetByID(string id);
        Task<IEnumerable<BillViewModel>> GetByIDs(IEnumerable<string> id);

        Task Storno(string id);

        Task SendOutToEmail(string id);
    }
}