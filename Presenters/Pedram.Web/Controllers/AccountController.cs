using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Pedram.Core.Domain.Users;
using Pedram.Framework.Controller;
using Pedram.Framework.CustomizedAttributes;
using Pedram.Framework.Helpers;
using Pedram.Services.Services.Users.Interfaces;
using Pedram.Web.Models.Management;
using Pedram.Web.Models.Users.Login;
using Pedram.Web.Models.Users.Register;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Pedram.Web.Controllers
{
    [Authorize]
    [PedramDescription( "Pedram.Controller.Account" )]
    public class AccountController : BaseController
        {
        private readonly IAuthenticationManager _authenticationManager;
        private readonly IApplicationSignInManager _signInManager;
        private readonly IApplicationUserManager _userManager;
        private readonly ILanguageHelper _ILanguageHelper;
        public AccountController( IApplicationUserManager userManager,
                                 IApplicationSignInManager signInManager,
                                 IAuthenticationManager authenticationManager,
                                 ILanguageHelper ILanguageHelper )
            {
            _userManager = userManager;
            _signInManager = signInManager;
            _authenticationManager = authenticationManager;
            _ILanguageHelper = ILanguageHelper;
            }

       
        #region External Login
            //
            // POST: /Account/ExternalLogin
            [HttpPost]
            [AllowAnonymous]
            [ValidateAntiForgeryToken]
            public ActionResult ExternalLogin( string provider, string returnUrl )
                {
                // Request a redirect to the external login provider
                return new ChallengeResult( provider, Url.Action( "ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl } ) );
                }

            //
            // GET: /Account/ExternalLoginCallback
            [AllowAnonymous]
            public async Task<ActionResult> ExternalLoginCallback( string returnUrl )
                {
                var loginInfo = await _authenticationManager.GetExternalLoginInfoAsync();
                if (loginInfo == null)
                    {
                    return RedirectToAction( "Login" );
                    }

                // Sign in the user with this external login provider if the user already has a login
                var result = await _signInManager.ExternalSignInAsync( loginInfo, isPersistent: false );
                switch (result)
                    {
                    case SignInStatus.Success:
                        return redirectToLocal( returnUrl );
                    case SignInStatus.LockedOut:
                        return View( "Lockout" );
                    case SignInStatus.RequiresVerification:
                        return RedirectToAction( "SendCode", new { ReturnUrl = returnUrl } );
                    default:
                        // If the user does not have an account, then prompt the user to create an account
                        ViewBag.ReturnUrl = returnUrl;
                        ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
                        return View( "ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { Email = loginInfo.Email } );
                    }
                }

            //
            // POST: /Account/ExternalLoginConfirmation
            [HttpPost]
            [AllowAnonymous]
            [ValidateAntiForgeryToken]
            public async Task<ActionResult> ExternalLoginConfirmation( ExternalLoginConfirmationViewModel model, string returnUrl )
                {
                if (User.Identity.IsAuthenticated)
                    {
                    return RedirectToAction( "Index", "Manage" );
                    }

                if (ModelState.IsValid)
                    {
                    // Get the information about the user from the external login provider
                    var info = await _authenticationManager.GetExternalLoginInfoAsync();
                    if (info == null)
                        {
                        return View( "ExternalLoginFailure" );
                        }
                    var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                    var result = await _userManager.CreateAsync( user );
                    if (result.Succeeded)
                        {
                        result = await _userManager.AddLoginAsync( user.Id, info.Login );
                        if (result.Succeeded)
                            {
                            await _signInManager.SignInAsync( user, isPersistent: false, rememberBrowser: false );
                            return redirectToLocal( returnUrl );
                            }
                        }
                    addErrors( result );
                    }

                ViewBag.ReturnUrl = returnUrl;
                return View( model );
                }

            //
            // GET: /Account/ExternalLoginFailure
            [AllowAnonymous]
            public ActionResult ExternalLoginFailure()
            {
            return View();
            }
        #endregion
        #region Forget Password
        //
        // GET: /Account/ForgotPassword
            [PedramDescription( "Pedram.Controller.Account.ForgetPassword" )]
        [PedramAuthorize()]
        public ActionResult ForgotPassword()
                {
                return View();
                }

            //
            // POST: /Account/ForgotPassword
            [HttpPost]
        [PedramAuthorize()]
        [ValidateAntiForgeryToken]
            public async Task<ActionResult> ForgotPassword( ForgotPasswordViewModel model )
                {
                if (ModelState.IsValid)
                    {
                    var user = await _userManager.FindByNameAsync( model.Email );
                    if (user == null || !(await _userManager.IsEmailConfirmedAsync( user.Id )))
                        {
                        // Don't reveal that the user does not exist or is not confirmed
                        return View( "ForgotPasswordConfirmation" );
                        }

                    var code = await _userManager.GeneratePasswordResetTokenAsync( user.Id );
                    var callbackUrl = Url.Action( "ResetPassword", "Account",
                        new { userId = user.Id, code }, protocol: Request.Url.Scheme );
                    await _userManager.SendEmailAsync( user.Id, "Reset Password", "Please reset your password by clicking here: <a href=\"" + callbackUrl + "\">link</a>" );
                    ViewBag.Link = callbackUrl;
                    return View( "ForgotPasswordConfirmation" );
                    }

                // If we got this far, something failed, redisplay form
                return View( model );
                }

            //
            // GET: /Account/ForgotPasswordConfirmation
            [AllowAnonymous]
            public ActionResult ForgotPasswordConfirmation()
            {
            return View();
            }
        #endregion
        #region Login
        //
        // GET: /Account/Login
        [PedramDescription( "Pedram.Controller.Account.Login" )]
        [AllowAnonymous]
            public ActionResult Login( string returnUrl )
                {
                Configurations.UseEmailInsteadUserName = true;
                ViewBag.ReturnUrl = returnUrl;
                return View();
                }

            //
            // POST: /Account/Login
            [HttpPost]
            [AllowAnonymous]
            [ValidateAntiForgeryToken]
            public async Task<ActionResult> Login( LoginViewModel model, string returnUrl )
            {
            if (!ModelState.IsValid)
                {
                return View( model );
                }

            // This doesn't count login failures towards lockout only two factor authentication
            // To enable password failures to trigger lockout, change to shouldLockout: true
            var result = await _signInManager.PasswordSignInAsync( model.Email, model.Password, model.RememberMe, shouldLockout: false );

            /*var userName = User.Identity.GetUserName();
            if (string.IsNullOrWhiteSpace(userName))
            {
            }*/

            switch (result)
                {
                case SignInStatus.Success:
                    return redirectToLocal( returnUrl );
                case SignInStatus.LockedOut:
                    return View( "Lockout" );
                case SignInStatus.RequiresVerification:
                    return RedirectToAction( "SendCode", new { ReturnUrl = returnUrl } );
                default:
                    ModelState.AddModelError( "", "Invalid login attempt." );
                    return View( model );
                }
            }
        #endregion
        #region LogOut
        //
        // POST: /Account/LogOff
        //[HttpPost]
        // [ValidateAntiForgeryToken]
        [PedramDescription( "Pedram.Controller.Account.LogOut" )]
        public async Task<ActionResult> Logout()
            {
            var user = await _userManager.FindByNameAsync( User.Identity.Name );
            _authenticationManager.SignOut( DefaultAuthenticationTypes.ApplicationCookie );
            await _userManager.UpdateSecurityStampAsync( user.Id );

            return RedirectToAction( "Index", "Home" );
            }
        #endregion
        #region Register
        //
        // GET: /Account/Register
            [PedramDescription( "Pedram.Controller.Account.Register" )]
            [PedramAuthorize()]
            public ActionResult Register()
                {
                Configurations.UseEmailInsteadUserName = true;
                return View(new RegisterViewModel());
                }

            //
            // POST: /Account/Register
            [HttpPost]
            [PedramAuthorize()]
            [ValidateAntiForgeryToken]
            public async Task<ActionResult> Register( RegisterViewModel model )
            {
            if (Configurations.UseEmailInsteadUserName) {
                ModelState.Remove("UserName");
            }
            if (ModelState.IsValid)
                {
                var user = new ApplicationUser { Email = model.Email, BirthDay = model.BirthDay, WorkDescription = model.WorkDescription };

                if (Configurations.UseEmailInsteadUserName)
                    user.UserName = model.Email;
                else
                    user.UserName = model.UserName;

                var result = await _userManager.CreateAsync( user, model.Password );
                if (result.Succeeded && Configurations.SendConfirmEmail)
                {
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    var callbackUrl = Url.Action("ConfirmEmail", "Account",
                        new { userId = user.Id, code }, protocol: Request.Url.Scheme);
                    await _userManager.SendEmailAsync(user.Id, _ILanguageHelper.GetResource("Pedram.Register.Confirmyouraccount"), _ILanguageHelper.GetResource("Pedram.Register.ConfirmyouraccountText") + "\n <a href=\"" + callbackUrl + "\">link</a>");
                    ViewBag.Link = callbackUrl;
                    return View("DisplayEmail");
                }
                else if (result.Succeeded) {

                    return View("Index","Home");
                }
                addErrors( result );
                }

            // If we got this far, something failed, redisplay form
            return View( model );
            }
            //
            // GET: /Account/ConfirmEmail
             [PedramAuthorize()]
            public async Task<ActionResult> ConfirmEmail( int? userId, string code )
                {
                if (userId == null || code == null)
                    {
                    return View( "Error" );
                    }
                var result = await _userManager.ConfirmEmailAsync( userId.Value, code );
                return View( result.Succeeded ? "ConfirmEmail" : "Error" );
                }

        #endregion
        #region Reset Password
        //
        // GET: /Account/ResetPassword
        [PedramDescription( "Pedram.Controller.Account.ResetPassword" )]
        [PedramAuthorize()]
        public ActionResult ResetPassword( string code )
                {
                return code == null ? View( "Error" ) : View();
                }

            //
            // POST: /Account/ResetPassword
            [HttpPost]
        [PedramAuthorize()]
        [ValidateAntiForgeryToken]
            public async Task<ActionResult> ResetPassword( ResetPasswordViewModel model )
                {
                if (!ModelState.IsValid)
                    {
                    return View( model );
                    }
                var user = await _userManager.FindByNameAsync( model.Email );
                if (user == null)
                    {
                    // Don't reveal that the user does not exist
                    return RedirectToAction( "ResetPasswordConfirmation", "Account" );
                    }
                var result = await _userManager.ResetPasswordAsync( user.Id, model.Code, model.Password );
                if (result.Succeeded)
                    {
                    return RedirectToAction( "ResetPasswordConfirmation", "Account" );
                    }
                addErrors( result );
                return View();
                }

            //
            // GET: /Account/ResetPasswordConfirmation
            [AllowAnonymous]
            public ActionResult ResetPasswordConfirmation()
            {
            return View();
            }
        #endregion
        #region Send and verify Code
            //
            // GET: /Account/SendCode
            [AllowAnonymous]
            public async Task<ActionResult> SendCode( string returnUrl )
                {
                var userId = await _signInManager.GetVerifiedUserIdAsync();
                /*if (userId == null)
                {
                    return View("Error");
                }*/
                var userFactors = await _userManager.GetValidTwoFactorProvidersAsync( userId );
                var factorOptions = userFactors.Select( purpose => new SelectListItem { Text = purpose, Value = purpose } ).ToList();
                return View( new SendCodeViewModel { Providers = factorOptions, ReturnUrl = returnUrl } );
                }

            //
            // POST: /Account/SendCode
            [HttpPost]
            [AllowAnonymous]
            [ValidateAntiForgeryToken]
            public async Task<ActionResult> SendCode( SendCodeViewModel model )
                {
                if (!ModelState.IsValid)
                    {
                    return View();
                    }

                // Generate the token and send it
                if (!await _signInManager.SendTwoFactorCodeAsync( model.SelectedProvider ))
                    {
                    return View( "Error" );
                    }
                return RedirectToAction( "VerifyCode", new { Provider = model.SelectedProvider, model.ReturnUrl } );
                }

            //
            // GET: /Account/VerifyCode
            [AllowAnonymous]
            public async Task<ActionResult> VerifyCode( string provider, string returnUrl )
                {
                // Require that the user has already logged in via username/password or external login
                if (!await _signInManager.HasBeenVerifiedAsync())
                    {
                    return View( "Error" );
                    }
                var user = await _userManager.FindByIdAsync( await _signInManager.GetVerifiedUserIdAsync() );
                if (user != null)
                    {
                    ViewBag.Status = "For DEMO purposes the current " + provider + " code is: " + await _userManager.GenerateTwoFactorTokenAsync( user.Id, provider );
                    }
                return View( new VerifyCodeViewModel { Provider = provider, ReturnUrl = returnUrl,Code="",RememberBrowser=false } );
                }

            //
            // POST: /Account/VerifyCode
            [HttpPost]
            [AllowAnonymous]
            [ValidateAntiForgeryToken]
            public async Task<ActionResult> VerifyCode( VerifyCodeViewModel model )
            {
            if (!ModelState.IsValid)
                {
                return View( model );
                }

            var result = await _signInManager.TwoFactorSignInAsync( model.Provider, model.Code, isPersistent: false, rememberBrowser: model.RememberBrowser );
            switch (result)
                {
                case SignInStatus.Success:
                    return redirectToLocal( model.ReturnUrl );
                case SignInStatus.LockedOut:
                    return View( "Lockout" );
                default:
                    ModelState.AddModelError( "", "Invalid code." );
                    return View( model );
                }
            }
        #endregion
        #region OtherMethods
            private void addErrors( IdentityResult result )
            {
            foreach (var error in result.Errors)
                {
                ModelState.AddModelError( "", error );
                }
            }
        #endregion


        private ActionResult redirectToLocal( string returnUrl )
            {
            if (Url.IsLocalUrl( returnUrl ))
                {
                return Redirect( returnUrl );
                }
            return RedirectToAction( "Index", "Home" );
            }
        }
    }