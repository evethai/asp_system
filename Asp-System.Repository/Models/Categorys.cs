using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asp_System.Repository.Models
{
    public class Categorys
    {
        //Id	Name	Status
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Status { get; set; }

    }
}
