using API.Context;
using API.Models;
using API.ViewModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;

namespace API.Repository.Data
{

    public class AccountRepository : GeneralRepository<MyContext, Account, string>
    {
        private readonly MyContext context;
        public AccountRepository(MyContext myContext) : base(myContext)
        {
            context = myContext;
        }
        public int  Login(LoginVM loginVM)
        {
            var checkEmail = context.Employees.Where(b => b.Email == loginVM.Email).FirstOrDefault();
            var checkPhone = context.Employees.Where(b => b.Phone == loginVM.Phone).FirstOrDefault();
            if (checkEmail != null || checkPhone != null)
            {
                var password = (from e in context.Set<Employee>()
                                where e.Email == loginVM.Email || e.Phone == loginVM.Phone
                                join a in context.Set<Account>() on e.Nik equals a.Nik
                                select a.Password).Single();
                var nik = (from e in context.Set<Employee>()
                                where e.Email == loginVM.Email || e.Phone == loginVM.Phone
                                join a in context.Set<Account>() on e.Nik equals a.Nik
                           select e.Nik).Single();
                var checkPassword = Hashing.Hashing.ValidatePassword(loginVM.Password, password);

                if (checkPassword == false)
                {
                    return 3;
                }
                else
                {
                    return 1;
                }

            }
            else
            {
                return 2;
            }
        }   

        public int ForgotPassword(ForgotVM forgotVM)
        {
            var checkEmail = context.Employees.Where(b => b.Email == forgotVM.Email).FirstOrDefault();
            if (checkEmail != null)
            {
                string passwordnew = Guid.NewGuid().ToString().Substring(0, 12);
                var nik = (from e in context.Set<Employee>()
                           where e.Email == forgotVM.Email
                           join a in context.Set<Account>() on e.Nik equals a.Nik
                           select e.Nik).Single();

                var original = context.Accounts.Find(nik);
                var dataEmployee = context.Employees.Find(nik);
                if (original != null)
                {
                    original.Password = Hashing.Hashing.EncrpytPassword(passwordnew);
                    context.SaveChanges();

                    System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
                    mail.To.Add(forgotVM.Email);
                    mail.From = new MailAddress("krisforgotpass@gmail.com", "Forgot Password", System.Text.Encoding.UTF8);
                    DateTimeOffset now = (DateTimeOffset)DateTime.Now;
                    mail.Subject = "RESET PASSWORD " + now;
                    mail.SubjectEncoding = System.Text.Encoding.UTF8;
                    mail.Body = "<p>hai " + dataEmployee.FirstName + dataEmployee.LastName + "</p><p>ini adalah password baru kamu " +
                        ": " + passwordnew +"</p>";
                    mail.BodyEncoding = System.Text.Encoding.UTF8;
                    mail.IsBodyHtml = true;
                    mail.Priority = MailPriority.High;
                    SmtpClient client = new SmtpClient();
                    client.Credentials = new System.Net.NetworkCredential("krisforgotpass@gmail.com", "Kris123456789");
                    client.Port = 587;
                    client.Host = "smtp.gmail.com";
                    client.EnableSsl = true;
                    try
                    {
                        client.Send(mail);
                        return 1;
                    }
                    catch (Exception)
                    {
                        return 3;
                    }
                }
            }
            return 2;
        }

        public int ChangePassword (ChangePasVM changePasVM)
        {
            var checkEmail = context.Employees.Where(b => b.Email == changePasVM.Email).FirstOrDefault();
            if (checkEmail != null)
            {
                if (changePasVM.NewPassword == changePasVM.ConfirmPassword)
                {
                    var nik = (from e in context.Set<Employee>()
                               where e.Email == changePasVM.Email
                               join a in context.Set<Account>() on e.Nik equals a.Nik
                               select e.Nik).Single();
                    var password = (from e in context.Set<Employee>()
                                    where e.Email == changePasVM.Email
                                    join a in context.Set<Account>() on e.Nik equals a.Nik
                                    select a.Password).Single();
                    var checkPassword = Hashing.Hashing.ValidatePassword(changePasVM.OldPassword,
                        password);
                    if (checkPassword == false)
                    {
                        return 4;
                    }
                    var original = context.Accounts.Find(nik);
                    if (original != null)
                    {
                        original.Password = Hashing.Hashing.EncrpytPassword(changePasVM.NewPassword);
                        context.SaveChanges();
                        return 1;
                    }
                }
                else
                {
                    return 3;
                }
            }
            return 2;
        }


        public IEnumerable GetProfile(LoginVM loginVM)
        {
            var nik = (from e in context.Set<Employee>()
                       where e.Email == loginVM.Email
                       join a in context.Set<Account>() on e.Nik equals a.Nik
                       select e.Nik).Single();

            var query = from e in context.Set<Employee>()
                        join p in context.Set<Profilling>() on nik equals p.Nik
                        join ed in context.Set<Education>() on p.EducationId equals ed.Id
                        join u in context.Set<University>() on ed.UniversityId equals u.Id
                        where nik == e.Nik

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
    }
}
