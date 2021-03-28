﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace VotingSystem.Models
{
    public class User
    {   [Key]
        public int userId { get; set; }

        [Required(ErrorMessage = "The field {0} is required.")]
        [MinLength(5, ErrorMessage = "The field {0} must had minimun {1} characters")]
        [MaxLength(100, ErrorMessage = "The field {0} must had maximun {1} characters")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        [Index("UsernameIndex", IsUnique= true)]
        public string userName { get; set; }

        [Required(ErrorMessage = "The field {0} is required.")]
        [MinLength(5, ErrorMessage = "The field {0} must had minimun {1} characters")]
        [MaxLength(100, ErrorMessage = "The field {0} must had maximun {1} characters")]
        [Display(Name = "Firts Name")]
        public string firtsName { get; set; }

        [Required(ErrorMessage = "The field {0} is required.")]
        [MinLength(5, ErrorMessage = "The field {0} must had minimun {1} characters")]
        [MaxLength(100, ErrorMessage = "The field {0} must had maximun {1} characters")]
        [Display(Name = "Last Name")]
        public string lastName { get; set;  }

        public string fullName { get { return string.Format("{0} {1}", this.firtsName, this.lastName); } }

        [Required(ErrorMessage = "The field {0} is required.")]
        [MinLength(5, ErrorMessage = "The field {0} must had minimun {1} characters")]
        [MaxLength(100, ErrorMessage = "The field {0} must had maximun {1} characters")]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone")]
        public string phone { get; set; }

        [Required(ErrorMessage = "The field {0} is required.")]
        [MinLength(5, ErrorMessage = "The field {0} must had minimun {1} characters")]
        [MaxLength(100, ErrorMessage = "The field {0} must had maximun {1} characters")]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Address")]
        public string address { get; set; }

        [MinLength(1, ErrorMessage = "The field {0} must had minimun {1} characters")]
        [MaxLength(20, ErrorMessage = "The field {0} must had maximun {1} characters")]
        public string grade { get; set; }

        [MinLength(1, ErrorMessage = "The field {0} must had minimun {1} characters")]
        [MaxLength(20, ErrorMessage = "The field {0} must had maximun {1} characters")]
        public string group { get; set; }

        [DataType(DataType.ImageUrl)]
        public string photo { get; set; }

        public virtual ICollection<GroupMembers> GroupMembers { get; set; }
    }
}