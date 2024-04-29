using System.Text.Json;
using ViaEventAssociation.Core.Tools.ObjectMapper.Interfaces;

namespace ViaEventAssociation.Core.Tools.ObjectMapper.Implementations;

public abstract class ObjectMapper (IServiceProvider serviceProvider) : IMapper
{
    public TOutput Map<TOutput>(object input)
        where TOutput : class
    {
        Type type = typeof(IMappingConfig<,>)
            .MakeGenericType(input.GetType(), typeof(TOutput));
        dynamic mappingConfig = serviceProvider.GetService(type)!;

        if (mappingConfig != null)
        {
            return mappingConfig.Map((dynamic)input);
        }

        string toJson = JsonSerializer.Serialize(input);
        return JsonSerializer.Deserialize<TOutput>(toJson)!;
    }
}
