using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.Core.DTO
{
    public class ApplyJobDto
    {
        public int UserId { get; set; }
        public int JobId { get; set; }
        public string ResumePath { get; set; }
    }
}
