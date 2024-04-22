namespace VIAEventAssociation.Core.QueryContracts.Contract;

public interface IQueryHandler<in TQuery, TAnswer> where TQuery : IQuery<TAnswer>
{
    
}