using Microsoft.EntityFrameworkCore.Infrastructure;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    
    [Table("tb_m_account")]
    public class Account
    {
        [JsonIgnore]
        [Key]
        public string NIK { get; set; }
        [JsonIgnore]
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [JsonIgnore]
        public virtual Employee employee {
            get;
            set;
        }
        public virtual Profilling profilling
        {
            get;
            set;
        }
        [JsonIgnore]
        public virtual ICollection<AccountRole> accountrole
        {
            get;
            set;
        }

    }

}
