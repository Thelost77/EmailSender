using EmailSender.Models.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmailSender.Models.ViewModels
{
    public class IndexViewModel
    {
        public List<Email> Emails { get; set; }
    }
}