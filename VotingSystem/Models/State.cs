using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VotingSystem.Models
{
    public class State
    {
        [Key]
        public int stateId { get; set; }

        [Required(ErrorMessage = "The field {0} is required. Enter a description")]
        [MinLength(5, ErrorMessage = "The field {0} must had minimun {1} characters")]
        [MaxLength(100, ErrorMessage = "The field {0} must had maximun {1} characters")]
        public string description { get; set; }

        public virtual ICollection<Voting> Voting { get; set; }
    }
}