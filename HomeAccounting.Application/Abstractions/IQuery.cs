using MediatR;

namespace HomeAccounting.Application.Abstractions
{
    public interface IQuery<TResponse> : IRequest<TResponse>
    {
    }
}
