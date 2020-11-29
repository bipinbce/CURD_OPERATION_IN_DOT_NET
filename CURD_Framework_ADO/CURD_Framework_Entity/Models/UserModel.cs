using System;

namespace CURD_Framework_Entity.Models
{
    public class UserModel
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string MobileNo { get; set; }
        public string UserEmail { get; set; }
        public string UserAddress { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string AdditionalInfo { get; set; }
        public DateTime CreateDate { get; set; }
        
    }
    public class UserCreateModel
    {        
        public string UserName { get; set; }
        public string MobileNo { get; set; }
        public string UserEmail { get; set; }

        public string UserAddress { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string AdditionalInfo { get; set; }        
    }
    public class UserUpdateModel
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }

        public string MobileNo { get; set; }
        public string UserAddress { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string AdditionalInfo { get; set; }
    }
}