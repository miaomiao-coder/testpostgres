using Model;
using Repository.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Impl
{
    public class ManagerRepo : BaseRepository<Manager>, IManagerRepo
    {
        private EDbContext _dbContext;
        public ManagerRepo(EDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
