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
        public string description { get; set; }
    }
}