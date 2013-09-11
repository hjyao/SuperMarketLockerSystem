using System.Collections.Generic;
using SupermarketLockerSystem;
using Xunit;
using System.Linq;

namespace SupermarketLockerSystem
{
    public class Manager
    {
        private readonly List<Locker> lockers;
        private readonly List<SmartRobot> robots;

        public Manager(List<Locker> lockers, List<SmartRobot> robots)
        {
            this.lockers = lockers;
            this.robots = robots;
        }

        public Ticket Store(Bag bag)
        {
            if(lockers.Any(l => l.IsAvailable))
                return lockers.First(l => l.IsAvailable).Store(bag);
            if (robots.Any(r => r.IsAvailable))
                return robots.First(r => r.IsAvailable).Store(bag);
            return null;
        }

        public Bag Pick(Ticket ticket)
        {
            foreach (var bag in lockers.Select(locker => locker.Pick(ticket)).Where(bag => bag != null))
            {
                return bag;
            }
            return robots.Select(robot => robot.Pick(ticket))
                         .FirstOrDefault(b => b != null);
        }
    }
}

namespace SupermarketLockerSystemTest
{
    public class ManagerTest
    {
        [Fact]
        public void should_store_bag_in_sequence_given_all_kinds_of_robots_and_lockers()
        {
            var locker = new Locker(1);
            var lockers = new List<Locker> { locker };
            var smartRobot = new SmartRobot(new List<Locker> {new Locker(1)});
            var superRobot = new SuperRobot(new List<Locker> {new Locker(1)});

            var robots = new List<SmartRobot> {
                smartRobot,
                superRobot
            };

            var manager = new Manager(lockers, robots);
            var bag1 = new Bag();
            var bag2 = new Bag();
            var bag3 = new Bag();
            var ticket1 = manager.Store(bag1);
            var ticket2 = manager.Store(bag2);
            var ticket3 = manager.Store(bag3);

            Assert.Equal(bag1, locker.Pick(ticket1));
            Assert.Equal(bag2, smartRobot.Pick(ticket2));
            Assert.Equal(bag3, superRobot.Pick(ticket3));
        }

        [Fact]
        public void should_pick_bag_given_existing_ticket()
        {
            var locker = new Locker(1);
            var lockers = new List<Locker> { locker };
            var smartRobot = new SmartRobot(new List<Locker> {new Locker(1)});
            var superRobot = new SuperRobot(new List<Locker> {new Locker(1)});

            var robots = new List<SmartRobot> {
                smartRobot,
                superRobot
            };
            var manager = new Manager(lockers, robots);
            var expectedBag = new Bag();
            var ticket = manager.Store(expectedBag);
            var actualBag = manager.Pick(ticket);

            Assert.Equal(expectedBag, actualBag);
        }
    }
}