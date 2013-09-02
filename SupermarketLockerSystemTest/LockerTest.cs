using SupermarketLockerSystem;
using Xunit;

namespace SupermarketLockerSystemTest
{
    public class LockerTest
    {
        [Fact]
        public void should_return_ticket_after_store_a_bag_into_locker()
        {
            var locker = new Locker(1);
            var bag = new Bag();

            Ticket ticket = locker.Store(bag);
            Assert.NotNull(ticket);
        }

        [Fact]
        public void should_return_null_when_locker_is_full()
        {
            var locker = new Locker(1);
            locker.Store(new Bag());

            var bag = new Bag();
            var ticket = locker.Store(bag);
            Assert.Null(ticket);
        }

        [Fact]
        public void should_be_able_to_store_multiple_bags_in_one_locker()
        {
            var locker = new Locker(10);
            locker.Store(new Bag());
            Assert.NotNull(locker.Store(new Bag()));
        }

        [Fact]
        public void should_return_ticket_after_store_nothing_into_locker()
        {
            var locker = new Locker(1);

            Ticket ticket = locker.Store(null);
            Assert.NotNull(ticket);
        }

        [Fact]
        public void should_pick_nothing_with_ticket_if_stored_nothing()
        {
            var locker = new Locker(1);

            var ticket = locker.Store(null);
            var bag = locker.Pick(ticket);
            Assert.Null(bag);
        }

        [Fact]
        public void should_pick_the_bag_stored_in_locker_with_ticket()
        {
            var locker = new Locker(2);
            var expectedBag1 = new Bag();
            var expectedBag2 = new Bag();
            var ticket1 = locker.Store(expectedBag1);
            var ticket2 = locker.Store(expectedBag2);

            var bag1 = locker.Pick(ticket1);
            var bag2 = locker.Pick(ticket2);
            Assert.Equal(expectedBag1, bag1);
            Assert.Equal(expectedBag2, bag2);
        }


        [Fact]
        public void should_failed_if_pick_with_not_matched_ticket()
        {
            var locker = new Locker(1);
            locker.Store(new Bag());

            var bag = locker.Pick(new Ticket());
            Assert.Null(bag);
        }

        [Fact]
        public void should_be_able_to_store_after_pick_from_full_locker()
        {
            var locker = new Locker(1);
            var ticket = locker.Store(new Bag());
            locker.Pick(ticket);
            Ticket ticket2 = locker.Store(new Bag());
            Assert.NotNull(ticket2);
        }
    }
}
