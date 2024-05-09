using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson10.Tests
{
    public class MainWindowViewModelTests
    {
        [Fact]
        public void CommandShouldIncreaseEachTimeByOne()
        {
            var viewModel = new MainWindowViewModel();
            int current = viewModel.Counter;

            viewModel.ButtonClickedCommand.Execute(null);

            Assert.Equal(current + 1, viewModel.Counter);
        }

        [Fact]
        public void ShouldCalculateCorrectlyTotalSalary()
        {
            var employees = new List<Employee>
            {
                new Employee
                {
                    Id = 1,
                    Salary = 500,
                },
                new Employee
                {
                    Id = 1,
                    Salary = 500,
                }
            };

            var viewModel = new MainWindowViewModel();

            decimal result = viewModel.CalculateTotalSalary(employees);

            Assert.Equal(950, result);
        }

        [Fact]
        public void ShouldCalculateCorrectlyTotalSalaryWithNegative()
        {
            var employees = new List<Employee>
            {
                new Employee
                {
                    Id = 1,
                    Salary = -500,
                },
                new Employee
                {
                    Id = 1,
                    Salary = 500,
                }
            };

            var viewModel = new MainWindowViewModel();

            decimal result = viewModel.CalculateTotalSalary(employees);

            Assert.Equal(475, result);
        }

        [Fact]
        public void ShouldNotFailWithNull()
        {
            var viewModel = new MainWindowViewModel();

            decimal result = viewModel.CalculateTotalSalary(null);

            Assert.Equal(0, result);
        }
    }
}
