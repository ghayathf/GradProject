using Microsoft.IdentityModel.Tokens;
using TheNeqatcomApp.Core.DTO;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TheNeqatcomApp.Core.Data;
using TheNeqatcomApp.Core.Repository;
using TheNeqatcomApp.Core.Service;

namespace TheNeqatcomApp.Infra.Service
{
    public class UserService:IUserService
    {
        
            private readonly IUserRepository userRepository;

            public UserService(IUserRepository userRepository)
            {
                this.userRepository = userRepository;
            }

        public List<Followers> GetAllGpfollower(int lendId)
        {
            return userRepository.GetAllGpfollower(lendId);
        }
        public void addfollower(int lendId, int loaneId)
        {
            userRepository.addfollower(lendId, loaneId);
        }
        public void DeleteFollower(int lendId, int loaneId)
        {
            userRepository.DeleteFollower(lendId, loaneId);
        }
        public string Auth(Gpuser login)
        {

            var result = userRepository.Auth(login);
            if (result == null)
            {
                return null;
            }
            else
            {
                var securitykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKeyNeqatcommmmmmmmmmm"));
                var signin = new SigningCredentials(securitykey, SecurityAlgorithms.HmacSha256);
                var claims = new List<Claim>
                {
                    new Claim("Username", result.Username),
                    new Claim("Role", result.Role.ToString()),
                    new Claim("Userid", result.Userid.ToString()),
                    new Claim("Firstname", result.Firstname),
                    new Claim("Lastname", result.Lastname),
                    new Claim("Phonenumber", result.Phonenum),
                    new Claim("Email", result.Email),
                    new Claim("Imagename", result.Userimage),

                };
                if (result.Role == "Lender")
                {
                    claims.Add(new Claim("Lenderid", result.lenderId.ToString()));
                }
                else if (result.Role == "Loanee")
                {
                    claims.Add(new Claim("Loaneeid", result.loaneeId.ToString()));
                    claims.Add(new Claim("CreditScore", result.Creditscore.ToString()));
                }
                var tokenOptions = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.Now.AddSeconds(60),
                    signingCredentials: signin);
                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
                return tokenString;
            }
        }

        public void CreateUser(Gpuser user)
        {
            userRepository.CreateUser(user);
        }
        public void updatePassword(Gpuser gpuser)
        {
            userRepository.updatePassword(gpuser);
        }
        public void DeleteUser(int id)
        {
            userRepository.DeleteUser(id);
        }

        public List<Gpuser> GetAllUsers()
        {
            return userRepository.GetAllUsers();
        }

        public Gpuser GetUserById(int id)
        {
            return userRepository.GetUserById(id);
        }

        public void UpdateUser(Gpuser user)
        {
            userRepository.UpdateUser(user);
        }
    }
}
