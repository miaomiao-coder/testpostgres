using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Model;
using Repository;
using Service.Base;
using Service.Output;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Impl
{
    public class ManagerService : ServiceBase<Manager>, IManagerService
    {

        protected readonly IMapper _mapper;
        private readonly EDbContext _db;
        public ManagerService(IMapper mapper, EDbContext db, IManagerRepo repository) : base(mapper, repository)
        {
            _mapper = mapper;
            _db = db;
        }

        public async Task<IEnumerable<ManagerJoinOutput>> GetJoiIncludeAsync()
        {
            var query = _db.Managers.Include(x => x.company).Select(
                    x => new ManagerJoinOutput (){ id=x.id, name = x.name,companyname= x.company.name });
            var result = query.ToList();
            return result;
        }
    }
}
