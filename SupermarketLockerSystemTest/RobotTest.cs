using System.Collections.Generic;
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
            var robot = new Robot(1, 1);
            

            Ticket ticket = robot.Store(bag);
            Assert.NotNull(ticket);
        }

        [Fact]
        public void should_return_bag_when_pick_bag_from_robot_with_ticket()
        {
            var expectedBag = new Bag();
            var robot = new Robot(1, 1);

            var ticket = robot.Store(expectedBag);

            var bag = robot.Pick(ticket);
            Assert.Equal(expectedBag, bag);
        }

        [Fact]
        public void should_return_specific_bag_when_store_bag_to_robot_for_multiple_times()
        {
            var expectedBag1 = new Bag();
            var expectedBag2 = new Bag();

            var robot = new Robot(3, 2);
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

            var robot = new Robot(2, 2);
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
            var robot = new Robot(5, 1);
            var bag = robot.Pick(new Ticket());
            Assert.Null(bag);
        }
        
        [Fact]
        public void should_pick_failed_when_given_used_ticket_to_robot()
        {
            var robot = new Robot(5, 1);
            var usedTicket = robot.Store(new Bag());
            robot.Store(new Bag());
            robot.Pick(usedTicket);

            var bagNoExisting = robot.Pick(usedTicket);

            Assert.Null(bagNoExisting);
        }
        
        [Fact]
        public void should_store_failed_when_robot_manange_no_locker()
        {
            var robot = new Robot(0, 1);
            var ticket = robot.Store(new Bag());
            Assert.Null(ticket);
        }

        [Fact]
        public void should_select_the_locker_with_most_boxes_when_store_a_bag()
        {
            var locker1 = new Locker(2);
            var locker2 = new Locker(2);
            var lockers = new List<Locker>{locker1, locker2};

            var robot = new Robot(lockers);
            
            robot.Store(new Bag());
            var ticket = robot.Store(new Bag());

            Assert.NotNull(locker2.Pick(ticket));

        }
    }
}
