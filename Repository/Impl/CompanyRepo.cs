using Microsoft.EntityFrameworkCore;
using Model;
using Repository.Base;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class CompanyRepo: BaseRepository<Company>, ICompanyRepo
    {
        private EDbContext _dbContext;
        public CompanyRepo(EDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
