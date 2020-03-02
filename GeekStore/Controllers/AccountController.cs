using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

using System.Threading;
using System.Threading.Tasks;
using GeekStore.Data.EFContext;
using GeekStore.Data.Tables;
using GeekStore.Data.ViewModels;
using GeekStore.Models;
using GeekStore.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
namespace GeekStore.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<DbUser> _userManager;
        private readonly SignInManager<DbUser> _signInManager;
        private readonly DBContext _context;
       
        public AccountController( UserManager<DbUser> userManager, SignInManager<DbUser> signInManager,
        DBContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
            //_emailSender = emailSender;
        }
        [HttpGet]
        [Route("Account/ChangePassword/{id}")]
        public IActionResult ChangePassword(string id)
        {
            return View();
        }
        [HttpPost]
        [Route("Account/ChangePassword/{id}")]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model,string id)
        {
            var user = _context.Users.FirstOrDefault(x => x.Id == id);
            if(user == null)
            {
                ModelState.AddModelError("", "This user not registered");
                return View(model);
            }
            var res=_userManager.PasswordHasher.HashPassword(user,model.Password);
            user.PasswordHash = res;
            var result = await _userManager.UpdateAsync(user);
            return RedirectToAction("Login","Account");
        }
        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {

            var user = _context.Users.FirstOrDefault(x => x.Email == model.Email);
            if (user == null)
            {
                ModelState.AddModelError("", "Email not registered");
                return View(model);
            }
            EmailSender sender = new EmailSender();
            string url = "https://localhost:44349/Account/ChangePassword/" + user.Id;
            sender.SendEmail(model.Email, 
                $"<head><link rel='stylesheet' href='https://stackpath.bootstrapcdn.com/bootstrap/4.4.1/css/bootstrap.min.css' integrity='sha384-Vkoo8x4CGsO3+Hhxv8T/Q5PaXtkKtu6ug5TOeNV6gBiFeWPGFN9MuhOf23Q9Ifjh' crossorigin='anonymous'></head>" +
                $"<h1>dear {user.UserName},<br/>from geekstore <h1/>" +
                $"if you don`t want to change password, ignore this message, else press button" +
                //$"<script src='https://cdn.jsdelivr.net/npm/popper.js@1.16.0/dist/umd/popper.min.js' integrity='sha384-Q6E9RHvbIyZFJoft+2mJbHaEWldlvI9IOYy5n3zV9zzTtmI3UksdQRVvoxMfooAo' crossorigin='anonymous'></script>"+
                //$"<script src = 'https://stackpath.bootstrapcdn.com/bootstrap/4.4.1/js/bootstrap.min.js' integrity = 'sha384-wfSDF2E50Y2D1uUdj0O3uMBJnjuUD4Ih7YwaYd1iqfktj0Uod8GCExl3Og8ifwB6' crossorigin = 'anonymous' ></ script > "+
                $"<a href='{url}'><button  class='btn btn-primary'>press<button></a>"
                );
            return RedirectToAction("Index", "Home");
            //ModelState.AddModelError("", "Input email");
            //return View(model);

        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            var info = HttpContext.Session.GetString("SessionUserData");
            if (info != null)
            {
                var res = JsonConvert.DeserializeObject<UserInfo>(info);
                var user = await _userManager.FindByIdAsync(res.UserId);
                var userPr = _context.UserProfiles.FirstOrDefault(x => x.Id == res.UserId);
                List<Order> orders = new List<Order>();
                foreach (var el in _context.UserOrders.Where(x => x.UserId == res.UserId))
                {
                    orders.Add(_context.Orders.FirstOrDefault(x => x.Id==el.OrderId));
                }
                return View(new EditProfileViewModel()
                {
                    User = user,
                    UserProfile = userPr,
                    Orders=orders
                });

            }
            return RedirectToAction("Login", "Account");

        }
        [HttpGet]
        public IActionResult AddFilesForm()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddFilesForm(IFormFile uploadedFile)
        {
            FileService service = new FileService();
            await service.AddFile(uploadedFile);
            return RedirectToAction("Profile", "Account");

            
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Profile(EditProfileViewModel model)
        {
            var info = HttpContext.Session.GetString("SessionUserData");
            if (info != null)
            {
                var res = JsonConvert.DeserializeObject<UserInfo>(info);
                var user = await _userManager.FindByIdAsync(res.UserId);
                if (user.PhoneNumber != model.PhoneNumber)
                {
                    _context.Users.FirstOrDefault(x => x.Id == res.UserId).PhoneNumber = model.PhoneNumber;
                }
                if (user.Email != model.Email)
                {
                    _context.Users.FirstOrDefault(x => x.Id == res.UserId).Email = model.Email;
                }

                var userPr = _context.UserProfiles.FirstOrDefault(x => x.Id == res.UserId);
                userPr.PostDepartament = model.PostDepartment;
                userPr.FirstName = model.FirstName;
                userPr.LastName = model.LastName;
                userPr.Sity = model.Sity;
                _context.SaveChanges();
                return RedirectToAction("Profile", "Account");

            }
            return RedirectToAction("Login", "Account");

        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Login(string returnUrl)
        {
            var m = new LoginViewModel()
            {
                ReturnUrl = returnUrl,
                EnternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList()
            };
            return View(m);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {

            if (!ModelState.IsValid)
            {

                return View(model);
            }
            var user = _context.Users.FirstOrDefault(x => x.Email == model.Email);
            if (user == null)
            {

                ModelState.AddModelError("", "Not correct email");
                return View(model);
            }
            var res = _signInManager
                .PasswordSignInAsync(user, model.Password, false, false).Result;
            if (!res.Succeeded)
            {
                ModelState.AddModelError("", "Not correct password");

                return View(model);
            }

            await _signInManager.SignInAsync(user, isPersistent: false);


            await Authenticate(user);
            var userInfo = new UserInfo()
            {
                Email = user.Email,
                UserId = user.Id
            };
            HttpContext.Session.SetString("SessionUserData", JsonConvert.SerializeObject(userInfo));
            return RedirectToAction("Index", "Home");


        }
        [HttpPost]
        public IActionResult EnternalLogin(string provider, string returnUrl)
        {
            var redirect = Url.Action("ExternalLoginCallback", "Account",
                new { ReturnUrl = returnUrl });
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirect);
            return new ChallengeResult(provider, properties);
        }
        public async Task<IActionResult> ExternalLoginCallback(string returnUrl = null, string remoteError = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            LoginViewModel loginViewModel = new LoginViewModel
            {
                ReturnUrl = returnUrl,
                EnternalLogins =
               (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList()
            };

            if (remoteError != null)
            {
                ModelState
                    .AddModelError(string.Empty, $"Error from external provider: {remoteError}");

                return View("Login", loginViewModel);
            }

            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                ModelState
                    .AddModelError(string.Empty, "Error loading external login information.");

                return View("Login", loginViewModel);
            }

            var signInResult = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider,
                info.ProviderKey, isPersistent: false, bypassTwoFactor: true);
            var email = info.Principal.FindFirstValue(ClaimTypes.Email);
            var un = info.Principal.FindFirstValue(ClaimTypes.Name);

            DbUser user = null;
            if (email == null)
            {
                user = await _userManager.FindByNameAsync(un);
            }
            else
            {
                user = await _userManager.FindByEmailAsync(email);
            }
            UserProfile userProfile = new UserProfile()
            {
                FirstName = info.Principal.FindFirstValue(ClaimTypes.GivenName),
                LastName = info.Principal.FindFirstValue(ClaimTypes.Surname),
                PostDepartament = 0,
                Sity = info.Principal.FindFirstValue(ClaimTypes.StateOrProvince),
                RegisterDate = DateTime.Now
            };

            if (signInResult.Succeeded)
            {
                var userInfo = new UserInfo()
                {
                    Email = user.UserName,
                    UserId = user.Id
                };

                HttpContext.Session.SetString("SessionUserData", JsonConvert.SerializeObject(userInfo));
                return LocalRedirect(returnUrl);
            }

            else
            {
                // Get the email claim value


                // Create a new user without password if we do not have a user already

                if (user == null)
                {
                    if (info.LoginProvider == "Facebook")
                    {
                        user = new DbUser
                        {
                            UserProfile = userProfile,
                            UserName = info.Principal.FindFirstValue(ClaimTypes.GivenName),
                            Email = info.Principal.FindFirstValue(ClaimTypes.Email),
                            PhoneNumber = info.Principal.FindFirstValue(ClaimTypes.MobilePhone)
                        };
                    }
                    else
                    {

                        user = new DbUser
                        {
                            UserProfile = userProfile,
                            UserName = info.Principal.FindFirstValue(ClaimTypes.Name),
                            Email = info.Principal.FindFirstValue(ClaimTypes.Email),
                            PhoneNumber = info.Principal.FindFirstValue(ClaimTypes.MobilePhone)
                        };
                    }

                    await _userManager.CreateAsync(user);
                }

                var userInfo = new UserInfo()
                {
                    Email = user.UserName,
                    UserId = user.Id
                };
                HttpContext.Session.SetString("SessionUserData", JsonConvert.SerializeObject(userInfo));
                await _userManager.AddLoginAsync(user, info);
                await _signInManager.SignInAsync(user, isPersistent: false);

                return LocalRedirect(returnUrl);

            }
            // If we cannot find the user email we cannot continue
            ViewBag.ErrorTitle = $"Email claim not received from: {info.LoginProvider}";
            ViewBag.ErrorMessage = "Please contact support on geekstore.support@gmail.com";

            return View("Error");
        }
        //public async Task<IActionResult> IndexAsync(CancellationToken cancellationToken)
        //{
        //    System.Web.Mvc.Controller controller;
        //    var result = await new AuthorizationCodeMvcApp(controller, new AppFlowMetadata()).
        //        AuthorizeAsync(cancellationToken);



        //        return new RedirectResult(result.RedirectUri);

        //}
        private async Task Authenticate(DbUser user)
        {
            // создаем один claim
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.UserName),
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Id)
            };
            // создаем объект ClaimsIdentity
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            // установка аутентификационных куки
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        public IActionResult AccessDenied()
        {
            return RedirectToAction("Login", "Account");
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {

            await _signInManager.SignOutAsync();
            //await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            //ne pyskae mene gadina
            if (ModelState.IsValid)
            {
                UserProfile userProfile = new UserProfile()
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    PostDepartament = model.PostDepartament,
                    Sity = model.Sity,
                    RegisterDate = DateTime.Now
                };
                DbUser user = new DbUser()
                {
                    PhoneNumber = model.Phone,
                    Email = model.Email,
                    UserName = model.Email,
                    UserProfile = userProfile
                };
                var res = await _userManager.CreateAsync(user, model.Password);
                res = _userManager.AddToRoleAsync(user, "User").Result;
                if (res.Succeeded)
                {
                    await _signInManager.SignInAsync(user, false);
                    return RedirectToAction("AllDone", "Home");
                }
                else
                {
                    foreach (var item in res.Errors)
                    {
                        ModelState.AddModelError(string.Empty, item.Description);
                    }
                }
            }

            return View();

        }
    }
}