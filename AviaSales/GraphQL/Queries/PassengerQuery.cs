using System.Collections.Generic;
using System.Linq;
using AviaSales.Data.Models;
using AviaSales.GraphQL.GraphTypes;
using AviaSales.GraphQL.Services;
using GraphQL;
using GraphQL.Types;

namespace AviaSales.GraphQL.Queries
{
    public class PassengerQuery:ObjectGraphType
    {
        private readonly IPassengerService _service;
        
        public PassengerQuery(IPassengerService service)
        {
            _service = service;
            
            Field<ListGraphType<PassengerGraphType>>("passengers", "Query for take all passengers",
                resolve: GetAllPassengers);
            Field<PassengerGraphType>("passenger", "Query for take passenger",
                new QueryArguments(MakeNonNullStringArgument("id", "ID passenger")),
                resolve: GetPassenger);

            // with pagination
            Field<ListGraphType<PassengerGraphType>>("passengersPag", "Query fir take all passengers",
                new QueryArguments(MakeNonNullStringArgument("index", "Number of begining page"),
                    MakeNonNullStringArgument("count", "How much elements to display")),
                resolve: GetAllPassengersPag);

        }
        
        private IEnumerable<Passenger> GetAllPassengersPag(IResolveFieldContext<object?> arg)
        {
            var index = int.Parse(arg.GetArgument<string>("index"));
            var count = int.Parse(arg.GetArgument<string>("count"));

            var items = _service.GetAllPassengers()
                .Skip(index)
                .Take(count).ToList();
            return items;
        }
        private QueryArgument MakeNonNullStringArgument(string name, string description) {
            return new QueryArgument<NonNullGraphType<StringGraphType>> {
                Name = name, Description = description
            };
        }

        private IEnumerable<Passenger> GetAllPassengers(IResolveFieldContext<object?> arg)
        {
            return _service.GetAllPassengers();
        }
    
        private Passenger GetPassenger(IResolveFieldContext<object?> arg)
        {
            return _service.GetPassengerById(int.Parse(arg.GetArgument<string>("id")));
        }
        
    }
}