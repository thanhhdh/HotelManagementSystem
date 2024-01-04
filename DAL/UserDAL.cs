using DTO;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class UserDAL
    {
        DBContext connect = new DBContext();
        public bool Login(UserDTO userDTO)
        {
            try
            {
                connect.OpenConnect();
                using (SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM users WHERE UserName = @UserName AND UserPassword = @UserPassword", connect.GetConnection()))
                {
                    command.Parameters.AddWithValue("@UserName", userDTO.UserName);
                    command.Parameters.AddWithValue("@UserPassword", ComputeMD5Hash(userDTO.UserPassword));

                    int count = Convert.ToInt32(command.ExecuteScalar());

                    return count > 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred during the login process.", ex);
            }
            finally
            {
                connect.CloseConnect();
            }
        }

        public DataTable GetUserList()
        {
            SqlCommand command = new SqlCommand("Select * from users", connect.GetConnection());
            connect.OpenConnect();
            try
            {
                SqlDataReader rd = command.ExecuteReader();
                DataTable dataTable = new DataTable();
                dataTable.Load(rd);
                return dataTable;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public UserDTO GetUserById(int userId)
        {
            try
            {
                connect.OpenConnect();
                using (SqlCommand command = new SqlCommand("SELECT * FROM Users WHERE UserId = @UserId", connect.GetConnection()))
                {
                    command.Parameters.AddWithValue("@UserId", userId);

                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        UserDTO user = new UserDTO
                        {
                            UserId = Convert.ToInt32(reader["UserId"]),
                            UserName = reader["UserName"].ToString(),
                            UserEmail = reader["UserEmail"].ToString(),
                            UserPhone = reader["UserPhone"].ToString(),
                        };

                        return user;
                    }
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error :", ex);
            }
            finally
            {
                connect.CloseConnect();
            }
        }
        public UserDTO GetUserByUsername(string username)
        {
            try
            {
                connect.OpenConnect();
                using (SqlCommand command = new SqlCommand("SELECT * FROM users WHERE Username = @Username", connect.GetConnection()))
                {
                    command.Parameters.AddWithValue("@Username", username);

                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        UserDTO user = new UserDTO
                        {
                            UserId = Convert.ToInt32(reader["UserId"]),
                            UserName = reader["UserName"].ToString(),
                            UserEmail = reader["UserEmail"].ToString(),
                            UserPhone = reader["UserPhone"].ToString(),
                            UserPassword = reader["UserPassword"].ToString(),
                        };

                        return user;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving user information.", ex);
            }
            finally
            {
                connect.CloseConnect();
            }
        }

        public UserDTO InsertUser(UserDTO userDTO)
        {
            try
            {
                connect.OpenConnect();
                SqlCommand command = new SqlCommand("insert into users(UserName, UserEmail, UserPhone, UserPassword) values (@Username, @UserEmail, @UserPhone, @UserPassword)", connect.GetConnection());
                command.Parameters.AddWithValue("@UserName", userDTO.UserName);
                command.Parameters.AddWithValue("@UserEmail", userDTO.UserEmail);
                command.Parameters.AddWithValue("@UserPhone", userDTO.UserPhone);
                command.Parameters.AddWithValue("@UserPassword", ComputeMD5Hash(userDTO.UserPassword));
                int result = command.ExecuteNonQuery();

                if (result == 1)
                {
                    return userDTO;
                }
                else
                {
                    throw new Exception("Failed to insert user.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while inserting user." + ex.Message);
            }
            finally
            {
                connect.CloseConnect();
            }
        }

        private string ComputeMD5Hash(string input)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);
                StringBuilder sb = new StringBuilder();

                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("x2"));
                }

                return sb.ToString();
            }
        }

        public List<UserDTO> SearchUserName(string username)
        {
            try
            {
                connect.OpenConnect();
                using (SqlCommand command = new SqlCommand("SELECT * FROM users WHERE UserName LIKE @Username", connect.GetConnection()))
                {
                    command.Parameters.AddWithValue("@Username", "%" + username + "%");

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        List<UserDTO> users = new List<UserDTO>();

                        while (reader.Read())
                        {
                            UserDTO user = new UserDTO();
                            user.UserId = reader.GetInt32("UserId");
                            user.UserName = reader.GetString("UserName");
                            user.UserEmail = reader.GetString("UserEmail");
                            user.UserPhone = reader.GetString("UserPhone");
                            user.UserPassword = reader.GetString("UserPassword");

                            users.Add(user);
                        }

                        return users;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred" + ex.Message);
            }
            finally
            {
                connect.CloseConnect();
            }
        }

        public UserDTO UpdateUser(UserDTO userDTO)
        {
            try
            {

                connect.OpenConnect();
                SqlCommand command = new SqlCommand("UPDATE users SET UserName = @UserName, UserEmail = @UserEmail, UserPhone = @UserPhone WHERE UserId = @UserId", connect.GetConnection());
                command.Parameters.AddWithValue("@UserId", userDTO.UserId);
                command.Parameters.AddWithValue("@UserName", userDTO.UserName);
                command.Parameters.AddWithValue("@UserPhone", userDTO.UserPhone);
                command.Parameters.AddWithValue("@UserEmail", userDTO.UserEmail);

                int result = command.ExecuteNonQuery();

                if (result == 1)
                {
                    return userDTO;
                }
                else
                {
                    throw new Exception("Failed to update user.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while update user." + ex.Message);
            }
            finally
            {
                connect.CloseConnect();
            }
        }

        public void DeleteUser(int  userId)
        {
            try
            {
                connect.OpenConnect();
                SqlCommand command = new SqlCommand("delete users where UserId = @UserId", connect.GetConnection());
                command.Parameters.AddWithValue("UserId", userId);
                command.ExecuteNonQuery();
            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
