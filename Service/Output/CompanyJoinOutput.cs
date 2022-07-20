using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Output
{
    public class CompanyJoinOutput
    {
        /// <summary>
        /// 公司名称
        /// </summary>
        public string name { get; set; }
        public IEnumerable<ManagerJoin> managers { get; set; }
    }
    public class ManagerJoin
    { 
        /// <summary>
        /// 经理名称
        /// </summary>
        public string managername { get; set; }
    }
}
