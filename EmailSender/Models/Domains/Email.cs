using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EmailSender.Models.Domains
{
    public class Email
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Tytuł wiadomości")]

        public string Title { get; set; }
        [Required]
        [Display(Name = "Adresat")]
        public string Recipient { get; set; }
        [Required]
        [Display(Name = "Tekst wiadomości")]
        public string Text { get; set; }
        [Display(Name = "Data wysłania")]
        [Required]        
        public DateTime SentDate { get; set; }
        [Required]
        public string UserId { get; set; }
    }
}