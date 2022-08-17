using Apj.Net.Dao;
using Apj.Net.Dao.Common;
using Apj.Net.Dao.Common.Query;
using Apj.Net.Dao.Connection;
using APJ.NET.Helper;
using APJ.NET.Models;
using APJ.NET.Models.Dto;
using APJ.NET.Models.User;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace APJ.NET.Services
{

    public interface IAuthService
    {
        public ClientUserModel Authenticate(string login, string password);
        public Users FindUsersById(string id);
    }
    public class AuthentificationSerivce : UsersService, IAuthService
    {

        internal static string Secret = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJpc3MiOiJtb29jX3NwcmluZ19zZWN1cml0eSIsImlhdCI6MTU5NjIwNjcxMywiZXhwIjoxNjI3NzQyNzEzLCJhdWQiOiJtb29jX3N0dWRlbnRzIiwic3ViIjoibW9vY19zcHJpbmdfc2VjdXJpdHkiLCJHaXZlbk5hbWUiOiJNb29jIn0.gx1iCqhrx1gikFigcUTqlBBdGZPbXs6bZYxDp5V93fs";
        public AuthentificationSerivce() { }

        public ClientUserModel Authenticate(string login, string password)
        {
            IApjDao dao = ApjDaoFactory.GetInstance();
            DbConnection connection = ConnectionFactory.GetConnection();
            ClientUserModel model = null;
            try
            {
                connection.Open();
                IList<Users> data = SearchQuery.findWhere<Users>(connection, Criteria.Eq("Login", login));
                if (data.Count > 0)
                {
                    model = new ClientUserModel();
                    var user = data[0];
                    model.Login = user.Login;
                    model.Id = user.Id;
                    model.Username = user.Username;
                    model.Role = new Role();
                    model.Role.Id = user.IdRole;
                    model.Role.Load(connection);
                    model.Token = GenerateJwtToken(model.Id);
                }
            }
            finally
            {
                if (connection != null) connection.Close();
            }
            return model;
        }

        private string GenerateJwtToken(string userId)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", userId) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }

}
