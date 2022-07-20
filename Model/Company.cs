using Model.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{
    public class Company: EntityBase
    {
        public string name { get; set; }

        //[InverseProperty(nameof(Manager.name))]
        public List<Manager> managers { get; set; }
    }
}