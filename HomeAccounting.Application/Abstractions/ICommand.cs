using MediatR;

namespace HomeAccounting.Application.Abstractions
{
    public interface ICommand<out TResponse> : IRequest<TResponse>
    {
    }
}
