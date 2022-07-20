using Model.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Manager : EntityBase
    {
        public string name { get; set; }
        //public ClientUser Clients { get; set; }

        public int companyid { get; set; }

        public Company company { get; set; }
    }
}
