using MediatR;

namespace OPM.Queries.API.Queries
{
    public interface IAsyncRequest<T>: IRequest<T>
    {
    }
}