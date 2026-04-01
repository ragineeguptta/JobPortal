using JobPortal.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.Core.Interfaces
{
    public interface IAuthRepository
    {
        Task<User> RegisterAsync(User user);
        Task<User?> GetUserByEmailAsync(string email);
    }
}
