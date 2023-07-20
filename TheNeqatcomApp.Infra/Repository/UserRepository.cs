using TheNeqatcomApp.Core.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using TheNeqatcomApp.Core.Common;
using TheNeqatcomApp.Core.Repository;
using System.Linq;
using System.Data;
using Dapper;
using TheNeqatcomApp.Core.Data;
using System.Security.Cryptography;

namespace TheNeqatcomApp.Infra.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly IDBContext _dbContext;
        public UserRepository(IDBContext dbContext)
        {
            this._dbContext = dbContext;
        }
        public string EncryptPassword(string password)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
                byte[] hash = md5.ComputeHash(passwordBytes);
                return Convert.ToBase64String(hash);
            }
        }
        public List<Gpuser> GetAllUsers()
        {
            string query = "SELECT * FROM GPUser";
            IEnumerable<Gpuser> result = _dbContext.Connection.Query<Gpuser>(query);
            return result.ToList();
        }
        public void CreateUser(Gpuser user)
        {
            var parameters = new
            {
                Fname = user.Firstname,
                Lname = string.IsNullOrEmpty(user.Lastname) ? "." : user.Lastname,
                emaail = user.Email,
                pass = EncryptPassword(user.Password),
                phone = user.Phonenum,
                addr = user.Address,
                R = user.Role,
                Uname = user.Username,
                img = string.IsNullOrEmpty(user.Userimage) ? "https://neqatcomstorage.blob.core.windows.net/images-container/80b8f1df-ad88-4b82-8f75-b5e15953281a_1876578-200.png" : user.Userimage
            };

            var result = _dbContext.Connection.Execute("INSERT INTO GPUser (firstname, lastname, email, password, phonenum, address, role, username, userimage) VALUES (@Fname, @Lname, @emaail, @pass, @phone, @addr, @R, @Uname, @img)",
                                                        parameters);
        }

        public Gpuser GetUserById(int id)
        {
            var parameters = new { idd = id };
            string query = "SELECT * FROM GPUser WHERE GPUser.userid = @idd";
            Gpuser user = _dbContext.Connection.QueryFirstOrDefault<Gpuser>(query, parameters);
            return user;
        }

        public void UpdateUser(Gpuser user)
        {
            var parameters = new
            {
                idd = user.Userid,
                Fname = user.Firstname,
                Lname = user.Lastname,
                emaail = user.Email,
                phone = user.Phonenum,
                addr = user.Address,
                R = user.Role,
                Uname = user.Username,
                img = user.Userimage
            };

            var result = _dbContext.Connection.Execute("UPDATE GPUser SET firstname = @Fname, lastname = @Lname, email = @emaail, phonenum = @phone, address = @addr, role = @R, username = @Uname, userimage = @img WHERE userid = @idd",
                                                       parameters);
        
    }

        public void DeleteUser(int id)
        {
            var parameters = new
            {
                id
            };

            var result = _dbContext.Connection.Execute("DELETE FROM GPUser WHERE userid = @id", parameters);

        }

        public LoginClaims Auth(Gpuser login)
        {
            var parameters = new
            {
                email = EscapeSpecialCharacters(login.Email),
                password = EncryptPassword(EscapeSpecialCharacters(login.Password))
            };

            string query = @"
        SELECT U.USERNAME, U.userid, U.firstname, U.lastname, U.email, U.phonenum, U.userimage, U.role,
        CASE WHEN U.role = 'Lender' THEN L.lenderid ELSE NULL END AS lenderId,
        CASE WHEN U.role = 'Loanee' THEN LN.loaneeid ELSE NULL END AS loaneeId,
        LN.creditscore,
        L.registerstatus
        FROM GPUser U
        LEFT JOIN GPlenderstore L ON U.userid = L.lenderuserid AND U.role = 'Lender'
        LEFT JOIN GPloanee LN ON U.userid = LN.loaneeuserid AND U.role = 'Loanee'
        WHERE U.email = @email
        AND U.password = @password";

            var result = _dbContext.Connection.QuerySingleOrDefault<LoginClaims>(query, parameters);

            return result;
        }
        private string EscapeSpecialCharacters(string input)
        {
            // Escape special characters: <, >, ', "
            input = input.Replace("<", "&lt;")
                         .Replace(">", "&gt;")
                         .Replace("'", "''")
                         .Replace("\"", "\"\"");

            return input;
        }


        public void updatePassword(Gpuser gpuser)
        {
            var parameters = new
            {
                IDD = gpuser.Userid,
                PASS = EncryptPassword(gpuser.Password)
            };

            var result = _dbContext.Connection.Execute("UPDATE GPUser SET password = @PASS WHERE userid = @IDD", parameters);

        }

        public List<Followers> GetAllGpfollower(int lendId)
        {
            string query = @"
        SELECT gpfollowers.*, gploanee.*, gpuser.*
        FROM gpfollowers
        LEFT JOIN gploanee ON gpfollowers.loaneeid = gploanee.loaneeid
        LEFT JOIN gpuser ON gploanee.loaneeuserid = gpuser.userid
        WHERE gpfollowers.lenderid = @LenderId;
    ";

            var parameters = new { LenderId = lendId };

            IEnumerable<Followers> result = _dbContext.Connection.Query<Followers>(query, parameters);

            return result.ToList();
        }

        public void addfollower(int lendId, int loaneId)
        {
            var parameters = new
            {
                lendeId = lendId,
                loaneeId = loaneId
            };

            string query = "INSERT INTO GPFOLLOWERS (LENDERID, LOANEEID) VALUES (@lendeId, @loaneeId)";

            _dbContext.Connection.Execute(query, parameters);
        }

        public void DeleteFollower(int lendId, int loaneId)
        {
            var parameters = new
            {
                lendId,
                loaneId
            };

            var result = _dbContext.Connection.Execute("DELETE FROM GPFOLLOWERS WHERE LENDERID = @lendId AND LOANEEID = @loaneId", parameters);

        }
    }
}
