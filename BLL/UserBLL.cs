using DAL;
using DTO;
using System.Data;
using System.Net;
using System.Numerics;

namespace BLL
{
    public class UserBLL
    {

        UserDAL userDAL = new UserDAL();
        public UserDTO LoginUser(UserDTO userDTO)
        {
            try
            {
                UserDTO loginDTO = new UserDTO
                {
                    UserId = userDTO.UserId,
                    UserName = userDTO.UserName,
                    UserEmail = userDTO.UserEmail,
                    UserPassword = userDTO.UserPassword
                };

                bool loginResult = userDAL.Login(loginDTO);

                if (loginResult)
                {
                    UserDTO loggedInUser = userDAL.GetUserByUsername(userDTO.UserName);
                    return loggedInUser;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred during the login process.", ex);
            }
        }
            
        public List<UserDTO> SearchUsersByUsername(string username)
        {
            try
            {
                return userDAL.SearchUserName(username);
            } catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public DataTable GetUserList()
        {
            try
            {
                var listUser = userDAL.GetUserList();
                return listUser;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public UserDTO GetUserById(int id)
        {
            try 
            { 
            return userDAL.GetUserById(id);
            }
            catch(Exception ex) 
            { 
                throw new Exception(ex.Message, ex);
            }
        }

        public UserDTO InsertUser(UserDTO userDTO)
        {
            try
            {
                UserDTO user = new UserDTO
                {
                    UserId = userDTO.UserId,
                    UserName = userDTO.UserName,
                    UserEmail = userDTO.UserEmail,
                    UserPassword = userDTO.UserPassword,
                    UserPhone = userDTO.UserPhone,
                };
                return userDAL.InsertUser(user);
            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public UserDTO UpdateUser(UserDTO userDTO)
        {
            try
            {
                UserDTO user = new UserDTO
                {
                    UserId = userDTO.UserId,
                    UserName = userDTO.UserName,
                    UserEmail = userDTO.UserEmail,
                    UserPhone = userDTO.UserPhone,
                };
                return userDAL.UpdateUser(user);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void DeleteUser(int userId)
        {
            try
            {
                userDAL.DeleteUser(userId);
            } catch (Exception ex)
            {
                throw new Exception("Có lỗi" + ex.Message);
            }
        }
    }
}