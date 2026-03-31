using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.Core.Entities
{
    public class Application
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int JobId { get; set; }
        public string ResumePath { get; set; }
        public string Status { get; set; }
        public DateTime AppliedAt { get; set; }
    }
}
