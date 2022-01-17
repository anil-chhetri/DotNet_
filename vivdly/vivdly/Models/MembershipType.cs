using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace vivdly.Models
{
    public class MembershipType
    {
        public byte Id { get; set; }

        public short SignUpFee { get; set; }

        [StringLength(255)]
        public string Name { get; set; }

        public byte DurationMonth { get; set; }

        public byte Discount { get; set; }


    }
}