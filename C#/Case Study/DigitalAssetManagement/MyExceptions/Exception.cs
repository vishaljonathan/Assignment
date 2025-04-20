using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalAssetManagement.MyExceptions
{
    public class Exception : System.Exception
    {
        public class AssetNotFoundException : System.Exception
        {
            public AssetNotFoundException()
                : base("The asset ID entered does not exist.") { }

            public AssetNotFoundException(string message)
                : base(message) { }
        }
        public class AssetNotMaintainException : System.Exception
        {
            public AssetNotMaintainException()
                : base("The asset has not been maintained for over 2 years.") { }

            public AssetNotMaintainException(string message)
                : base(message) { }
        }

    }
}
