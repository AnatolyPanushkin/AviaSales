using System;
using System.Linq;
using AviaSales.Data;
using AviaSales.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AviaSales.GraphQL.Services
{
    public class PassengerService:IPassengerService
    {
        private readonly AviaSalesContext _context;
        private Town BadRequestResult { get; set; }
        
        
        public PassengerService(AviaSalesContext context)
        {
            _context = context;
        }
        
        public IQueryable<Passenger> GetAllPassengers()
        {
            return _context.Passengers
                .Include(t => t.Ticket)
                .ThenInclude(t=> t.ArriveTown)
                .Include(t=>t.Ticket)
                .ThenInclude(t=>t.DepartTown)
                .Select(p => p);
        }

        public Passenger GetPassengerById(int id)
        {
            return _context.Passengers
                .Include(t => t.Ticket)
                .ThenInclude(t=> t.ArriveTown)
                .Include(t=>t.Ticket)
                .ThenInclude(t=>t.DepartTown).SingleOrDefault(p => p.Id==id)!;
        }

        public Passenger UpdatePassenger(Passenger passenger, int id)
        {
            var updatingPassenger = _context.Passengers.SingleOrDefault(t=>t.Id==id);
            
            updatingPassenger.FirstName = passenger.FirstName;
            updatingPassenger.LastName = passenger.LastName;
            updatingPassenger.Id = id;
            
            _context.Passengers.Update(updatingPassenger);
            _context.SaveChanges();

            return updatingPassenger;
        }

        public Passenger AddPassenger(Passenger passenger)
        { 
            var result = _context.Passengers.Add(passenger).Entity;
            _context.SaveChanges();
            return result;
        }

        public Passenger DeletePassenger(int id)
        {
            var deletePassenger = _context.Passengers.FirstOrDefault(p => p.Id == id);
            var deleteTicket = _context.Tickets.FirstOrDefault(t => t.Id == id);

            _context.Tickets.Remove(deleteTicket);
            _context.Passengers.Remove(deletePassenger);
            
            _context.SaveChanges();

            return deletePassenger;
        }

        
        public Town CheckTown(string name)
        {
            var result = _context.Towns.FirstOrDefault(t => t.Name.Equals(name));
            return result;
            
            /*else
            {
                return BadRequestResult;
            }*/
        }

        public Ticket AddTicket(Ticket ticket)
        {
            var result = _context.Tickets.Add(ticket).Entity;
            _context.SaveChanges();
            return result;
        }
    }
}