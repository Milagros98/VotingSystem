using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VotingSystem.Models
{
    public class GroupDetailsView
    {
        public int groupId { get; set; }
        public string description { get; set; }
        public List<GroupMembers> members { get; set; }
    }
}