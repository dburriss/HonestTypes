using System;
using System.Collections.Generic;
using System.Text;

namespace DemoHonestTypes
{
    public class Result<T>
    {
        public T Value { get; set; }
        public bool IsSuccess { get; set; }
        public IEnumerable<string> Errors { get; set; }
        public Result()
        {
            Errors = new List<string>();
        }

        public Result(T value)
        {
            if(value == null)
            {
                IsSuccess = false;
            }
            else
            {
                IsSuccess = true;
                Value = value;
            }
        }
    }
}
