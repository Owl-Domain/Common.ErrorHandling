Common.ErrorHandling
===

A package that contains commonly used functionality for error handling.

Test extensions (that extend the [OwlDomain.Testing](https://github.com/Owl-Domain/Testing) package)
are also provided.



## Currently implemented

### Results

Results are a simple way to return either a valid value, or an error, a very crude example of this being:
```cs
using OwlDomain.Common.Results;

// Implementation
Result<int> ParseInt(string text) 
{
   try 
   {
      int result = int.Parse(text);
      return new(result);
   }
   catch (Exception error) 
   {
      return new(error);
   }
}

// Usage
Result<int> result = ParseInt("5");
if (result.IsOk(out int value, out Exception? error))
{
   // Do something with the result value
}
else
{
   // Report the error
}
```

There are 3 different result structs that are provided, `Result`, `Result<TValue>` and `Result<TValue, TError>`.
Along with them, a plethora of useful interfaces is provided which can be used for fine-grained generic 
constraints if you need to be able to work with any of the provided result structs. Using the structs
directly is recommended in order to avoid 
[boxing](https://learn.microsoft.com/dotnet/csharp/programming-guide/types/boxing-and-unboxing).

The `Result<TValue>` struct can be seen as `Result<TValue, Exception>`, and the `Result` struct
can be seen as `Result<bool, Exception>`.


Here is a short description of all of the provided interfaces:
```cs

namespace OwlDomain.Common.Results.Components
{
   interface IValueResult { bool IsOk(); }
   interface IErrorResult { bool IsError(); }

   interface IValueResult<TValue> : IValueResult 
   {
      bool IsOk(out TValue value); 
   }
   interface IErrorResult<TError> : IErrorResult 
      where TError : notnull
   { 
      bool IsError(out TError error); 
   }
}

namespace OwlDomain.Common.Results
{
   interface IResult : IValueResult, IErrorResult { }
   interface IResult<TValue, TError> : IResult, IValueResult<TValue>, IErrorResult<TError> 
      where TError : notnull
   {
      bool IsOk(out TValue value, out TError error);
      bool IsError(out TError error, out TValue value);
   }
}
```

The results also perfectly respect the nullability of the value types that you want to use,
which will ensure that nullable analysis will work with them just as you would expect, for example:
```cs
Result<string> result = new("value");
if (result.IsOk(out string? value, out Exception? error)) 
{
    string val = value; // value is not null here
    Exception? err = error; // error is null here
}
else
{
    string? val = value; // value is null here
    Exception err = error; // error is not null here
}
```
```cs
Result<string?> result = new(value: null);
if (result.IsOk(out string? value, out Exception? error)) 
{
    string? val = value; // value may be null here
    Exception? err = error; // error is null here
}
else
{
    string? val = value; // value is null here
    Exception err = error; // error is not null here
}
```
This means that the results will still work perfectly fine when returning a null value
is an acceptable result.



## Contributions

Code contributions will not be accepted, however feel free to provide feedback / suggestions 
by creating a [new issue](https://github.com/Owl-Domain/Common.ErrorHandling/issues/new), or look at 
the [existing issues](https://github.com/Owl-Domain/Common.ErrorHandling/issues?q=) to see if your
concern / suggestion has already been raised.



## License

This project (the source, and the release files, e.t.c) are not currently under any license, 
all rights are reserved, however it will become more permissive at a later date.