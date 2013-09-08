using System.Collections.Generic;
using System.Linq;
using SupermarketLockerSystem;
using Xunit;

namespace SupermarketLockerSystemTest
{
    public class SuperRobotTest
    {
        [Fact]
        public void should_return_ticket_when_store_bag()
        {
            var bag = new Bag();
            var superRobot = new SuperRobot(1, 1);
            Ticket ticket = superRobot.Store(bag);
            Assert.NotNull(ticket);
        }

        [Fact]
        public void should_pick_bag_with_ticket()
        {
            var expectedBag = new Bag();
            var superRobot = new SuperRobot(1,1);
            var ticket = superRobot.Store(expectedBag);
            var bag = superRobot.Pick(ticket);
            Assert.Equal(expectedBag, bag);
        }

        [Fact]
        public void should_store_bag_in_max_vacancy_locker()
        {
            var locker1 = new Locker(2);
            var locker2 = new Locker(1);
            var lockers = new List<Locker> {locker1, locker2};
            locker1.Store(new Bag());

            var superRobot = new SuperRobot(lockers);
            superRobot.Store(new Bag());
            Assert.True(!locker2.IsAvailable);
        }
    }

    public class SuperRobot : Robot
    {
        public SuperRobot(int capacity, int boxesNumber) : base(capacity, boxesNumber)
        {
        }

        public SuperRobot(List<Locker> lockers) : base(lockers)
        {
        }

        public new Ticket Store(Bag bag)
        {
            var availableLocker = Lockers.Find(l => l.GetVacancy() == Lockers.Max(lo => lo.GetVacancy()));
            return availableLocker != null ? availableLocker.Store(bag) : null;
        }

    }
}
