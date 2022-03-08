using System;

namespace Uno.AspNetCore.Framework.Data
{
    public class Result
    {
        public bool Succeeded { get; set; }
        public string Error { get; set; }
        public Result()
        {
        }

        public Result(string error)
        {
            if (string.IsNullOrWhiteSpace(error))
                throw new ArgumentNullException(error);
            this.Error = error;
        }
    }
}
