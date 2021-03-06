using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VotingSystem.Models
{
    public class Group
    {
        [Key]
        public int groupId { get; set; }

        [Required(ErrorMessage = "The field {0} is required. Enter a description")]
        [MinLength(5, ErrorMessage = "The field {0} must had minimun {1} characters")]
        [MaxLength(100, ErrorMessage = "The field {0} must had maximun {1} characters")]
        public string description { get; set; }

        [Display(Name = "Members")]
        public virtual ICollection<GroupMembers> GroupMembers { get; set; }

    }
}