using System.Collections.Generic;
using System.Linq;
using SupermarketLockerSystem;
using Xunit;

namespace SupermarketLockerSystemTest
{
    public class RobotTest
    {
        
        [Fact]
        public void should_return_ticket_after_store_a_bag_by_robot()
        {
            var bag = new Bag();
            var lockers = Enumerable.Range(0, 1).Select(i => new Locker(1)).ToList();
            var robot = new SmartRobot(lockers);
            

            Ticket ticket = robot.Store(bag);
            Assert.NotNull(ticket);
        }

        [Fact]
        public void should_return_bag_when_pick_bag_from_robot_with_ticket()
        {
            var expectedBag = new Bag();
            var lockers = Enumerable.Range(0, 1).Select(i => new Locker(1)).ToList();
            var robot = new SmartRobot(lockers);

            var ticket = robot.Store(expectedBag);

            var bag = robot.Pick(ticket);
            Assert.Equal(expectedBag, bag);
        }

        [Fact]
        public void should_return_specific_bag_when_store_bag_to_robot_for_multiple_times()
        {
            var expectedBag1 = new Bag();
            var expectedBag2 = new Bag();

            var lockers = Enumerable.Range(0, 3).Select(i => new Locker(2)).ToList();
            var robot = new SmartRobot(lockers);
            var ticket1 = robot.Store(expectedBag1);
            robot.Store(expectedBag2);

            var bag1 = robot.Pick(ticket1);
            Assert.Equal(expectedBag1, bag1);

        }

        [Fact]
        public void should_store_failed_when_lockers_robot_managed_has_been_all_full()
        {
            var bag1 = new Bag();
            var bag2 = new Bag();
            var bag3 = new Bag();
            var bag4 = new Bag();
            var bag5 = new Bag();

            var lockers = Enumerable.Range(0, 2).Select(i => new Locker(2)).ToList();
            var robot = new SmartRobot(lockers);
            robot.Store(bag1);
            robot.Store(bag2);
            robot.Store(bag3);
            robot.Store(bag4);
            var ticket = robot.Store(bag5);
            Assert.Null(ticket);
        }

        [Fact]
        public void should_pick_failed_when_given_no_existing_ticket_to_robot()
        {
            var lockers = Enumerable.Range(0, 5).Select(i => new Locker(1)).ToList();
            var robot = new SmartRobot(lockers);
            var bag = robot.Pick(new Ticket());
            Assert.Null(bag);
        }
        
        [Fact]
        public void should_pick_failed_when_given_used_ticket_to_robot()
        {
            var lockers = Enumerable.Range(0, 5).Select(i => new Locker(1)).ToList();
            var robot = new SmartRobot(lockers);
            var usedTicket = robot.Store(new Bag());
            robot.Store(new Bag());
            robot.Pick(usedTicket);

            var bagNoExisting = robot.Pick(usedTicket);

            Assert.Null(bagNoExisting);
        }
        
        [Fact]
        public void should_store_failed_when_robot_manange_no_locker()
        {
            var lockers = Enumerable.Range(0, 0).Select(i => new Locker(1)).ToList();
            var robot = new SmartRobot(lockers);
            var ticket = robot.Store(new Bag());
            Assert.Null(ticket);
        }

        [Fact]
        public void should_select_the_locker_with_most_boxes_when_store_a_bag()
        {
            var locker1 = new Locker(2);
            var locker2 = new Locker(2);
            var lockers = new List<Locker>{locker1, locker2};

            var robot = new SmartRobot(lockers);
            
            robot.Store(new Bag());
            var ticket = robot.Store(new Bag());

            Assert.NotNull(locker2.Pick(ticket));

        }
    }
}
