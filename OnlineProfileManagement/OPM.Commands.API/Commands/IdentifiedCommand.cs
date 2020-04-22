using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using MediatR;

namespace OPM.Commands.API.Commands
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class IdentifiedCommand<T, R> : IRequest<R>
         where T : IRequest<R>
    {
        public T Command { get; }
        public Guid Id { get; }
        public IdentifiedCommand(T command, Guid id)
        {
            Command = command;
            Id = id;
        }
    }
}
