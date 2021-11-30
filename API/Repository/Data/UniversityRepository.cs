using API.Context;
using API.Models;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace API.Repository.Data
{
    
    public class UniversityRepository : GeneralRepository<MyContext, University, int>
    {
        private readonly MyContext context;
        public UniversityRepository(MyContext myContext) : base(myContext)
        {
            context = myContext;
        }
        public IEnumerable GetAllCount()
        {
            var query = from e in context.Set<Employee>()
                        join p in context.Set<Profilling>() on e.NIK equals p.NIK
                        join ed in context.Set<Education>() on p.EducationId equals ed.Id
                        join u in context.Set<University>() on ed.UniversityId equals u.Id
                        group u by new { u.Id, u.Name } into g

                        select new
                        {
                            g.Key.Name,
                            total = g.Count()
                        };
            return query.ToArray();
        }
    }
}
