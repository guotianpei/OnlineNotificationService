using System;
using MediatR;
namespace OPM.Queries.API.Queries
{
    public abstract class QueryBase<TResult> : IAsyncRequest<TResult> where TResult :class
    { 
        
    }
}
