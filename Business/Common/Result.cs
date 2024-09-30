using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Common
{
    public class Result
    {
        public bool IsSuccess { get; protected set; }
        public string Error { get; protected set; }

        protected Result(bool isSuccess, string error)
        {
            IsSuccess = isSuccess;
            Error = error;
        }

        public static Result Success()
        {
            return new Result(true, null);
        }

        public static Result Failure(string error)
        {
            return new Result(false, error);
        }
    }

    public class Result<T> : Result
    {
        public T Value { get; private set; }

        protected Result(bool isSuccess, string error, T value) : base(isSuccess, error)
        {
            Value = value;
        }

        public static new Result<T> Success(T value)
        {
            return new Result<T>(true, null, value);
        }

        public static new Result<T> Failure(string error)
        {
            return new Result<T>(false, error, default);
        }
    }

}
