using System;

namespace EcommApi.Models
{

    public class Token
    {
        public Token()
        {
            this.CreatedOn = DateTime.UtcNow;
            this.ExpiryDate = this.CreatedOn.AddHours(1);
            this.IsValid = true;
        }
        public int Id { get; set; }
        public string tokenId { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ExpiryDate { get; set; }
        public bool IsValid { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}