using AviaSales.Data.Models;
using GraphQL.Types;

namespace AviaSales.GraphQL.GraphTypes
{
    public class TicketGraphType:ObjectGraphType<Ticket>
    {
        public TicketGraphType()
        {
            Name = "Ticket";
            Field(p => p.Id, true).Description("ID of ticket)");
            Field(p => p.TicketNumber, false);
            Field(p => p.DepartTown, false, typeof(TownGraphType)).Description("Department Town");
            Field(p => p.ArriveTown, false,typeof(TownGraphType)).Description("Arrive Town");
            Field(p => p.Passenger, true,typeof(PassengerGraphType));
        }
    }
}