using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
    public class Result : IResult
    {
      
        public Result(bool success, string message):this(success)
        {
            Message = message;
        }

        public Result(bool success)
        {
            Success = success;
        }

        // aşağıdakiler sadece get methodunu içerdikleri için read onlydirler. Ancak bunlar constructor ile set edilebilir.
        public bool Success { get; }

        public string Message { get; }
    }
}
