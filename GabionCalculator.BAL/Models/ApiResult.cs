using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GabionCalculator.BAL.Models
{
    public class ApiResult<T>
    {
        public bool Succeeded { get; set; }
        public T Result { get; set; }
        public IEnumerable<string> Errors { get; set; }
        public int AdditNum { get; set; }
        private ApiResult()
        {
        }
        private ApiResult(bool succeeded, T result, IEnumerable<string> errors)
        {
            Succeeded = succeeded;
            Result = result;
            Errors = errors;
        }

        private ApiResult(bool succeeded, T result, IEnumerable<string> errors, int additNum)
        {
            Succeeded = succeeded;
            Result = result;
            Errors = errors;
            AdditNum = additNum;
        }

        public static ApiResult<T> Success(T result)
        {
            return new ApiResult<T>(true, result, new List<string>());
        }

        public static ApiResult<T> Failure(IEnumerable<string> errors)
        {
            return new ApiResult<T>(false, default, errors);
        }

        public static ApiResult<T> SuccessWithAdditNum(T result, int additNum)
        {
            return new ApiResult<T>(true, result, new List<string>(), additNum);
        }
    }
}
