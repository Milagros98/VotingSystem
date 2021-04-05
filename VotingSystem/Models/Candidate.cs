using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VotingSystem.Models
{
    public class Candidate
    {
        [Key]
        public int candidateId { get; set; }

        public int votingId { get; set; }

        public int userId { get; set; }

        public int quantityVotes { get; set; }

        public virtual Voting Voting { get; set; }
        public virtual User User { get; set; }
    }
}