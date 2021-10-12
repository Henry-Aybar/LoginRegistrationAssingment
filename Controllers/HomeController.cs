using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using LoginRegistrationAssingment.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;

namespace LoginRegistrationAssingment.Controllers
{
    public class HomeController : Controller
    {
        private MyContext _context;

        public HomeController(MyContext context)
        {
            _context = context;
        }

        //======================
        //   Login Route
        //======================
        public IActionResult Index()
        {
            return View("register");
        }
        //======================
        //   POST Reg w/ Validations
        //======================
        [HttpPost("register")]
        public IActionResult RegisterUser(User newUser)
        {
            if(ModelState.IsValid)
            {
                //thus searches for User Email and returns bool becaue of "Any."
                if(_context.Users.Any(user => user.Email == newUser.Email))
                {
                    ModelState.AddModelError("Email", "Email already in use!");
                    return View("Register");
                }

                // Console.WriteLine(newUser.Password);
                PasswordHasher<User> Hasher = new PasswordHasher<User>(); 
                newUser.Password = Hasher.HashPassword(newUser, newUser.Password);
                // Console.WriteLine(newUser.Password);
                
                // Adds User to DB
                _context.Add(newUser);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }
            return View("Register");
        }
        //======================
        //   Login Route
        //======================

        [HttpGet("loginPage")]
        public IActionResult LoginPage()
        {
            return View();
        }

        //======================
        //   Login Route
        //======================
        [HttpPost("login")]
        public IActionResult Login(LoginUser checkMe)
        {
            if(ModelState.IsValid)
            {
                //find User with Email
                User userInDb = _context.Users.FirstOrDefault(use => use.Email == checkMe.LoginEmail);
                //if User doesn't exist
                    //send Validation Error
                if(userInDb == null)
                {
                    ModelState.AddModelError("LoginEmail", "Invalid Login");

                    return View("LoginPage");
                }
                //verify Password
                PasswordHasher<LoginUser> hasher = new PasswordHasher<LoginUser>();
                var result = hasher.VerifyHashedPassword(checkMe, userInDb.Password, checkMe.LoginPassword);
                //if wrong Password
                    //send Validation error
                if(result == 0)
                {
                    ModelState.AddModelError("LoginEmail", "Invalid Login");
                    return View("LoginPage");
                }
                //put user id in session
                HttpContext.Session.SetInt32("LoggedUserId", userInDb.UserId);

                return RedirectToAction("Success");
            }
            return View("LoginPage");
        }

        //=====================
        //  Success Route
        //=====================
        [HttpGet("success")]
        public IActionResult Success()
        {
            //pervents a jump into Success Page w/ Session
            int? loggedUserId = HttpContext.Session.GetInt32("LoggedUserId");
            // if(loggedUserId == null) return RedirectToAction("LoginPage");
            if(loggedUserId == null) return RedirectToAction("Index");

            ViewBag.User = _context.Users.FirstOrDefault(use => use.UserId == loggedUserId);

            return View();
        }

        //=====================
        //   Logout 
        //=====================
        [HttpGet("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

