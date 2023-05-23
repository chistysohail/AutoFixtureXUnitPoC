using System;
using AutoFixture;
using AutoFixture.Xunit2;
using Xunit;


namespace TestAutoFixtureInXUnitPoC
{
    public class ExmployeetWithAutoFixtureTest
    {
        [Theory]
        [InlineData("Sachin", "Ramesh", "Tendulkar", "Sachin Ramesh Tendulkar")]
        [InlineData("Anjali", "Ramesh", "Tendulkar", "Anjali Ramesh Tendulkar")]
        public void FullName_By_MultipleInlineData(
            string firstName,
            string middleName,
            string lastName,
            string expected)
        {
            var fixture = new Fixture();
            //AutoFixture
            var sut = fixture.Build<Employee>()
                .With(a => a.FirstName, firstName)
                .With(a => a.MiddleName, middleName)
                .With(a => a.LastName, lastName)
                .Create();

            var actual = sut.FullName;

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineAutoData("Sachin", "Ramesh", "Tendulkar", "Sachin Ramesh Tendulkar")]
        [InlineAutoData("Anjali", "Ramesh", "Tendulkar", "Anjali Ramesh Tendulkar")]
        public void FullName_By_AutoDataAttribute(
            string firstName,
            string middleName,
            string lastName,
            string expected,
            Employee sut
        )
        {
            sut.FirstName = firstName;
            sut.LastName = lastName;
            sut.MiddleName = middleName;
            var actual = sut.FullName;

            Assert.Equal(expected, actual);
        }
    }

    public class Department
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
    public class Employee
    {

        public Employee(
            string firstName,
            string lastName,
            int employeeNumber,
            int birthDay,
            string middleName)
        {
            FirstName = firstName ?? throw new ArgumentNullException(nameof(firstName));
            LastName = lastName ?? throw new ArgumentNullException(nameof(lastName));
            EmployeeNumber = employeeNumber;
            BirthDay = birthDay;
            MiddleName = middleName ?? throw new ArgumentNullException(nameof(middleName));
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public int EmployeeNumber { get; set; }
        public int BirthDay { get; set; }
        public string FullName
        {
            get
            {
                return $"{FirstName} {MiddleName} {LastName}";
            }
        }

    }
}