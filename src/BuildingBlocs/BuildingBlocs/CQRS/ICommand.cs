using MediatR;

namespace BuildingBlocs.CQRS
{
    //Guid represent the Guid type of the mediator 
    public interface ICommand : ICommand<Unit>
    {

    }
    public interface ICommand<out TResponse> : IRequest<TResponse>
    {
    }
}
