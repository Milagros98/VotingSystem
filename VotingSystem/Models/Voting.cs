using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VotingSystem.Models
{
    public class Voting
    {
        [Key]
        public int votingId { get; set; }

        [Required(ErrorMessage = "The field {0} is required.")]
        [MinLength(5, ErrorMessage = "The field {0} must had minimun {1} characters")]
        [MaxLength(100, ErrorMessage = "The field {0} must had maximun {1} characters")]
        public string description { get; set; }

        [Required(ErrorMessage = "The field {0} is required.")]
        [Display(Name = "State")]
        public int stateId { get; set; }

        [MinLength(5, ErrorMessage = "The field {0} must had minimun {1} characters")]
        [MaxLength(100, ErrorMessage = "The field {0} must had maximun {1} characters")]
        [DataType(DataType.MultilineText)]
        public string remarks { get; set; }

        [Required(ErrorMessage = "The field {0} is required.")]
        [Display(Name = "Date time start")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy:MM:dd hh:mm tt}", ApplyFormatInEditMode = true)]
        public DateTime dateTimeStart { get; set; }

        [Required(ErrorMessage = "The field {0} is required.")]
        [Display(Name = "Date time end")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy:MM:dd hh:mm tt}", ApplyFormatInEditMode = true)]
        public DateTime dateTimeEnd { get; set; }

        [Required(ErrorMessage = "The field {0} is required.")]
        [Display(Name = "Is For All Users?")]
        public bool isForAllUsers { get; set; }

        [Required(ErrorMessage = "The field {0} is required.")]
        [Display(Name = "Is Enabled Blank Votes?")]
        public bool isEnabledBlankVotes { get; set; }

        [Display(Name = "Quantity Votes")]
        public int quantityVotes { get; set; }

        [Display(Name = "Quantity Blank Votes")]
        public int quantityBlankVotes { get; set; }

        [Display(Name = "Winner")]
        public int candidateWinId { get; set; }


        public virtual State State { get; set; }
        public virtual ICollection<VotingGroup> VotingGroups { get; set; }
        public virtual ICollection<Candidate> Candidates { get; set; }
    }
}