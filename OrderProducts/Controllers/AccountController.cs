using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using OrderProducts.DAL.OrderProducts.BLL.DTO;
using OrderProducts.DAL.OrderProducts.BLL.Infrastructure;
using OrderProducts.DAL.OrderProducts.BLL.Interfaces;
using OrderProducts.Models;
using OrderProducts.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace OrderProducts.Controllers
{
    // GET: Account
    public class AccountController : Controller
    {
        private IUserService UserService
        {
            get { return HttpContext.GetOwinContext().GetUserManager<IUserService>(); }
        }

        private IAuthenticationManager AuthenticationManager
        {
            get { return HttpContext.GetOwinContext().Authentication; }
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginModel model)
        {
            await SetInitialDataAsync();
            if (ModelState.IsValid)
            {
                UserDTO userDto = new UserDTO { Email = model.UserLogin, Password = model.Password };
                ClaimsIdentity claim = await UserService.Authenticate(userDto);

                if (claim == null)
                {
                    ModelState.AddModelError("", "Неверный логин или пароль.");
                }
                else
                {
                    AuthenticationManager.SignOut();
                    AuthenticationManager.SignIn(new AuthenticationProperties
                    {
                        IsPersistent = true
                    }, claim);
                    return RedirectToAction("Index", "Home");
                }
            }
            return View(model);
        }

        public ActionResult LogOff()
        {
            Session["order"] = null;

            AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterModel model)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            await SetInitialDataAsync();
            if (ModelState.IsValid)
            {
                UserDTO userDto = new UserDTO
                {
                    LastName = model.LastName,
                    FirstName = model.FirstName,
                    UserLogin = model.UserLogin,
                    Phone = model.Phone,
                    Email = model.Email,
                    Password = model.Password,
                    ConfirmPassword = model.ConfirmPassword,
                    Role = "user",
                };
                OperationDetails operationDetails = await UserService.Create(userDto);
                if (operationDetails.Succedeed)
                    return View("Login");
                else
                    ModelState.AddModelError(operationDetails.Property, operationDetails.Message);
            }
            return View(model);
        }
        private async Task SetInitialDataAsync()
        {
            await UserService.SetInitialData(new UserDTO
            {
                FirstName = "FirstName",
                LastName = "LastName",
                UserLogin = "Admin",
                Phone = "095-123-45-67",
                Email = "admin@gmail.com",
                Password = "7777777",
                ConfirmPassword = "7777777",
                Role = "admin",
            }, new List<string> { "user", "admin" });

            await UserService.SetInitialData(new UserDTO
            {
                FirstName = "FirstName",
                LastName = "LastName",
                UserLogin = "Employee",
                Phone = "095-111-45-67",
                Email = "employee@gmail.com",
                Password = "111111",
                ConfirmPassword = "111111",
                Role = "employee",
            }, new List<string> { "employee" });
        }
    }
}
