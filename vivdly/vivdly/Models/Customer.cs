using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace vivdly.Models
{
    public class Customer
    {
        public int ID { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        public bool IsSubscribedToNewsLetter { get; set; }

        // used for navigation 
        public MembershipType MembershipType { get; set; }

        // treats this property as foreign key
        [Display(Name = "Membership Type")]
        public byte MembershipTypeID { get; set; }

        [Display(Name ="Date of Birth")]
        [Min18YearsIfAMember]
        public DateTime? DateOfBirth { get; set; }

    }
}