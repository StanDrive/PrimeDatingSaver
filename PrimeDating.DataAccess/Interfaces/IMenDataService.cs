using System.Collections.Generic;
using PrimeDating.Models.Database;

namespace PrimeDating.DataAccess.Interfaces
{
    public interface IMenDataService
    {
        void AddOrUpdateMen(List<Men> men);
    }
}
