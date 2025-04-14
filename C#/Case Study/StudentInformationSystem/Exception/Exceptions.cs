using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentInformationSystem.Exception
{
    class Exceptions
    {
        public class CourseNotFoundException : System.Exception
        {
            public CourseNotFoundException(string message) : base(message) { }
        }
        public class DuplicateEnrollmentException : System.Exception
        {
            public DuplicateEnrollmentException(string message) : base(message) { }
        }
        public class InsufficientFundsException : System.Exception
        {
            public InsufficientFundsException(string message) : base(message) { }
        }
        public class InvalidCourseDataException : System.Exception
        {
            public InvalidCourseDataException(string message) : base(message) { }
        }
        public class InvalidEnrollmentDataException : System.Exception
        {
            public InvalidEnrollmentDataException(string message) : base(message) { }
        }
        public class InvalidStudentDataException : System.Exception
        {
            public InvalidStudentDataException(string message) : base(message) { }
        }
        public class InvalidTeacherDataException : System.Exception
        {
            public InvalidTeacherDataException(string message) : base(message) { }
        }
        public class PaymentValidationException : System.Exception
        {
            public PaymentValidationException(string message) : base(message) { }
        }
        public class StudentNotFoundException : System.Exception
        {
            public StudentNotFoundException(string message) : base(message) { }
        }
        public class TeacherNotFoundException : System.Exception
        {
            public TeacherNotFoundException(string message) : base(message) { }
        }
    }
}
