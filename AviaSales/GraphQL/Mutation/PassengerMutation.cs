using System;
using AviaSales.Data.Models;
using AviaSales.GraphQL.GraphTypes;
using AviaSales.GraphQL.Services;
using GraphQL;
using GraphQL.Types;

namespace AviaSales.GraphQL.Mutation
{
    public class PassengerMutation: ObjectGraphType
    {
        private readonly IPassengerService _service;
        
        public PassengerMutation(IPassengerService service)
        {
            _service = service;

            Field<PassengerGraphType>("createPassenger",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "firstName" },
                    new QueryArgument<NonNullGraphType<StringGraphType>>{Name = "lastName"},
                    new QueryArgument<NonNullGraphType<StringGraphType>>{Name = "departTown"},
                    new QueryArgument<NonNullGraphType<StringGraphType>>{Name ="arriveTown"}
                ),
                resolve: tContext =>
                {
                    var firstName = tContext.GetArgument<string>("firstName");
                    var lastName = tContext.GetArgument<string>("lastName");
                    var departTown = tContext.GetArgument<string>("departTown");
                    var arriveTown = tContext.GetArgument<string>("arriveTown");

                    var newPassenger = new Passenger()
                    {
                        FirstName = firstName,
                        LastName = lastName
                    };
                    var passenger = _service.AddPassenger(newPassenger);

                    var newTicket = new Ticket()
                    {
                        Id = passenger.Id,
                        TicketNumber = passenger.Id * 100,
                        DepartTown = _service.CheckTown(departTown),
                        ArriveTown = _service.CheckTown(arriveTown)
                    };
                    var ticket = _service.AddTicket(newTicket);


                    return new Passenger()
                    {
                        FirstName = firstName,
                        LastName = lastName,
                        Ticket = ticket
                    };

                });
        }
    }
}