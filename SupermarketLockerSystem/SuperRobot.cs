using System.Collections.Generic;
using System.Linq;

namespace SupermarketLockerSystem
{
    public class SuperRobot : SmartRobot
    {
        public SuperRobot(List<Locker> lockers) : base(lockers)
        {
        }

        protected override bool GetLocker(Locker l)
        {
            return l.GetVacancy() == Lockers.Max(lo => lo.GetVacancy());
        }
    }
}