﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentInformationSystem.Exception
{
    public class DuplicateEnrollmentException : System.Exception
    {
        public DuplicateEnrollmentException(string message) : base(message) { }
    }
}
