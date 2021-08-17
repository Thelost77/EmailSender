using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EmailSender.Models.ViewModels
{
    public class EmailViewModel
    {
        [Required]
        [Display(Name = "Odbiorcy")]
        public string Recipients { get; set; }
        [Display(Name = "Nazwa nadawcy")]
        public string SenderName { get; set; }
        [Required]
        [Display(Name = "Temat")]
        public string Subject { get; set; }
        [Display(Name = "Tekst wiadomości")]
        [Required]
        public string Text { get; set; }
    }
}