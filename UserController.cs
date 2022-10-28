using BusinessLayer.Interfaces;
using CommonLayer.Models;
using Fundoo.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace Fundoo.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        IUserBL Userbl;
        public UsersController(IUserBL Userbl)
        {
            this.Userbl = Userbl;
        }
        [HttpPost("Resgister")]
        public IActionResult AddUser(UserRegistration userRegistration)
        {
            try
            {
                var reg = this.Userbl.Registration(userRegistration);
                if (reg != null)

                {

                    return this.Ok(new { Success = true, message = "Registration Sucessfull", Response = reg });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Registration Unsucessfull" });
                }
            }
            catch (Exception ex)
            {

                return this.BadRequest(new { Success = false, message = ex.Message });
            }
        }
        [HttpPost("Login")]
        public IActionResult Add(UserLogin LoginRegistration)
        {
            try
            {
                var reg = this.Userbl.Login(LoginRegistration);
                if (reg != null)

                {

                    return this.Ok(new { Success = true, message = "Login Sucessfull", Data = reg });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Login Unsucessfull" });
                }
            }
            catch (Exception ex)
            {

                return this.BadRequest(new { Success = false, message = ex.Message });
            }
        }
        [HttpPost("ForgetPassWord")]
        public IActionResult ForgetPassword(string EmailId)
        {
            try
            {

                var reg = this.Userbl.ForgetPassword(EmailId);
                if (reg != null)

                {

                    return this.Ok(new { Success = true, message = "Token sent Sucessfully please check your mail" });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "unable to send token to mail" });
                }
            }
            catch (Exception ex)
            {

                return this.BadRequest(new { Success = false, message = ex.Message });
            }
        }
        [Authorize]
        [HttpPost("ResetPassword")]
        public IActionResult ResetPassword(string password, string confirmPassword)
        {
            try
            {
                var Email = User.FindFirst(ClaimTypes.Email).Value.ToString();
                //var email = User.Claims.First(e => e.Type == "Email").Value;
                //var result = userBL.ResetPassword(email, password, confirmPassword);

                if (Userbl.ResetPassword(Email, password, confirmPassword))
                {
                    return Ok(new { success = true, message = "Password Reset Successful" });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Password Reset Unsuccessful" });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }





    }


}










