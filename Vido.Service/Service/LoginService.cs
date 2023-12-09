﻿using Dapper;
using Microsoft.IdentityModel.Tokens;
using Pos.Application.Extensions.Helper;
using Pos.Model.Model.Auth;
using Pos.Model.Model.Proc;
using Pos.Model.Model.Table;
using Promotion.Application.Extensions;
using Promotion.Application.Interfaces;
using Promotion.Model.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Promotion.Application.Services
{
    public interface ILoginService : IEntityService<RDStore>
    {
        string LoginUser(string UserID, string Password);
        IEnumerable<dynamic> getJWTTokenClaim(string token);
        int getStoreIdByToken(string token);
    }
    public class LoginService : POSEntityService<RDStore>, ILoginService
    {
        public LoginService()
        {

        }
        public LoginService(IDbConnection db) : base(db)
        {

        }

        private IEnumerable<FT_Store> getListStore(string where)
        {
            return _connection.AutoConnect().SqlQuery<FT_Store>($"select * from FT_Store() Where {where}");
        }


        private IEnumerable<Claim> GetUserClaims(JWTUser user)
        {
            IEnumerable<Claim> claims = new Claim[]
            {
                new Claim(ClaimTypes.Name, user.STORE_NAME),
                new Claim("USERID", user.USERID),
                new Claim("EMAILID", user.EMAILID),
                new Claim("PHONE", user.PHONE),
                new Claim("ACCESS_LEVEL", user.ACCESS_LEVEL.ToUpper()),
                new Claim("READ_ONLY", user.READ_ONLY.ToUpper())
            };
            return claims;
        }

        public IEnumerable<dynamic> getJWTTokenClaim(string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var securityToken = (JwtSecurityToken)tokenHandler.ReadToken(token);
                var claimValue = securityToken.Claims.Select(x=> new { x.Value, x.Type });
                return claimValue;
            }
            catch (Exception)
            {
                //TODO: Logger.Error
                return null;
            }
        }
        
        public string LoginUser(string UserID, string Password)
        {
            //Get user details for the user who is trying to login
            var LstUser = getListStore($" LOWER(Email) = LOWER('{UserID}')");
            var user = LstUser.FirstOrDefault();
           
            //Authenticate User, Check if it’s a registered user in Database
            if (user == null)
                return null;

            //If it's registered user, check user password stored in Database 
            //For demo, password is not hashed. Simple string comparison 
            //In real, password would be hashed and stored in DB. Before comparing, hash the password
            Password = Password.CreateMD5Hash();
            if (Password.ToLower() == user.Password.ToLower())
            {
                //Authentication successful, Issue Token with user credentials
                //Provide the security key which was given in the JWToken configuration in Startup.cs
                var key = Encoding.ASCII.GetBytes(AuthData.Key);
                //Generate Token for user 
                var jwtuser = new JWTUser
                {
                    USERID = user.StoreID.ToString(),
                    PASSWORD = user.Password,
                    STORE_NAME = user.StoreName,
                    EMAILID = user.Email,
                    PHONE = user.Phone,
                    ACCESS_LEVEL = "Analyst",
                    READ_ONLY = "true"
                };

                var JWToken = new JwtSecurityToken(
                    issuer: AuthData.Issuer,
                    audience: AuthData.Audience,
                    claims: GetUserClaims(jwtuser),
                    notBefore: new DateTimeOffset(DateTime.Now).DateTime,
                    expires: new DateTimeOffset(DateTime.Now.AddDays(1)).DateTime,
                    //Using HS256 Algorithm to encrypt Token
                    signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key),
                                        SecurityAlgorithms.HmacSha256Signature)
                );
                var token = new JwtSecurityTokenHandler().WriteToken(JWToken);
                return token;
            }
            else
            {
                return null;
            }
        }

        public int getStoreIdByToken(string token)
        {
            token = token.Replace("Bearer ", ""); 
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = (JwtSecurityToken)tokenHandler.ReadToken(token);
            //var claimValue = securityToken.Claims.Select(x => new { x.Value, x.Type });
            var StoreID = securityToken.Claims.FirstOrDefault(x => x.Type == "USERID").Value;
            return Convert.ToInt32(StoreID);
        }
    }
}
