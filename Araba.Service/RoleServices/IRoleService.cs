using Araba.Core.Domain.DbTables;
using System.Linq;

namespace Araba.Service.RoleServices
{
    public interface IRoleService:IGenericService<Role>
    {
        /// <summary>
        /// Rol role sahip mi.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="roleName"></param>
        /// <returns></returns>
        bool IsUserInRole(string userName, string roleName);

        /// <summary>
        /// Kullanıcıya göre roller.
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        IQueryable<Role> GetRolesByUserName(string userName);
    }
}
