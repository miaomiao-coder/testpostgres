using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Model;
using Repository;
using Service.Base;
using Service.Input;
using Service.Output;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class CompanyService: ServiceBase<Company>, ICompanyService
    {
        protected readonly IMapper _mapper;
        private readonly EDbContext _db;
        public CompanyService(IMapper mapper,EDbContext db, ICompanyRepo repository):base(mapper, repository)
        {
            _mapper = mapper;
            _db = db;
        }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="id"></param>
      /// <returns></returns>
        public async Task<CompanyOutput> GetAsync(int id)
        {
            //var compiledProductReports = EF.CompileQuery(
            //    (EDbContext ctx, decimal total)
            //        => ctx.Managers.AsNoTracking().IgnoreAutoIncludes()
            //    .GroupBy(a => a.companyid).Select(a => new
            //    {
            //        ProductId = a.Key,
            //        count =a.Count()
            //    }).Select(a => a.ProductId));
            //var productIds = compiledProductReports(_db, 100000).ToList();

            var QUERY = await _db.Companys.FindAsync(1);
            var result = _db.Companys.Include(x => x.managers).ToList();
            var ss = await _db.Companys.FindAsync(1);
            return await base.Get<CompanyOutput>(id);
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<CompanyOutput> InsertAsync(CompanyInput input)
        {
            return await base.Add<CompanyInput, CompanyOutput>(input);
        }
        public async Task<List<CompanyOutput>> GetJoinWhereAsync()
        {
            var query = _db.Companys.Where(x => x.managers.Select(x=>x.name).Contains("王经理"));
           var result= query.ToList();
           return _mapper.Map<List<Company>, List<CompanyOutput>>(result);
        }
        public async Task<IEnumerable<CompanyJoinOutput>> GetJoiIncludeAsync()
        {
            var query = _db.Companys.Include(x=>x.managers)
                .Select(x=>new CompanyJoinOutput() { 
                    name = x.name,
                    managers=x.managers.Select(n=>new ManagerJoin() { 
                        managername=n.name
                    })});
            var result= query.ToList();
            return result;
        }
    }
}
