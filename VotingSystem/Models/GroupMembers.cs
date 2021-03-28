using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VotingSystem.Models
{
    public class GroupMembers
    {
        [Key]
        public int groupMemberId { get; set; }
        public int groupId { get; set; }
        public int userId { get; set; }

        public virtual Group Group { get; set; }
        public virtual User User { get; set; }
    }
}