using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    [Table("tb_t_account_role")]
    public class AccountRole
    {
        [Key]
        public int AccountRoleId { get; set; }

        [JsonIgnore]
        public virtual Account account
        {
            get;
            set;
        }
        public string AccountNIK { get; set; }
        [JsonIgnore]
        public virtual Role role
        {
            get;
            set;
        }
        public int RoleId { get; set; }
    }
}
