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
        DateTime now, min, max;

        [TestFixtureSetUp]
        public void Setup()
        {
            calc = new StandardCalculator(12, 8, 16);
            now = DateTime.Now;//localized
            min = now.Date.AddHours(17);
            max = now.Date.AddDays(1).AddHours(4);

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
    }
}
