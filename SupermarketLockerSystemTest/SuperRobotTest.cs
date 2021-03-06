﻿using System.Collections.Generic;
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
            var superRobot = new SuperRobot(new List<Locker> { new Locker(1) });
            Ticket ticket = superRobot.Store(bag);
            Assert.NotNull(ticket);
        }

        [Fact]
        public void should_pick_bag_with_ticket()
        {
            var expectedBag = new Bag();
            var superRobot = new SuperRobot(new List<Locker>{new Locker(1)});
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
            Assert.False(locker2.IsAvailable);
        }
    }
}
