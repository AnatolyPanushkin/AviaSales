using AviaSales.Data.Models;
using GraphQL.Types;

namespace AviaSales.GraphQL.GraphTypes
{
    public class PassengerGraphType:ObjectGraphType<Passenger>
    {
        public PassengerGraphType()
        {
            Name = "Passenger";
            Field(p => p.Id, true).Description("ID of passenger)");
            Field(p => p.FirstName, false);
            Field(p => p.LastName, false);
            Field(p => p.Ticket, true, typeof(TicketGraphType)).Description("Passenger's ticket");
            
        }
    }
}