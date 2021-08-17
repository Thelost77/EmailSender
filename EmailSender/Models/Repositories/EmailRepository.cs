using EmailSender.Models.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmailSender.Models.Repositories
{
    public class EmailRepository
    {
        public List<Email> GetEmails()
        {
            using (var context = new ApplicationDbContext())
            {
                return context.Emails.ToList();
            }
        }

        public void AddEmail(Email email)
        {
            using (var context = new ApplicationDbContext())
            {
                context.Emails.Add(email);
            }
        }

        public Email GetEmail(int id)
        {
            using (var context = new ApplicationDbContext())
            {
                return context.Emails.Single(x => x.Id == id);
            }
        }

    }
}