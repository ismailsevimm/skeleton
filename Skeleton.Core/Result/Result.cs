using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Skeleton.Core.Result
{
    public class Result<T> : IResult<T> where T : class
    {
        public T Data { get; set; }
        public int StatusCode { get; set; }

        [JsonIgnore]
        public bool IsSuccessful { get; set; }

        public ErrorDto Error { get; private set; }

        public static Result<T> Success(T data, int statusCode)
        {
            return new Result<T> { Data = data, StatusCode = statusCode, IsSuccessful = true };
        }

        public static Result<T> Success(int statusCode)
        {
            return new Result<T> { Data = default, StatusCode = statusCode, IsSuccessful = true };
        }

        public static Result<T> Fail(ErrorDto errorDto, int statusCode)
        {
            return new Result<T>
            {
                Error = errorDto,
                StatusCode = statusCode,
                IsSuccessful = false
            };
        }

        public static Result<T> Fail(string errorMessage, int statusCode, bool isShow)
        {
            var errorDto = new ErrorDto(errorMessage, isShow);

            return new Result<T> { Error = errorDto, StatusCode = statusCode, IsSuccessful = false };
        }
    }

    public class NoDataDto
    {
    }

    public class ErrorDto
    {
        public List<String> Errors { get; private set; } = new List<string>();

        public bool IsShow { get; private set; }

        public ErrorDto(string error, bool isShow)
        {
            Errors.Add(error);
            IsShow = isShow;
        }

        public ErrorDto(List<string> errors, bool isShow)
        {
            Errors = errors;
            IsShow = isShow;
        }
    }
}
