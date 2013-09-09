using System;
using System.Collections.Generic;
using System.Linq;

namespace SupermarketLockerSystem
{
    public class Robot
    {
        protected readonly List<Locker> Lockers;
        protected readonly int BoxesNumber;

        public Robot(int capacity, int boxesNumber)
        {
            BoxesNumber = boxesNumber;
            Lockers = new List<Locker>(capacity);
            for (var i = 1; i <= capacity; i++)
            {
                Lockers.Add(new Locker(BoxesNumber));
            }
        }

        public Robot(List<Locker> lockers)
        {
            Lockers = lockers;
        }

        public Ticket Store(Bag bag)
        {
            return StoreWithRule(bag, l => l.AvailableBoxesNumber == Lockers.Max(lo => lo.AvailableBoxesNumber));
        }

        protected Ticket StoreWithRule(Bag bag, Predicate<Locker> action)
        {
            var availableLocker = Lockers.Find(action);
            return availableLocker != null ? availableLocker.Store(bag) : null;
        }

        public Bag Pick(Ticket ticket)
        {
            var locker = Lockers.Find(l => l.KeyMapping.ContainsKey(ticket));
            return locker != null ? locker.Pick(ticket) : null;
        }
    }
}