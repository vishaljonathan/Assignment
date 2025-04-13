using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentInformationSystem.Exception
{
    public class InvalidStudentDataException
    {
        public InvalidStudentDataException(string message) : base(message) { }
    }
}
