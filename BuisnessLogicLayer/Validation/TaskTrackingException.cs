using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BuisnessLogicLayer.Validation
{
    [Serializable]
    public class TaskTrackingException : Exception
    {
        public TaskTrackingException() : base() { }

        public TaskTrackingException(string message) : base(message) { }

        public TaskTrackingException(string message, Exception innerException) : base(message, innerException) { }

        protected TaskTrackingException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
