using OwlDomain.Common.Results.Components;

namespace OwlDomain.Common.Results;

/// <summary>
///   Represents a result value.
/// </summary>
public interface IResult : IValueResult, IErrorResult { }