using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi_LandingPreferencias.Models
{
    public class CaptchaInfo
    {
        public CaptchaVpap.Captcha objCaptcha { get; set; }
        public DateTime fecha { get; set; }
    }
}
