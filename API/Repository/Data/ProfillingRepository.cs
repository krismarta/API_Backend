using API.Context;
using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repository.Data
{
    public class ProfillingRepository : GeneralRepository<MyContext, Profilling, string>
    {
        public ProfillingRepository(MyContext myContext) : base(myContext)
        {

        }
    }
}
