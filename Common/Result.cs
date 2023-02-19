namespace Common;

public class Result
{
    public string? Error { get; set; }

    public bool IsSuccess => string.IsNullOrEmpty(Error); 
}

public class Result<T>
{
    public T? Value { get; set; }
    public string? Error { get; set; }

    public bool IsSuccess => Value != null;
}