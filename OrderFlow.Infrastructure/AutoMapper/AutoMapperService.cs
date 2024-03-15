using AutoMapper;
using OrderFlow.Domain.Interfaces.Services;

namespace OrderFlow.Infrastructure.AutoMapper;

public class AutoMapperService : IMappingService
{
    private readonly IMapper _mapper;

    public AutoMapperService(IMapper mapper)
    {
        _mapper = mapper;
    }

    public TDestination Map<TDestination>(object source)
    {
        if (source is null)
        {
            throw new Exception("Source to map can not be null");
        }

        return _mapper.Map<TDestination>(source);
    }

    public void Map<TSource, TDestination>(TSource source, TDestination destination)
    {
        _mapper.Map(source, destination);
    }

    public IEnumerable<TDestination> MapRange<TDestination>(IEnumerable<object> source)
    {
        if (source is null)
        {
            throw new Exception("Source to map can not be null");
        }

        return _mapper.Map<IEnumerable<TDestination>>(source);
    }
}