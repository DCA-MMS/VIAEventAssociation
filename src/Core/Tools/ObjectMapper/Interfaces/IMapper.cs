namespace ViaEventAssociation.Core.Tools.ObjectMapper.Interfaces;

public interface IMapper
{
    TOutput Map<TOutput>(object input) where TOutput : class;
}