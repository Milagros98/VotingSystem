using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VotingSystem.Models
{
    public class VotingView
    {
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
        [Display(Name = "Date start")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy:MM:dd}", ApplyFormatInEditMode = true)]
        public DateTime dateStart { get; set; }


        [Required(ErrorMessage = "The field {0} is required.")]
        [Display(Name = "Date end")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy:MM:dd}", ApplyFormatInEditMode = true)]
        public DateTime dateEnd { get; set; }

        [Required(ErrorMessage = "The field {0} is required.")]
        [Display(Name = "Time start")]
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:hh:mm tt}", ApplyFormatInEditMode = true)]
        public DateTime timeStart { get; set; }

        [Required(ErrorMessage = "The field {0} is required.")]
        [Display(Name = "Time end")]
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:hh:mm tt}", ApplyFormatInEditMode = true)]
        public DateTime timeEnd { get; set; }

        [Required(ErrorMessage = "The field {0} is required.")]
        [Display(Name = "Is For All Users?")]
        public bool isForAllUsers { get; set; }

        [Required(ErrorMessage = "The field {0} is required.")]
        [Display(Name = "Is Enabled Blank Votes?")]
        public bool isEnabledBlankVotes { get; set; }
    }
}