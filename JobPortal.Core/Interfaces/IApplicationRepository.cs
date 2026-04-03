using JobPortal.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.Core.Interfaces
{
    public interface IApplicationRepository
    {
        Task ApplyAsync(Application app);
    }
}
