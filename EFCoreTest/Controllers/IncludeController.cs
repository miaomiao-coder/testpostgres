using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Model;
using Service;
using Service.Input;
using Service.Output;

namespace EFCoreTest.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class IncludeController : Controller
    {
        private readonly ICompanyService _service;
        private readonly IManagerService _managerService;
        protected readonly IMapper Mapper;
        public IncludeController(ICompanyService service, IManagerService managerService, IMapper mapper)
        {
            _service=service;
            _managerService=managerService;
            Mapper = mapper;
        }
        [HttpGet]
        public async Task<CompanyOutput> GetDetail(int id)
        {
            return await  _service.GetAsync(id);
        }
        [HttpPost]
        public async Task<CompanyOutput> Add(CompanyInput input)
        {
            var ss=Mapper.Map<CompanyInput,CompanyInputAim>(input);
            return await _service.InsertAsync(input);
        }

        [HttpGet]
        public async Task<List<CompanyOutput>> GetCompanyJoinWhere()
        {
            return await _service.GetJoinWhereAsync();
        }

        /// <summary>
        /// include关联查公司
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<CompanyJoinOutput>> GetCompanyJoiInclude()
        {
            return await _service.GetJoiIncludeAsync();
        }

        /// <summary>
        /// include关联查经理
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<ManagerJoinOutput>> GetManagerJoiInclude()
        {
            return await _managerService.GetJoiIncludeAsync();
        }
    }
}
