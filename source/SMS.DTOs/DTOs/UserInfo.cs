using System;

namespace SMS.DTOs.DTOs
{
    public class UserInfo
    {
        public string Email { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PermanentAddress { get; set; }
        public DateTime? CreationDate { get; set; }
        public string Phone { get; set; }
        public string ImageName { get; set; }
        public string ImagePath { get; set; }
        public int? ImageSize { get; set; }
        public byte[] Image { get; set; }
        public Guid ImageId { get; set; }
        public string ImageExtension { get; set; }
        public Guid PersonId { get; set; }



    }
}
