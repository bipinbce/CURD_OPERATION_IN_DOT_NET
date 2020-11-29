using CURD_Core_ADO.Helpers;
using CURD_Core_ADO.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace CURD_Core_ADO.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        [HttpPost]
        [Route("AddUser")]
        public string AddUser(UserCreateModel userCreateModel)
        {
            // validate input
            if (string.IsNullOrWhiteSpace(userCreateModel.UserName))
            {
                return "Invalid User Name";
            }
            if (string.IsNullOrWhiteSpace(userCreateModel.UserEmail))
            {
                return "Invalid User email";
            }
            if (string.IsNullOrWhiteSpace(userCreateModel.UserAddress))
            {
                return "Invalid User address";
            }
            if (string.IsNullOrWhiteSpace(userCreateModel.MobileNo))
            {
                return "Invalid User mobile";
            }
            if (string.IsNullOrWhiteSpace(userCreateModel.AdditionalInfo))
            {
                return "Invalid User additional info";
            }
            if (userCreateModel.DateOfBirth == null || userCreateModel.DateOfBirth == default(DateTime))
            {
                return "Invalid User date of birth";
            }
            // query for inserting user
            var query = $"insert into Users(UserId, UserName,MobileNo, UserEmail, UserAddress,DateOfBirth, AdditionalInfo, CreateDate" +
                $" )values(@pUserId, @pUserName, @pMobilrNo, @pUserEmail, @pAddress, @pDateOfBirth, @pAdditionInfo, @pCreateDate)";
            var sqlParameters = new SqlParameter[]
            {
                new SqlParameter("@pUserId", Guid.NewGuid()),
                new SqlParameter("@pUserName", userCreateModel.UserName),
                new SqlParameter("@pMobilrNo", userCreateModel.MobileNo),
                new SqlParameter("@pUserEmail", userCreateModel.UserEmail),
                new SqlParameter("@pAddress", userCreateModel.UserAddress),
                new SqlParameter("@pDateOfBirth", userCreateModel.DateOfBirth),
                new SqlParameter("@pAdditionInfo", userCreateModel.AdditionalInfo),
                new SqlParameter("@pCreateDate", DateTime.UtcNow)
            };
            var result = SqlHelper.ExecuteNonQuery(query, sqlParameters);
            return result;
        }
        [HttpGet]
        [Route("GetListUsers")]
        public List<UserModel> GetUsers()
        {
            var userData = new List<UserModel>();

            // query for inserting user
            var query = $"select * from Users ";

            var result = SqlHelper.ExecuteQuery(query, null);
            if (result.Rows.Count > 0)
            {
                userData = (from DataRow dr in result.Rows
                            select new UserModel()
                            {
                                UserId = (dr["UserId"]) == DBNull.Value ? Guid.Empty : Guid.Parse(Convert.ToString(dr["UserId"])),
                                UserName = (dr["UserName"]) == DBNull.Value ? "" : Convert.ToString(dr["UserName"]),
                                UserEmail = (dr["UserEmail"]) == DBNull.Value ? "" : Convert.ToString(dr["UserEmail"]),
                                UserAddress = (dr["UserAddress"]) == DBNull.Value ? "" : Convert.ToString(dr["UserAddress"]),
                                MobileNo = (dr["MobileNo"]) == DBNull.Value ? "" : Convert.ToString(dr["MobileNo"]),
                                AdditionalInfo = (dr["AdditionalInfo"]) == DBNull.Value ? "" : Convert.ToString(dr["AdditionalInfo"]),
                                DateOfBirth = (dr["DateOfBirth"]) == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dr["DateOfBirth"]),
                                CreateDate = (dr["CreateDate"]) == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dr["CreateDate"]),
                            }).ToList();
            }
            return userData;

        }
        [HttpGet]
        [Route("GetUserById")]
        public UserModel GetUserById(Guid userId)
        {
            var userData = new UserModel();

            // query for inserting user
            var query = $"select * from Users where UserId = @pUserId";
            var sqlParameter = new SqlParameter[]
            {
                new SqlParameter("@pUserId", userId)
            };
            var result = SqlHelper.ExecuteQuery(query, sqlParameter);
            if (result.Rows.Count > 0)
            {
                userData = (from DataRow dr in result.Rows
                            select new UserModel()
                            {
                                UserId = (dr["UserId"]) == DBNull.Value ? Guid.Empty : Guid.Parse(Convert.ToString(dr["UserId"])),
                                UserName = (dr["UserName"]) == DBNull.Value ? "" : Convert.ToString(dr["UserName"]),
                                UserEmail = (dr["UserEmail"]) == DBNull.Value ? "" : Convert.ToString(dr["UserEmail"]),
                                UserAddress = (dr["UserAddress"]) == DBNull.Value ? "" : Convert.ToString(dr["UserAddress"]),
                                MobileNo = (dr["MobileNo"]) == DBNull.Value ? "" : Convert.ToString(dr["MobileNo"]),
                                AdditionalInfo = (dr["AdditionalInfo"]) == DBNull.Value ? "" : Convert.ToString(dr["AdditionalInfo"]),
                                DateOfBirth = (dr["DateOfBirth"]) == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dr["DateOfBirth"]),
                                CreateDate = (dr["CreateDate"]) == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dr["CreateDate"]),
                            }).FirstOrDefault();
            }
            return userData;
        }
        [HttpDelete]
        [Route("DeleteUser")]
        public string DeleteUser(Guid userId)
        {
            // query for inserting user
            var query = $"delete from Users where UserId = @pUserId";
            var sqlParameters = new SqlParameter[]
            {
                new SqlParameter("@pUserId", userId)
            };
            var result = SqlHelper.ExecuteNonQuery(query, sqlParameters);
            return result;
        }
        [HttpPatch]
        [Route("UpdateUser")]
        public string UpdateUser(UserUpdateModel userUpdateModel)
        {
            var isUserExist = GetUserById(userUpdateModel.UserId);
            if (isUserExist == null)
            {
                return "Invalid UserId!";
            }
            if (string.IsNullOrEmpty(userUpdateModel.UserName))
            {
                userUpdateModel.UserName = isUserExist.UserName;
            }
            if (string.IsNullOrEmpty(userUpdateModel.UserEmail))
            {
                userUpdateModel.UserEmail = isUserExist.UserEmail;
            }
            if (string.IsNullOrEmpty(userUpdateModel.UserAddress))
            {
                userUpdateModel.UserAddress = isUserExist.UserAddress;
            }
            if (string.IsNullOrEmpty(userUpdateModel.MobileNo))
            {
                userUpdateModel.MobileNo = isUserExist.MobileNo;
            }
            if (string.IsNullOrEmpty(userUpdateModel.AdditionalInfo))
            {
                userUpdateModel.AdditionalInfo = isUserExist.AdditionalInfo;
            }
            if (userUpdateModel.DateOfBirth != null || userUpdateModel.DateOfBirth != default(DateTime))
            {
                userUpdateModel.DateOfBirth = isUserExist.DateOfBirth;
            }

            // query for inserting user
            var query = $"update Users set  UserName = @pUserName, MobileNo = @pMobileNo, UserAddress = @pAddress," +
                $" DateOfBirth = @pDateOfBirth, AdditionalInfo = @pAdditionInfo, UserEmail = @pUserEmail where UserId = @pUserId";

            var sqlParameters = new SqlParameter[]
            {
                new SqlParameter("@pUserId", userUpdateModel.UserId),
                new SqlParameter("@pUserName", userUpdateModel.UserName),
                new SqlParameter("@pMobileNo", userUpdateModel.MobileNo),
                new SqlParameter("@pUserEmail", userUpdateModel.UserEmail),
                new SqlParameter("@pAddress", userUpdateModel.UserAddress),
                new SqlParameter("@pDateOfBirth", userUpdateModel.DateOfBirth),
                new SqlParameter("@pAdditionInfo", userUpdateModel.AdditionalInfo)
            };
            var result = SqlHelper.ExecuteNonQuery(query, sqlParameters);
            return result;
        }

    }
}
