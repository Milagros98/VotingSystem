using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VotingSystem.Models
{
    public class AddCandidateView
    {
        public int votingId { get; set; }
        [Required(ErrorMessage = "The field {0} is required.")]
        [Display(Name = "Candidate")]
        public int userId { get; set; }
    }
}