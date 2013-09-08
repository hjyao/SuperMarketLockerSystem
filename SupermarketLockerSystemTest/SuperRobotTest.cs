using System.Collections.Generic;
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

    }

    public class SuperRobot : Robot
    {
        public SuperRobot(int capacity, int boxesNumber) : base(capacity, boxesNumber)
        {
        }

        public SuperRobot(List<Locker> lockers) : base(lockers)
        {
        }
    }
}
