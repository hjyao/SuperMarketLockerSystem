using System.Collections.Generic;

namespace SupermarketLockerSystem
{
    public class Locker
    {
        public int AvailableBoxesNumber { get; set; }
        public int Capacity { get; set; }
       
        public readonly Dictionary<Ticket, Bag> KeyMapping = new Dictionary<Ticket, Bag>();
        public bool IsAvailable { get; set; }

        public Locker(int capacity)
        {
            Capacity = capacity;
            AvailableBoxesNumber = capacity;
            IsAvailable = true;
        }

        public Ticket Store(Bag bag)
        {
            if (!IsAvailable)
            {
                return null;
            }

            AvailableBoxesNumber--;
            UpdateLockerIsAvailable();

            var ticket = new Ticket();

            KeyMapping.Add(ticket, bag); 
            return ticket;
        }

        public Bag Pick(Ticket ticket)
        {
            if (!KeyMapping.ContainsKey(ticket))
            {
                return null;
            }

            AvailableBoxesNumber++;
            IsAvailable = AvailableBoxesNumber > 0;

            var bag = KeyMapping[ticket];

            KeyMapping.Remove(ticket);
            return bag;
        }

        private void UpdateLockerIsAvailable()
        {
            IsAvailable = AvailableBoxesNumber > 0;
        }

        public long GetVacancy()
        {
            return AvailableBoxesNumber/Capacity;
        }
    }
}