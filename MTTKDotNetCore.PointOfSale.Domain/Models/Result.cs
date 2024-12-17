using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTTKDotNetCore.PointOfSale.Domain.Models;

public class Result<T>
{
    public bool IsSuccess { get; set; }
    public bool IsError { get { return !IsSuccess; } }
    public bool IsValidationError { get { return Type == EnumRespType.ValidationError; } }
    public bool IsSystemError   { get { return Type == EnumRespType.SystemError; } }
    public bool IsNotFound { get { return Type == EnumRespType.NotFound;  } }
    private EnumRespType Type { get; set; }
    public T? Data { get; set; }
    public string Message { get; set; } = null!;

    public static Result<T> Success(T? data, string message)
    {
        return new Result<T>
        {
            IsSuccess = true,
            Data = data,
            Type = EnumRespType.Success,
            Message = message
        };
    }

    public static Result<T> ValidationError(string message, T? data = default)
    {
        return new Result<T>
        {
            IsSuccess = false,
            Data = data,
            Type = EnumRespType.ValidationError,
            Message = message
        };
    }

    public static Result<T> SystemError(string message, T? data = default)
    {
        return new Result<T>
        {
            IsSuccess = false,
            Data = data,
            Type = EnumRespType.SystemError,
            Message = message
        };
    }
    
    public static Result<T> NotFound(string message, T? data = default)
    {
        return new Result<T>
        {
            IsSuccess = false,
            Data = data,
            Type = EnumRespType.NotFound,
            Message = message
        };
    }
}

public enum EnumRespType
{
    None,
    Success,
    ValidationError,
    SystemError,
    NotFound
}


