//using DataAccess;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Identity.UI.Services;
//using Microsoft.AspNetCore.Mvc;
//using Models.Identity;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Security.Claims;
//using System.Threading.Tasks;

//namespace DeluzionalPenguinz.API.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class UserController : ControllerBase
//    {
//        private readonly ApplicationDbContext appDb;
//        private readonly UserManager<IdentityUser> userManager;
//        private readonly SignInManager<IdentityUser> signInManager;
//        private readonly IEmailSender emailSender;

//        public UserController(ApplicationDbContext appDb,
//            UserManager<IdentityUser> userManager,
//            SignInManager<IdentityUser> signInManager,
//            IEmailSender emailSender)
//        {
//            this.appDb = appDb;
//            this.userManager = userManager;
//            this.signInManager = signInManager;
//            this.emailSender = emailSender;
//        }

//        [HttpGet("GetEmail")]
//        public IActionResult GetEmailOfSignedInUser()
//        {
//            return Ok(new Response()
//            { Message = User.FindFirstValue(ClaimTypes.Email) });
//        }


//        [Authorize(Roles = "Mpatiris")]
//        [HttpGet("GetEmailForMpatiris")]
//        public IActionResult GetEmailOfSignedInUserForMpatiris()
//        {
//            return Ok(new Response()
//            { Message = User.FindFirstValue(ClaimTypes.Email) });
//        }


//        [HttpPost("ForgotPassword")]
//        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordUser forgotPasswordUser)
//        {

//            var user = await userManager.FindByEmailAsync(forgotPasswordUser.Email);

//            if (user is null)
//                return null;

//            var token = await userManager.GeneratePasswordResetTokenAsync(user);

//            string htmlMessage = $"<h1>You have requested Password Change!</h1>" +
//                                "<h3>Please follow this link to do this!</h3>" +
//                            $"<a href=\"https:\\\\localhost:44354\\forgotpasswordconfirmation?token={token}&email={user.Email}\"></a>";


//            await emailSender.SendEmailAsync(user.Email, "Change Password Request", htmlMessage);



//            return Ok(
//                new Response()
//                { Message = "Check your email for your confirmation link!" });


//        }



//        [HttpPost("ForgotPasswordConfirmation")]
//        public async Task<IActionResult> ForgotPasswordConfirmation([FromBody] ForgotPasswordUserConfirmation forgotPasswordUser)
//        {
//            var user = await userManager.FindByEmailAsync(forgotPasswordUser.Email);

//            var result = await userManager.ResetPasswordAsync(user, forgotPasswordUser.Token,
//                forgotPasswordUser.Password);



//            return Ok(new Response()
//            { Message = "Everything Ok! You can login now with the new Password" });
//        }


//    }


//}
