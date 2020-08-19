using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Hotel.Business.Common;
using Hotel.Business.İnterfaces;
using Hotel.Web.Models.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hotel.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly IPersonnelService _personnelService;
        private readonly IProfileDetailService _profileDetailService;
        private readonly IProfilePersonnelService _profilePersonnelService;
        public UserController(IPersonnelService personnelService, IProfileDetailService profileDetailService, IProfilePersonnelService profilePersonnelService)
        {
            _personnelService = personnelService;
            _profileDetailService = profileDetailService;
            _profilePersonnelService = profilePersonnelService;
        }

        [AllowAnonymous]
        public ActionResult Login()
        {
            LoginViewModel model = new LoginViewModel();
            GetCaptchaImage();
            return View(model);
        }


        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            bool isValidCaptcha = CaptchaHelper.ValidateCaptchaCode(model.CaptchaCode, HttpContext);

            if (!isValidCaptcha)
            {
                ViewBag.Error = "Captcha Code Is Not Valid";
                return View(model);
            }

            SessionLoginResult result = SessionHelper.Login(model.UserName, model.Password, _personnelService, _profileDetailService, _profilePersonnelService);
            if (result.IsSuccess)
            {
                if (ModelState.IsValid)
                {
                    return RedirectToAction("Index", "Home");
                }
                return View(model);
            }
            else
            {
                ViewBag.Error = result.Message;
                return View(model);
            }
        }

        // oturum açmadan erişilecek
        [AllowAnonymous]
        public ActionResult Logout()
        {
            // session logout olmalı
            string _UserSessionTrace_SessionTraceGuid = "UserSessionTrace_SessionTraceGuid elde edilemedi";
            if (SessionHelper.CurrentUser != null)
            {

            }
            SessionHelper.Logout();
            return RedirectToAction("Login");
        }

        [AllowAnonymous]
        public ActionResult NotAuthorized()
        {
            return View();
        }

        [Route("get-captcha-image")]
        public IActionResult GetCaptchaImage()
        {
            int width = 120;
            int height = 36;
            var captchaCode = CaptchaHelper.GenerateCaptchaCode();
            var result = CaptchaHelper.GenerateCaptchaImage(width, height, captchaCode);
            HttpContext.Session.SetString("CaptchaCode", result.CaptchaCode);
            Stream s = new MemoryStream(result.CaptchaByteData);
            return new FileStreamResult(s, "image/png");
        }
    }
}
