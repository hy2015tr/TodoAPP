using System;
using System.Text;
using System.Linq;
using TodoAPI.Models;
using System.Security.Claims;
using System.Collections.Generic;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;

namespace TodoAPI.Services
{
    //-------------------------------------------------------------------------------------------------------------------------//

    public interface IUserService
    {
        TbUser Authenticate(string username, string password);
        List<TbUser> GetUsers();
        TbUser GetUser(int id);
    }

    //-------------------------------------------------------------------------------------------------------------------------//

    public class UserService : IUserService
    {
        //---------------------------------------------------------------------------------------------------------------------//

        private readonly string m_SecretKey;
        private readonly TodoDBContext m_TodoDBContext;

        //---------------------------------------------------------------------------------------------------------------------//

        public UserService(TodoDBContext p_Context, IConfiguration p_Configuration)
        {
            m_TodoDBContext = p_Context;
            m_SecretKey = p_Configuration.GetSection("AppSettings:SecretKey").Value;
        }

        //---------------------------------------------------------------------------------------------------------------------//

        public TbUser Authenticate(string username, string password)
        {
            // User
            var user = m_TodoDBContext.TbUser.SingleOrDefault(x => x.Username == username && x.Password == password);

            // Check
            if (user == null) return null;

            // JWT Generate
            var keyBA = Encoding.ASCII.GetBytes(m_SecretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("role", user.Role),
                    new Claim("user", user.Username),
                }),
                Expires = DateTime.UtcNow.AddDays(100),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyBA), SecurityAlgorithms.HmacSha256Signature)
            };

            // Token
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);

            // Reset
            user.Password = null;

            // Return
            return user;
        }

        //---------------------------------------------------------------------------------------------------------------------//

        public List<TbUser> GetUsers()
        {
            // Reset Passwords
            foreach (var user in m_TodoDBContext.TbUser)
            {
                user.Password = null;
            };
  
            // Return
            return m_TodoDBContext.TbUser.ToList();
        }

        //---------------------------------------------------------------------------------------------------------------------//

        public TbUser GetUser(int id)
        {
            // Get
            var user = m_TodoDBContext.TbUser.FirstOrDefault(x => x.Id == id);

            // Reset
            if (user != null) user.Password = null;

            // Return
            return user;
        }

        //---------------------------------------------------------------------------------------------------------------------//

    }
}