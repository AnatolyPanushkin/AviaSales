using System;
using System.Linq;
using AviaSales.Data.Models;

namespace AviaSales.GraphQL.Services
{
    public interface IPassengerService
    {
        IQueryable<Passenger> GetAllPassengers();
        
        Passenger GetPassengerById(int id);

        Passenger UpdatePassenger(Passenger passenger, int id);

        Passenger AddPassenger(Passenger passenger);

        Passenger DeletePassenger(Passenger passenger);

        Town CheckTown(string name);

        Ticket AddTicket(Ticket ticket);
    }
}