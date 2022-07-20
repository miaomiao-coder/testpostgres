using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Model.Base
{
    public class EntityBase
    {
        /// <summary>
        /// 编号
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int id { get; set; }


        

        public virtual void SetToDeletion()
        {
            //this.IsDeleted = true;
        }

    }
}
