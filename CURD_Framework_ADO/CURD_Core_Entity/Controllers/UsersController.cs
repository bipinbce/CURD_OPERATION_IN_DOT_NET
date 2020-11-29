using CURD_Core_Entity.Context;
using CURD_Core_Entity.Entities;
using CURD_Core_Entity.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CURD_Core_Entity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private MyContext myContext;

        public UsersController(MyContext myContext)
        {
            this.myContext = myContext;
        }
        [HttpPost]
        [Route("AddUser")]
        public string AddUser(UserCreateModel userCreateModel)
        {
            try
            {
                var userEntity = new UserEntity()
                {
                    UserId = Guid.NewGuid(),
                    UserName = userCreateModel.UserName,
                    UserEmail = userCreateModel.UserEmail,
                    UserAddress = userCreateModel.UserAddress,
                    MobileNo = userCreateModel.MobileNo,
                    AdditionalInfo = userCreateModel.AdditionalInfo,
                    DateOfBirth = userCreateModel.DateOfBirth,
                    CreateDate = DateTime.UtcNow
                };
                myContext.UserEntities.Add(userEntity);
                myContext.SaveChanges();
                return "user Added succesfully!";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        [HttpGet]
        [Route("GetUsers")]
        public List<UserModel> GetUsers()
        {
            try
            {
                var result = new List<UserModel>();
                var users = myContext.UserEntities.AsEnumerable().ToList();
                if (users.Any())
                {
                    result = (from user in users
                              select new UserModel()
                              {
                                  UserId = user.UserId,
                                  UserName = user.UserName,
                                  UserEmail = user.UserEmail,
                                  UserAddress = user.UserAddress,
                                  AdditionalInfo = user.AdditionalInfo,
                                  MobileNo = user.MobileNo,
                                  CreateDate = user.CreateDate,
                                  DateOfBirth = user.DateOfBirth
                              }).ToList();
                }
                return result;
            }catch(Exception ex)
            {
                return new List<UserModel>();
            }
        }
        [HttpGet]
        [Route("GetUserById")]
        public UserModel GetUserById(Guid userId)
        {
            try
            {
                var users = myContext.UserEntities.AsEnumerable().Where(e => e.UserId == userId).FirstOrDefault();
                if (users != null)
                {
                    return new UserModel()
                    {
                        UserId = users.UserId,
                        UserName = users.UserName,
                        UserEmail = users.UserEmail,
                        UserAddress = users.UserAddress,
                        AdditionalInfo = users.AdditionalInfo,
                        MobileNo = users.MobileNo,
                        CreateDate = users.CreateDate,
                        DateOfBirth = users.DateOfBirth
                    };
                }
                return new UserModel();
            }catch(Exception ex)
            {
                return new UserModel();
            }
        }
        [HttpPatch]
        [Route("UpdateUser")]
        public string UpdateUser(UserUpdateModel userUpdateModel)
        {
            try
            {
                var isExistUser = myContext.UserEntities.AsEnumerable().Where(e => e.UserId == userUpdateModel.UserId).FirstOrDefault();

                if (isExistUser == null)
                {
                    return "User not found!";
                }
                //isExistUser.UserId = userUpdateModel.UserId;
                isExistUser.UserName = string.IsNullOrWhiteSpace(userUpdateModel.UserName) ? isExistUser.UserName : userUpdateModel.UserName;
                isExistUser.UserEmail = string.IsNullOrWhiteSpace(userUpdateModel.UserEmail) ? isExistUser.UserEmail : userUpdateModel.UserEmail;
                isExistUser.UserAddress = string.IsNullOrWhiteSpace(userUpdateModel.UserAddress) ? isExistUser.UserAddress : userUpdateModel.UserAddress;
                isExistUser.MobileNo = string.IsNullOrWhiteSpace(userUpdateModel.MobileNo) ? isExistUser.MobileNo : userUpdateModel.MobileNo;
                isExistUser.AdditionalInfo = string.IsNullOrWhiteSpace(userUpdateModel.AdditionalInfo) ? isExistUser.AdditionalInfo : userUpdateModel.AdditionalInfo;
                isExistUser.DateOfBirth = (userUpdateModel.DateOfBirth == null || userUpdateModel.DateOfBirth == default(DateTime)) ? isExistUser.DateOfBirth : userUpdateModel.DateOfBirth;

                myContext.SaveChanges();
                return "user updates succesfully!";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }
        [HttpDelete]
        [Route("DeleteUser")]
        public string DeleteUser(Guid userId)
        {
            try
            {
                var isExistUser = myContext.UserEntities.AsEnumerable().Where(e => e.UserId == userId).FirstOrDefault();

                if (isExistUser == null)
                {
                    return "User not found!";
                }
                myContext.UserEntities.Remove(isExistUser);
                myContext.SaveChanges();
                return "user Deleted succesfully!";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }

}
