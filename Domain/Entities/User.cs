using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class User : BaseEntity
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int? HouseId { get; set; }
        public int UserRoleId { get; set; }
        public UserRole UserRole { get; set; }
        //public House? House { get; set; }
        public List<Transport>? Transports { get; set; }
        public List<GeneralConsumption>? GeneralConsumptions { get; set; }
        public string? AccessToken { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public string? RefreshToken { get; set; }
        public decimal FootPrint { get; set; }
    }
}
