using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FosterUniteForum.Areas.ForumAdmin.Models

{
    public class BoardModel
    {
        [Required (ErrorMessage = "Board name is required")]
        [Display(Name = "Name")]
        public string BoardName { get; set; }
        
        public string BoardDescription { get; set; }
    }
}