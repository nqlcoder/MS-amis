using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.core.Exceptions
{
    public class MISAValidateException : Exception
    {
        string _messenger;
        IDictionary _errorMsg;
        public MISAValidateException(string msg, List<string> errorMsgs = null)
        {
            _messenger = msg;
            _errorMsg = new Dictionary<string, List<string>>();
            _errorMsg.Add("Employee", errorMsgs);
        }
        public override string Message => this._messenger;
        public override IDictionary Data => this._errorMsg;
    }
}
