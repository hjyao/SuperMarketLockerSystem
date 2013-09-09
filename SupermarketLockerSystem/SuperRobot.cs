using System.Collections.Generic;
using System.Linq;

namespace SupermarketLockerSystem
{
    public class SuperRobot : Robot
    {
        public SuperRobot(List<Locker> lockers) : base(lockers)
        {
        }

        public new Ticket Store(Bag bag)
        {
            return StoreWithRule(bag, l => l.GetVacancy() == Lockers.Max(lo => lo.GetVacancy()));
        }

    }
}