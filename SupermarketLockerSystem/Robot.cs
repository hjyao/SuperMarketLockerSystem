using System.Collections.Generic;
using System.Linq;

namespace SupermarketLockerSystem
{
    public class Robot
    {
        private readonly List<Locker> lockers;
        private readonly int boxesNumber;

        public Robot(int capacity, int boxesNumber)
        {
            this.boxesNumber = boxesNumber;
            lockers = new List<Locker>(capacity);
            for (var i = 1; i <= capacity; i++)
            {
                lockers.Add(new Locker(this.boxesNumber));
            }
        }

        public Robot(List<Locker> lockers)
        {
            this.lockers = lockers;
        }

        public Ticket Store(Bag bag)
        {
            var availableLocker = lockers.Find(l => l.AvailableBoxesNumber == lockers.Max(lo => lo.AvailableBoxesNumber));
            return availableLocker != null ? availableLocker.Store(bag) : null;
        }

        public Bag Pick(Ticket ticket)
        {
            var locker = lockers.Find(l => l.KeyMapping.ContainsKey(ticket));
            return locker != null ? locker.Pick(ticket) : null;
        }
    }
}