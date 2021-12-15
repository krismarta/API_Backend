using API.Context;
using API.Models;
using API.ViewModel;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace API.Repository.Data
{
    public class EmployeeRepository : GeneralRepository<MyContext, Employee, string>
    {
        private readonly MyContext context;
        
        public EmployeeRepository(MyContext myContext) : base(myContext)
        {
            context = myContext;
            
        }

        public int Register(RegisterVM registerVM)
        {
            Employee e = new Employee()
            {
                Nik = registerVM.NIK,
                FirstName = registerVM.FirstName,
                LastName = registerVM.Lastname,
                Phone = registerVM.Phone,
                BirthDate = registerVM.BirthDate,
                Email = registerVM.Email,
                Salary = registerVM.Salary
            };
            var checkNik = context.Employees.Find(e.Nik);
            var checkEmail = context.Employees.Where(b => b.Email == registerVM.Email).FirstOrDefault();
            var checkPhone = context.Employees.Where(b => b.Phone == registerVM.Phone).FirstOrDefault();
            if (checkNik != null)
            {
                return 2;
            }
            else if (checkEmail != null)
            {
                return 3;
            }
            else if (checkPhone != null)
            {
                return 4;
            }
            context.Employees.Add(e);

            Account a = new Account()
            {
                Nik = registerVM.NIK,
                
                Password = Hashing.Hashing.EncrpytPassword(registerVM.Password)
            };
            context.Accounts.Add(a);


            Education ed = new Education()
            {
                Degree = registerVM.degree,
                GPA = registerVM.GPA,
                UniversityId = registerVM.universityId
            };
            context.Educations.Add(ed);
            AccountRole ar = new AccountRole()
            {
                AccountNIK = registerVM.NIK,
                RoleId = 3
            };
            context.AccountRoles.Add(ar);

            context.SaveChanges();

            Profilling p = new Profilling()
            {
                Nik = registerVM.NIK,
                EducationId = ed.Id
            };

            context.Profillings.Add(p);

            if (context.SaveChanges() >= 1)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public IEnumerable GetRegister()
        {

            var query = from e in context.Set<Employee>()
                        join p in context.Set<Profilling>() on e.Nik equals p.Nik
                        join ed in context.Set<Education>() on p.EducationId equals ed.Id
                        join u in context.Set<University>() on ed.UniversityId equals u.Id

                        select new {
                            nik = e.Nik,
                            e.FirstName,
                            e.LastName,
                            FullName = e.FirstName + " " + e.LastName,
                            gender = e.Gender == 0 ? "Male" : "Female",
                            e.Phone,
                            e.BirthDate,
                            e.Salary,
                            e.Email,
                            ed.Degree,
                            ed.GPA,
                            universityId = u.Id
                        };

            return query.ToList();
        }
        
        public IEnumerable GetProfile(string key)
        {
            var query = from e in context.Set<Employee>()
                        where e.Nik == key
                        join p in context.Set<Profilling>() on e.Nik equals p.Nik
                        join ed in context.Set<Education>() on p.EducationId equals ed.Id
                        join u in context.Set<University>() on ed.UniversityId equals u.Id
                        where key == e.Nik
                        select new
                        {
                            FullName = e.FirstName + " " + e.LastName,
                            gender = e.Gender == 0 ? "Male" : "Female",
                            e.Phone,
                            e.BirthDate,
                            e.Salary,
                            e.Email,
                            ed.Degree,
                            ed.GPA,
                            u.Name
                        };
            return query.ToList();


        }

        public int SignManager(SignManagerVM signManagerVM)
        {
            var checkRole = context.Roles.Where(b => b.RoleId == signManagerVM.id_role).FirstOrDefault();
            if (checkRole != null)
            {
                AccountRole accountRole = new AccountRole()
                {
                    AccountNIK = signManagerVM.nik,
                    RoleId = signManagerVM.id_role
                };
                context.AccountRoles.Add(accountRole);
                context.SaveChanges();
                return 1;
            }
            return 2;
        }

        public int getCountMale()
        {
            var query1 = context.Employees.Count(a => a.Gender == 0 );
            return query1;
        }

        public int getCountFemale()
        {
            var query2 = context.Employees.Where(c => c.Gender > 0).Count();
            return query2;
        }

    }
}
