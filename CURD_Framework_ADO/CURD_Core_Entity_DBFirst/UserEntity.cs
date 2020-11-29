using System;
using System.Collections.Generic;

#nullable disable

namespace CURD_Core_Entity_DBFirst
{
    public partial class UserEntity
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
}
