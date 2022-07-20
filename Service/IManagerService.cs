using Model;
using Service.Output;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public interface IManagerService
    {
        Task<IEnumerable<ManagerJoinOutput>> GetJoiIncludeAsync();
    }
}
