using Model;
using Service.Input;
using Service.Output;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public interface ICompanyService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<CompanyOutput> GetAsync(int id);

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<CompanyOutput> InsertAsync(CompanyInput input);

        Task<List<CompanyOutput>> GetJoinWhereAsync();

        Task<IEnumerable<CompanyJoinOutput>> GetJoiIncludeAsync();
    }
}
