using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace FreeCourse.Shared.Dtos
{
    public class ResponseDto<T>
    {
        public T Data { get; private set; }

        [JsonIgnore]
        public int StatusCode { get; private set; }
        [JsonIgnore]
        public bool IsSuccessful { get; private set; }
        public List<string> Errors { get; private set; }

        //static factory method
        public static ResponseDto<T> Success(T data, int statusCode)
        {
            return new ResponseDto<T> { StatusCode = statusCode, Data = data, IsSuccessful = true };
        }

        public static ResponseDto<T> Success(int statusCode)
        {
            return new ResponseDto<T> { Data = default, StatusCode = statusCode, IsSuccessful = true };
        }

        public static ResponseDto<T> Fail(string error, int statusCode)
        {
            List<string> errors = new List<string> { error };
            return new ResponseDto<T> { Errors = errors, StatusCode = statusCode, IsSuccessful = false };
        }

        public static ResponseDto<T> Fail(List<string> errors, int statusCode)
        {
            return new ResponseDto<T> { Errors = errors, StatusCode = statusCode, IsSuccessful = false };
        }
    }
}
