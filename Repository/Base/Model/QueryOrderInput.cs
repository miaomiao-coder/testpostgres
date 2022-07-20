using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Base
{
    public class QueryOrderInput
    {
        /// <summary>
        /// 列名
        /// </summary> 
        public string FieldName { get; set; }

        /// <summary>
        /// 排序方式
        /// </summary> 
        public string Order { get; set; }
    }
}
