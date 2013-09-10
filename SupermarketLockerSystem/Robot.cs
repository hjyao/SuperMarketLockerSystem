using System.Collections.Generic;
using System.Linq;

namespace SupermarketLockerSystem
{
    public class Robot
    {
        protected readonly List<Locker> Lockers;
        public Robot(List<Locker> lockers)
        {
            Lockers = lockers;
        }

        public Ticket Store(Bag bag)
        {
            var availableLocker = Lockers.Find(GetLocker);
            return availableLocker != null ? availableLocker.Store(bag) : null;
        }

        protected virtual bool GetLocker(Locker l)
        {
            return l.AvailableBoxesNumber == Lockers.Max(lo => lo.AvailableBoxesNumber);
        }

        public Bag Pick(Ticket ticket)
        {
            var locker = Lockers.Find(l => l.KeyMapping.ContainsKey(ticket));
            return locker != null ? locker.Pick(ticket) : null;
        }
    }
}