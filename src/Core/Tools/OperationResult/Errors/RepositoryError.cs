namespace VIAEventAssociation.Core.Tools.OperationResult.Errors;

public class RepositoryError : Error
{
    public override ErrorCode Code { get; init; }
    public override string? Message { get; init; }
    private RepositoryError(ErrorCode code, string message) : base(code, message) { }
    
    public static RepositoryError ItemNotFound() => new RepositoryError(ErrorCode.ItemNotFound, "Item not found in the repository");
}