using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentInformationSystem.Exception
{
    public class InvalidCourseDataException: System.Exception
    {
        public InvalidCourseDataException(string message) : base(message) { }
    }
}
