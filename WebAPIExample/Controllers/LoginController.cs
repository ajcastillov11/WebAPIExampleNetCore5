using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebAPIExample.Data;
using WebAPIExample.Models;

namespace WebAPIExample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {

        private readonly AppDBContext _db;
        private readonly JwtSettings _settings;

        public LoginController(AppDBContext db, JwtSettings settings)
        {

            _db = db;
            _settings = settings;
        }


        [HttpPost]
        public IActionResult Login([FromBody] User modelUser)
        {
            try
            {
                IActionResult response = Unauthorized();

                if (modelUser == null || !ModelState.IsValid)
                {
                    return response;
                }

                AppUserAuth modelAuth = AuthenticateUser(modelUser);

                if (modelAuth.IsAuthenticated)
                {
                    modelAuth.BearerToken = GenerateJSONWebToken(modelAuth);
                    return Ok(modelAuth);
                }
                else
                {
                    return StatusCode(404, "Invalid User Name/Password!");
                }



            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Generates the json web token.
        /// </summary>
        /// <param name="modelAuth">The model authentication.</param>
        /// <returns></returns>
        /// <exception cref="Claim">JwtRegisteredClaimNames.UniqueName, modelAuth.UserName</exception>
        private string GenerateJSONWebToken(AppUserAuth modelAuth)
        {
            try
            {
                SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.Key));
                SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                List<Claim> jwtClaims = new()
                {
                    new Claim(JwtRegisteredClaimNames.UniqueName, modelAuth.UserName),
                    new Claim(JwtRegisteredClaimNames.Iss, _settings.Issuer),
                    new Claim(JwtRegisteredClaimNames.Aud, _settings.Audience),
                    new Claim("IsAuthenticated", modelAuth.IsAuthenticated.ToString().ToLower()),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };


                modelAuth.Claims.ForEach(x =>
                {
                    jwtClaims.Add(new Claim(x.ClaimType, x.ClaimValue.ToString().ToLower()));
                });


                JwtSecurityToken token = new JwtSecurityToken(
                  issuer: _settings.Issuer,
                  audience: _settings.Audience,
                  claims: jwtClaims,
                  expires: DateTime.Now.AddMinutes(_settings.minutesToExpiration),
                  signingCredentials: credentials);

                return new JwtSecurityTokenHandler().WriteToken(token);

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }



        /// <summary>
        /// Authenticates the user.
        /// </summary>
        /// <param name="modelAuth">The model authentication.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception"></exception>
        private AppUserAuth AuthenticateUser(User modelAuth)
        {
            try
            {
                AppUserAuth response = new();

                User obj = _db.User.FirstOrDefault(x => x.UserName == modelAuth.UserName && x.Password == modelAuth.Password);

                if (obj != null)
                {
                    response.UserName = modelAuth.UserName;
                    response.IsAuthenticated = obj != null;

                    response.Claims = _db.UserClaim.Where(x => x.UserId == obj.Id).ToList();
                }

                return response;

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

    }
}
