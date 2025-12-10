

using MediatR;

namespace BuildingBlocs.CQRS
{
    public  interface IQuery<out TResponse> : IRequest<TResponse> where TResponse : notnull
    {
    }
}
