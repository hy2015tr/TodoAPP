using System;
using System.Text;
using System.Linq;
using TodoAPI.Models;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;

namespace TodoAPI.Services
{
    //-------------------------------------------------------------------------------------------------------------------------//

    public interface IUserService
    {
        Models.TbUser Authenticate(string username, string password);
        IQueryable<Models.TbUser> GetAll();
        Models.TbUser GetById(int id);
    }

    //-------------------------------------------------------------------------------------------------------------------------//

    public class UserService : IUserService
    {
        //---------------------------------------------------------------------------------------------------------------------//

        private readonly string m_SecretKey;
        private readonly TodoContext m_TodoContext;

        //---------------------------------------------------------------------------------------------------------------------//

        public UserService(IConfiguration p_Configuration, TodoContext p_Context)
        {
            m_TodoContext = p_Context;
            m_SecretKey = p_Configuration.GetSection("AppSettings:SecretKey").Value;
        }

        //---------------------------------------------------------------------------------------------------------------------//

        public Models.TbUser Authenticate(string username, string password)
        {
            // User
            var user = m_TodoContext.TbUser.SingleOrDefault(x => x.Username == username && x.Password == password);

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
                Expires = DateTime.UtcNow.AddDays(7),
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

        public IQueryable<Models.TbUser> GetAll()
        {
            // Users
            foreach (var user in m_TodoContext.TbUser)
            {
                user.Password = null;
            };
  
            // Return
            return m_TodoContext.TbUser;
        }

        //---------------------------------------------------------------------------------------------------------------------//

        public Models.TbUser GetById(int id)
        {
            // Get
            var user = m_TodoContext.TbUser.FirstOrDefault(x => x.Id == id);

            // Reset
            if (user != null) user.Password = null;

            // Return
            return user;
        }

        //---------------------------------------------------------------------------------------------------------------------//

    }
}