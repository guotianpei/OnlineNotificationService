using System;
using MediatR;
namespace OPM.Queries.API.Queries
{
    public abstract class QueryBase<TResult> : IRequest<TResult> where TResult :class
    { 
        
    }
}
