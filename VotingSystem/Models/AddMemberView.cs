using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VotingSystem.Models
{
    public class AddMemberView
    {
        public int groupId { get; set; }
        [Required(ErrorMessage = "The field {0} is required.")]
        //[Display(Name = "User")]
        public int userId { get; set; }
    }
}