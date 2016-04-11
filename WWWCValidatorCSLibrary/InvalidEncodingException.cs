using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WWWCValidatorCSLibrary
{
    class InvalidEncodingException : Exception
    {
        public InvalidEncodingException(string message) : base(message) { }
        public InvalidEncodingException(string message, System.Exception inner) : base (message, inner) { }
        protected InvalidEncodingException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) { }
    }
}
