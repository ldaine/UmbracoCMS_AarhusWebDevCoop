﻿using AarhusWebDevCoop.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using Umbraco.Web.Mvc;

namespace AarhusWebDevCoop.Controllers
{
    public class ContactFormSurfaceController : SurfaceController
    {
        // GET: ContactFormSurface
        public ActionResult Index()
        {
            return PartialView("ContactForm", new ContactForm());
        }

        [HttpPost]
        public ActionResult HandleFormSubmit(ContactForm model)
        {
            MailMessage message = new MailMessage();
            message.To.Add("liga.daine@gmail.com");
            message.Subject = model.Subject;
            message.From = new MailAddress(model.Email, model.Name); 
            message.Body = model.Message;

            using(SmtpClient smtp = new SmtpClient())
            {
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.UseDefaultCredentials = false;
                smtp.EnableSsl = true;
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.Credentials = new System.Net.NetworkCredential("liga.daine@gmail.com", "node1024");
                smtp.EnableSsl = true;
                // send mail
                smtp.Send(message);
            }

            return RedirectToCurrentUmbracoPage();
        }
    }
}