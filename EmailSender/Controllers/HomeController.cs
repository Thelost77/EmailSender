using EmailSender.Models.Classes;
using EmailSender.Models.Repositories;
using EmailSender.Models.ViewModels;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
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
    [Authorize]
    public class HomeController : Controller
    {
        private Email _email;
        private EmailRepository _emailRepository = new EmailRepository();
        public ActionResult Index()
        {
            var vm = PrepareIndexVm();
            return View(vm.Emails);
        }

        private IndexViewModel PrepareIndexVm()
        {
            var userId = User.Identity.GetUserId();

            return new IndexViewModel
            {
                Emails = _emailRepository.GetEmails(userId)
            };
        }

        [AllowAnonymous]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [AllowAnonymous]
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
            var userId = User.Identity.GetUserId();

            string[] emails = vm.Recipients.Split(',', ' ');

            foreach (string emailAddress in emails)
            {
                if (string.IsNullOrWhiteSpace(emailAddress))
                    continue;

                _email.Send(vm.Subject, vm.Text, emailAddress);

                var email = new Models.Domains.Email()
                {
                    Title = vm.Subject,
                    Recipient = emailAddress,
                    Text = vm.Text,
                    SentDate = DateTime.Now,
                    UserId = userId
                };

                _emailRepository.AddEmail(email);

            }

            return RedirectToAction("Index");
        }

        public ActionResult ReadEmail(int id)
        {
            var userId = User.Identity.GetUserId();

            var email = _emailRepository.GetEmail(id, userId);

            return View(email);
        }


        private Email InitalizeEmail()
        {
            return new Email(new EmailParams
            {
                HostSmtp = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                SenderName = String.Empty,
                SenderEmail = "Enter Here Your Email!",
                SenderEmailPassword = "Enter Here Your Password!"
            });
        }

        [HttpPost]
        public ActionResult DeleteEmail(int id)
        {
            try
            {
                var userId = User.Identity.GetUserId();
                _emailRepository.Delete(id, userId);
            }
            catch (Exception e)
            {
                // logowanie do pliku
                return Json(new { Success = true, Message = e.Message });

            }
            return Json(new { Success = true });

        }

    }
}