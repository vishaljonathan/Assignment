using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalAssetManagementApplication.MyExceptions
{
    public class Exceptions : Exception
    {
        public class AssetNotFoundException : Exception
        {
            public AssetNotFoundException(string message) : base(message) { }
        }
        public class AssetNotMaintainException : Exception
        {
            public AssetNotMaintainException(string message) : base(message) { }
        }
    }
}
