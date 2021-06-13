using SelfServiceHrProject.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace SelfServiceHrProject.Data
{
    public class DbInitializer
    {
        public static void Initialize(SelfServiceHrProjectContext context)
        {
            

            

            // Look for any students.
            if (context.Employees.Any())
            {
                return;   // DB has been seeded
            }

            var paidLeaveReasons = new LeaveReasons
            {
                Name = "Paid leaves",
                Code = "PAL",
                Days = 20
            };

            var unpaidLeaveReasons = new LeaveReasons
            {
                Name = "Unpayed leaves",
                Code = "UPl",
                Days = 30
            };

            context.LeaveReasons.Add(paidLeaveReasons);
            context.LeaveReasons.Add(unpaidLeaveReasons);

            context.SaveChanges();

            var orgUnitOne = new Organisations
            {
                Name = "Developement",
                Code = "DEV"
            };

            var orgUnitTwo = new Organisations
            {
                Name = "Directors",
                Code = "DIR"
            };

            var posUnitOne = new Positions
            {
                Name = "Software developer",
                Code = "SOF_DEV"
            };

            var posUnitTwo = new Positions
            {
                Name = "Executive director",
                Code = "EXE_DIR"
            };

            var employee = new Employees
            {
                Name = "Jake",
                Surname = "Frencis",
                Family = "Harper",
                EGN = "1234567890",
                Birthdate = DateTime.ParseExact("19900118", "yyyyMMdd", CultureInfo.InvariantCulture),
                PhoneNumber = "0871234801",
                Oragnisation = orgUnitOne,
                Position = posUnitOne
            };

            var manager = new Employees
            {
                Name = "Jeff",
                Surname = "Amazon",
                Family = "Bezos",
                EGN = "7402184728",
                Birthdate = DateTime.ParseExact("19830118", "yyyyMMdd", CultureInfo.InvariantCulture),
                PhoneNumber = "0871549374",
                Oragnisation = orgUnitTwo,
                Position = posUnitTwo
            };

            var systemUserEmployee = new SystemUsers
            {
                Username = "jharper",
                Password = "123456",
                Role = 1,
                Employee = employee
            };

            var systemUserManager = new SystemUsers
            {
                Username = "manager",
                Password = "manageTeam",
                Role = 2,
                Employee = manager
            };

            var salaryEmployee = new Salary
            {
                NetSalary = 3400,
                BonusSalary = 500,
                Employee = employee
            };

            var salaryManager = new Salary
            {
                NetSalary = 15000,
                BonusSalary = 5000,
                Employee = manager
            };

            var addressEmployee = new Address
            {
                Country = "Bulgaria",
                City = "Sofia",
                LocalAddress = "Suhata reka, 85",
                Employee = employee
            };

            var addressManager = new Address
            {
                Country = "Bulgaria",
                City = "Sofia",
                LocalAddress = "West street, 17",
                Employee = manager
            };

            context.Organisations.Add(orgUnitOne);
            context.Organisations.Add(orgUnitTwo);

            context.Positions.Add(posUnitOne);
            context.Positions.Add(posUnitTwo);

            context.Employees.Add(employee);
            context.Employees.Add(manager);

            context.SystemUsers.Add(systemUserEmployee);
            context.SystemUsers.Add(systemUserManager);

            context.Salary.Add(salaryEmployee);
            context.Salary.Add(salaryManager);

            context.Address.Add(addressEmployee);
            context.Address.Add(addressManager);

            context.SaveChanges();


        }
    }
}
