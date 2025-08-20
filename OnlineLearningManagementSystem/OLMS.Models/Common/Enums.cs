using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLMS.Models.Common
{
    public enum EnrollmentStatus
    {
        Active,
        Completed,
        Cancelled
    }

    public enum UserRole
    {
        Admin = 1,
        Trainer = 2,
        Student = 3
    }
}
