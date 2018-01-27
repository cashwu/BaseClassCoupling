using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace BaseClassCoupling
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void calculate_LessThanOneYearEmployee_Bonus()
        {
            //if my monthly salary is 1200, working year is 0.5, my bonus should be 600
            var lessThanOneYearEmployee = new FakeLessThanOneYearEmployee()
            {
                Id = 91,
                StartWorkingDate = new DateTime(2017, 7, 29)
            };

            //Console.WriteLine("your StartDate should be :{0}", DateTime.Today.AddDays(365/2*-1));
            var actual = lessThanOneYearEmployee.GetYearlyBonus();
            Assert.AreEqual(600, actual);
        }
    }

    internal class FakeLessThanOneYearEmployee : LessThanOneYearEmployee
    {
        protected override decimal GetMonthlySalary()
        {
            return 1200;
        }

        protected override void LogSalaryInfo(decimal salary)
        {
            Console.WriteLine("Salary {0} ", salary);
        }
    }

    public abstract class Employee
    {
        public DateTime StartWorkingDate { get; set; }

        protected virtual decimal GetMonthlySalary()
        {
            DebugHelper.Info($"query monthly salary id:{Id}");
            return SalaryRepo.Get(this.Id);
        }

        public abstract decimal GetYearlyBonus();

        public int Id { get; set; }
    }

    public class LessThanOneYearEmployee : Employee
    {
        public override decimal GetYearlyBonus()
        {
            var salary = this.GetMonthlySalary();
            LogSalaryInfo(salary);
            return Convert.ToDecimal(this.WorkingYear()) * salary;
        }

        protected virtual void LogSalaryInfo(decimal salary)
        {
            DebugHelper.Info($"id:{Id}, his monthly salary is:{salary}");
        }

        private double WorkingYear()
        {
            var year = Math.Round((DateTime.Today - StartWorkingDate).TotalDays / 365, 2);
            return year > 1 ? 1 : year;
        }
    }

    public class DebugHelper
    {
        public static void Info(string message)
        {
            //you can't modified this function
            throw new NotImplementedException();
        }
    }

    public class SalaryRepo
    {
        internal static decimal Get(int id)
        {
            //you can't modified this function
            throw new NotImplementedException();
        }
    }
}