﻿using System;
using System.Collections.Generic;
using System.Text;

namespace VaultAccess.Shared.Settings
{
    public interface IVault
    {
        string Name { get; set; }
        string ClientId { get; set; }
        string ClientSecret { get; set; }
    }
}