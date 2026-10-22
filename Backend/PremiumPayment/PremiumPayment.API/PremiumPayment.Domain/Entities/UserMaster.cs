using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PremiumPayment.Domain.Entities
{
    public class UserMaster
    {
        public long user_id { get; set; }
        public long employee_id { get; set; }
        public string username { get; set; } = string.Empty;
        public string password_hash { get; set; } = string.Empty;
        public string email { get; set; } = string.Empty;
        public string mobile { get; set; } = string.Empty;
        public long role_id { get; set; }
        public long branch_id { get; set; }
        public string status { get; set; } = string.Empty;
    }

}
