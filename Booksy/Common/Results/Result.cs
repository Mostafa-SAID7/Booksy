namespace Booksy.Common.Results;

/// <summary>
/// Standard API response wrapper for consistency
/// </summary>
public class Result
{
    public bool Success { get; set; }
    public string? Message { get; set; }
    public List<Error>? Errors { get; set; }

    public Result() { }

    public Result(bool success, string? message = null, List<Error>? errors = null)
    {
        Success = success;
        Message = message;
        Errors = errors ?? new List<Error>();
    }

    /// <summary>
    /// Creates a successful result
    /// </summary>
    public static Result Ok(string? message = "Operation completed successfully")
        => new(true, message);

    /// <summary>
    /// Creates a failed result
    /// </summary>
    public static Result Fail(string message, List<Error>? errors = null)
        => new(false, message, errors);

    /// <summary>
    /// Creates a failed result with errors
    /// </summary>
    public static Result Fail(params Error[] errors)
        => new(false, "Operation failed", errors.ToList());
}

/// <summary>
/// Generic API response wrapper with data
/// </summary>
public class Result<T> : Result
{
    public T? Data { get; set; }

    public Result() { }

    public Result(T? data, bool success, string? message = null, List<Error>? errors = null)
        : base(success, message, errors)
    {
        Data = data;
    }

    /// <summary>
    /// Creates a successful result with data
    /// </summary>
    public static Result<T> Ok(T data, string? message = "Operation completed successfully")
        => new(data, true, message);

    /// <summary>
    /// Creates a failed result
    /// </summary>
    public new static Result<T> Fail(string message, List<Error>? errors = null)
        => new(default, false, message, errors);

    /// <summary>
    /// Creates a failed result with errors
    /// </summary>
    public new static Result<T> Fail(params Error[] errors)
        => new(default, false, "Operation failed", errors.ToList());
}

/// <summary>
/// Error details in a result
/// </summary>
public class Error
{
    public string Code { get; set; }
    public string Message { get; set; }

    public Error(string code, string message)
    {
        Code = code;
        Message = message;
    }

    public Error(string message) : this("ERROR", message) { }
}
