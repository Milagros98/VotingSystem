using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VotingSystem.Models
{
    public class VotingGroup
    {
        [Key]
        public int votingGroupId { get; set; }
        public int votingId { get; set; }
        public int groupId { get; set; }
        public virtual Voting voting { get; set; }
        public virtual Group group { get; set; }
    }
}