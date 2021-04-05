using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VotingSystem.Models
{
    public class AddGroupView
    {
        public int votingId { get; set; }
        [Required(ErrorMessage = "The field {0} is required.")]
        [Display(Name = "Group")]
        public int groupId { get; set; }
    }
}