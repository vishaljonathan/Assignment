using System;

namespace OrderManagementSystem.Exceptions
{
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException()
            : base("The user ID entered does not exist.") { }

        public UserNotFoundException(string message) : base(message) { }
    }

    public class OrderNotFoundException : Exception
    {
        public OrderNotFoundException()
            : base("The order ID entered does not exist.") { }

        public OrderNotFoundException(string message) : base(message) { }
    }
}
