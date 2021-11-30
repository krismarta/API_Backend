using API.Models;
using API.Repository.Data;

namespace API.Controllers
{
    public class RolesController : Base.BasesController<Role, RoleRepository, int>
    {
        public RolesController(RoleRepository roleRepository) : base(roleRepository)
        {

        }
    }
}
