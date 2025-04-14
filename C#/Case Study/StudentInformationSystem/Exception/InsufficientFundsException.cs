﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentInformationSystem.Exception
{
    public class InsufficientFundsException: System.Exception
    {
        public InsufficientFundsException(string message) : base(message) { }
    }
}
