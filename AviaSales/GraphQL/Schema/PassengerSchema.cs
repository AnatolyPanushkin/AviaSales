using AutoMapper;
using GraphQL.Types;
using AviaSales.Data;
using AviaSales.GraphQL.Mutation;
using AviaSales.GraphQL.Queries;
using AviaSales.GraphQL.Services;


namespace AviaSales.GraphQL.Schema
{
    public class PassengerSchema: global::GraphQL.Types.Schema
    {
        public PassengerSchema(IPassengerService service)
        {
            Query = new PassengerQuery(service);
            Mutation = new PassengerMutation(service);
        }
    }
}