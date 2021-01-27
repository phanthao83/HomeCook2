/* Author : Thi Xuan Thao, Phan
 *Linkedin  : https://www.linkedin.com/in/phan-thao-bb782bb5/
 */
using Microsoft.AspNetCore.Identity.UI.Services;

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HC.Ultility
{
    public class EmailSender : IEmailSender

    { 

        
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            throw new NotImplementedException();
        }
    }
}
