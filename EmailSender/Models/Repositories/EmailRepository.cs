using EmailSender.Models.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmailSender.Models.Repositories
{
    public class EmailRepository
    {
        public List<Email> GetEmails(string userId)
        {
            using (var context = new ApplicationDbContext())
            {
                return context.Emails.Where(x => x.UserId == userId).ToList();
            }
        }

        public void AddEmail(Email email)
        {
            using (var context = new ApplicationDbContext())
            {
                context.Emails.Add(email);

                context.SaveChanges();
            }
        }

        public Email GetEmail(int id, string userId)
        {
            using (var context = new ApplicationDbContext())
            {
                return context.Emails.Single(x => x.Id == id && x.UserId == userId);
            }
        }
        public void Delete(int id, string userId)
        {
            using (var context = new ApplicationDbContext())
            {
                var emailToDelete = context.Emails.Single(x => x.Id == id && x.UserId == userId);

                context.Emails.Remove(emailToDelete);

                context.SaveChanges();
            }
        }

    }
}