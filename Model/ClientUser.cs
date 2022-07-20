using Model.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class ClientUser : EntityBase
    {
        public string name { get; set; }
        public string managerid { get; set; }
    }
}
