using System;
using PetaTest;

namespace BabysitterCalculator.Tests
{
    [TestFixture]
    class MainClass
    {
        public static void Main(string[] args)
        {
            new Runner().Run(args);
        }

        ICalculator calc;
        DateTime now, min, max, bedAt8, bedAt1;

        [TestFixtureSetUp]
        public void Setup()
        {
            calc = new StandardCalculator(12, 8, 16);
            now = DateTime.Now;//localized
            min = now.Date.AddHours(17);
            max = now.Date.AddDays(1).AddHours(4);
            bedAt8 = now.Date.AddHours(20);
            bedAt1 = now.Date.AddHours(25);
        }

        [Test]
        public void Throws_if_start_time_before_5pm()
        {
            Assert.Throws<ArgumentException>(() => calc.Calculate(min.AddMinutes(-30), max, DateTime.MinValue));
        }

        [Test]
        public void Throws_if_start_time_after_4am()
        {
            Assert.Throws<ArgumentException>(() => calc.Calculate(max.AddMinutes(30), max, DateTime.MinValue));
        }

        [Test]
        public void Throws_if_end_time_after_start_time()
        {
            Assert.Throws<ArgumentException>(() => calc.Calculate(min.AddHours(2), min.AddHours(1), DateTime.MinValue));
        }

        [Test]
        public void Throws_if_end_time_before_5pm()
        {
            Assert.Throws<ArgumentException>(() => calc.Calculate(max, min.AddMinutes(-30), DateTime.MinValue));
        }

        [Test]
        public void Throws_if_bed_time_before_5pm()
        {
            Assert.Throws<ArgumentException>(() => calc.Calculate(min, max, min.AddHours(-1)));
        }

        [Test]
        public void Throws_if_bed_time_after_4am()
        {
            Assert.Throws<ArgumentException>(() => calc.Calculate(min, max, max.AddHours(1)));
        }

        [Test]
        public void Throws_if_bed_time_not_on_hour()
        {
            Assert.Throws<ArgumentException>(() => calc.Calculate(min, max, min.AddMinutes(30)));
        }

        [Test]
        public void Test_FloorHours()
        {
            Assert.AreEqual(min.AddMinutes(30).AddSeconds(30).FloorHour(), min);
        }

        [Test]
        public void Test_CeilHours()
        {
            Assert.AreEqual(min.AddMinutes(30).AddSeconds(30).CeilHour(), min.AddHours(1));
        }

        [Test]
        public void Test_earliest_start_times_midnight()
        {
            Assert.AreEqual(min.Date.AddDays(1), min.GetMidnightForShift());
        }

        [Test]
        public void Test_latest_start_times_midnight()
        {
            Assert.AreEqual(max.Date, min.GetMidnightForShift());
        }

        [Test]
        public void Throws_if_midnight_outside_of_shift()
        {
            Assert.Throws<ArgumentException>(() => min.Date.AddHours(12).GetMidnightForShift());
        }

        [Test]
        public void Test_min_charge()
        {
            Assert.AreEqual(calc.Calculate(min, min.AddMinutes(30), min), 8);
        }

        [Test]
        public void Test_max_charge()
        {
            Assert.AreEqual(calc.Calculate(min, max, max), 148);
        }

        [Test]
        public void Test_to_midnight_charge()
        {
            Assert.AreEqual(calc.Calculate(min, min.GetMidnightForShift(), min.GetMidnightForShift()), 84);
        }

        [Test]
        public void Test_from_midnight_to_4am_charge()
        {
            Assert.AreEqual(calc.Calculate(min.GetMidnightForShift(), max, max), 64);
        }

        [Test]
        public void Test_total_hours()
        {
            Assert.AreEqual(calc.GetTotalHours(min, max), 11);
        }

        [Test]
        public void Test_all_start_times_before_after_midnight()
        {
            int totalHours = calc.GetTotalHours(min, max);
            Console.WriteLine("Total hours: {0}", totalHours);
            for (int startHour = 0; startHour < totalHours; startHour++)
            {
                DateTime current = min.AddHours(startHour);
                Console.WriteLine("Validating with start hour {0}: bed time: {1}, total charge: {2}", current.Hour, bedAt8.Hour, calc.Calculate(current, max, bedAt8).ToString("C0"));
                Console.WriteLine("Validating with start hour {0}: bed time: {1}, total charge: {2}", current.Hour, bedAt1.Hour, calc.Calculate(current, max, bedAt1).ToString("C0"));
            }
        }
    }
}
