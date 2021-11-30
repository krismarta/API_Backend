using API.Models;
using API.Repository.Data;

namespace API.Controllers
{
    public class AccountrolesController : Base.BasesController<AccountRole, AccountRoleRepository, int>
    {
        public AccountrolesController(AccountRoleRepository accountRoleRepository) : base(accountRoleRepository)
        {

        }
    }
}
