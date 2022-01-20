using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace UserManagement.Data
{
    public class Group  
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string GroupName { get; set; }


        public IEnumerable<User>? Members { get; set; }


    }
}
