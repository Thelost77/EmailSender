using EmailSender.Models.Classes;
using EmailSender.Models.Repositories;
using EmailSender.Models.ViewModels;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace EmailSender.Controllers
{
    public class HomeController : Controller
    {
        private Email _email;
        private EmailRepository _emailRepository;
        public ActionResult Index()
        {
            var vm = PrepareIndexVm();
            return View(vm);
        }

        private IndexViewModel PrepareIndexVm()
        {
            return new IndexViewModel
            {
                Emails = _emailRepository.GetEmails()
            };
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Email()
        {
            var vm = new EmailViewModel();

            return View(vm);
        }
        [HttpPost]
        public ActionResult Email(EmailViewModel vm)
        {
            _email = InitalizeEmail();
            _email.SenderName = vm.SenderName;

            string[] emails = vm.Recipients.Split(',', ' ');

            foreach (string emailAddress in emails)

            {
                if (string.IsNullOrWhiteSpace(emailAddress))
                    continue;

                _email.Send(vm.Subject, vm.Text, emailAddress);

            }

            return RedirectToAction("Index");
        }

        private Email InitalizeEmail()
        {
            return new Email(new EmailParams
            {
                HostSmtp = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                SenderName = String.Empty,
                SenderEmail = "slavic.financials@gmail.com",
                SenderEmailPassword = "tsjuyhqywtjsvreh"
            });
        }

        private string DecryptSenderEmailPassword()
        {
            throw new NotImplementedException();
        }
    }
}