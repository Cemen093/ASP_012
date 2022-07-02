using Dapper;
using System.Data;
using Microsoft.Data.SqlClient;

namespace DapperMvcApp.Models
{
    public interface IUserRepository
    {
        List<User> GetUsers();
        User GetUserById(int id);
        User GetUserByEmailAndPassword(User user);
        bool Create(User user);
        bool Update(User user);
        bool Delete(int id);
    }
    public class UserRepository : IUserRepository
    {
        string connectionString = null;
        public UserRepository(string _connectionString)
        {
            connectionString = _connectionString;
        }
        public List<User> GetUsers()
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                return db.Query<User>("SELECT * FROM Users").ToList();
            }
        }

        public User GetUserById(int id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = $"SELECT * FROM Users WHERE id = {id}";
                return db.Query<User>(sqlQuery, new { id }).FirstOrDefault();
            }
        }

        public User GetUserByEmailAndPassword(User user)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = $"SELECT * FROM Users WHERE email = \'{user.email}\' and heshPassword = \'{user.heshPassword}\'";
                return db.Query<User>(sqlQuery, user).FirstOrDefault();
            }
        }

        public bool Create(User user)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = $"INSERT INTO Users VALUES(\'{user.email}\', \'{user.heshPassword}\')";
                if (db.Execute(sqlQuery, user) > 0)
                    return true;
                return false;
            }
        }

        public bool Update(User user)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = $"UPDATE Users SET email = \'{user.email}\', heshPassword = \'{user.heshPassword}\' WHERE id = {user.id}";
                if (db.Execute(sqlQuery, user) > 0)
                    return true;
                return false;
            }
        }

        public bool Delete(int id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = $"DELETE FROM Users WHERE id = {id}";
                if (db.Execute(sqlQuery, new { id }) > 0)
                    return true;
                return false;
            }
        }
    }
}