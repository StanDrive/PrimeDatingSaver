﻿using System.Collections.Generic;
using PrimeDating.DataAccess.Models;

namespace PrimeDating.DataAccess.Interfaces
{
    public interface IMenDataService
    {
        void AddOrUpdateMen(List<Men> men);
    }
}
