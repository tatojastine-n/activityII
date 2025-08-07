using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace CalendarEventOrganizer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Event> events = new List<Event>();

            Console.WriteLine("Event Management System");
            Console.WriteLine("Enter at least 5 events with dates (format: EventName MM/DD/YYYY)");
            Console.WriteLine("Type 'done' when finished (minimum 5 events required)");

            while (events.Count < 5)
            {
                Console.Write($"{events.Count + 1}. ");
                string input = Console.ReadLine();

                if (input.ToLower() == "done" && events.Count >= 5)
                    break;

                string[] parts = input.Split(' ', (char)StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length >= 2 && DateTime.TryParse(parts[1], out DateTime date))
                {
                    string eventName = string.Join(" ", parts.Skip(0).Take(parts.Length - 1));

                    if (events.Any(e => e.Date == date))
                    {
                        Console.WriteLine($"Error: An event already exists on {date:MM/dd/yyyy}. Please choose a different date.");
                        continue;
                    }
                    events.Add(new Event(eventName, date));
                }
                else
                {
                    Console.WriteLine("Invalid format. Please enter as: EventName MM/DD/YYYY");
                }
            }
            var sortedEvents = events.OrderBy(e => e.Date).ToList();

            Console.WriteLine("\nUpcoming Events (Sorted by Date):");
            for (int i = 0; i < sortedEvents.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {sortedEvents[i].Name} on {sortedEvents[i].Date:MM/dd/yyyy}");
            }
        }
    }
    class Event
    {
        public string Name { get; set; }
        public DateTime Date { get; set; }

        public Event(string name, DateTime date)
        {
            Name = name;
            Date = date;
        }
    }
}
