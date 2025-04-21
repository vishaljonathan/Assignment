using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagementSystem.Exceptions
{
    class Exception: System.Exception
    {
        public class UserNotFoundException : System.Exception
        {
            public UserNotFoundException()
                : base("The user ID entered does not exist.") { }

            public UserNotFoundException(string message) : base(message) { }
        }
        public class OrderNotFoundException : System.Exception
        {
            public OrderNotFoundException()
                : base("The order ID entered does not exist.") { }
            public OrderNotFoundException(string message) : base(message) { }
        }
    }
}
