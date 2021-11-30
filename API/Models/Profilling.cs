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
    [Table("tb_t_profilling")]
    public class Profilling
    {
        [JsonIgnore]
        [Key]
        public string NIK { get; set; }
        [JsonIgnore]
        public int EducationId { get; set; }
        public virtual Account account 
        {
            get;
            set;
        }
        public virtual Education education 
        {
            get;
            set;
        }
    }
}
