using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AVB.ApiBlog.Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
namespace AVB.ApiBlog.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class HelperController : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult> SendContactEmail([FromBody] Contact contact)
        {
            System.Threading.Thread.Sleep(3000);
            try
            {
                var mail = new MailMessage(); //yeni bir mail nesnesi Oluşturuldu.
                mail.IsBodyHtml = true; //mail içeriğinde html etiketleri kullanılsın mı?

                mail.To.Add("kjavelin@hotmail.com"); //Kime mail gönderilecek.

                //mail kimden geliyor, hangi ifade görünsün?
                mail.From = new MailAddress("aveyselbozoglu@gmail.com");
                //mail.To = new MailAddress("kjavelin@hotmail.com");
                mail.Subject = contact.Subject;//mailin konusu

                //mailin içeriği.. Bu alan isteğe göre genişletilip daraltılabilir.
                mail.Body = "Gönderen " + contact.Email +" <br>" + "İsmi " + contact.Name + "<br> " + "Konu "+
                    mail.Subject + "<br>" + "Mesaj " + contact.Message;
                mail.IsBodyHtml = true;
   
                var smp = new SmtpClient();

                //mailin gönderileceği adres ve şifresi
                smp.Credentials = new NetworkCredential("aveyselbozoglu@gmail.com", "85213589cz");
                smp.Port = 587;
                smp.Host = "smtp.gmail.com";//gmail üzerinden gönderiliyor.
                smp.EnableSsl = true;
                smp.Send(mail);//mail isimli mail gönderiliyor.
                return Ok(mail);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }

            

        }


    }
}
