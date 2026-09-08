using Moq;
using WeeklyAssignment.Models;
using WeeklyAssignment.Repositories.Interfaces;
using WeeklyAssignment.Services.Implementations;

namespace WeeklyAssignment.Tests.Services;

public class EmployeeServiceTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IEmployeeRepository> _employeeRepositoryMock;
    private readonly Mock<IDesignationRepository> _designationRepositoryMock;

    private readonly EmployeeService _employeeService;

    public EmployeeServiceTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _employeeRepositoryMock = new Mock<IEmployeeRepository>();
        _designationRepositoryMock = new Mock<IDesignationRepository>();

        _unitOfWorkMock.Setup(unitOfWork => unitOfWork.Employees).Returns(_employeeRepositoryMock.Object);

        _unitOfWorkMock.Setup(unitOfWork => unitOfWork.Designations).Returns(_designationRepositoryMock.Object);

        _employeeService = new EmployeeService(_unitOfWorkMock.Object);
    }

    #region GetById
    [Fact]
    public void GetById_WhenEmployeeExists_ReturnsEmployee()
    {
        var employee = new Employee
        {
            Id = 1,
            Name = "Ali Khan",
            Email = "ali.khan@example.com",
            Salary = 100000,
            JoiningDate = new DateTime(2024, 1, 1),
            DesignationId = 1
        };

        _employeeRepositoryMock.Setup(repository => repository.GetById(1)).Returns(employee);

        var result = _employeeService.GetById(1);

        Assert.NotNull(result);

        Assert.Equal(employee, result);
    }

    [Fact]
    public void GetById_WhenEmployeeDoesNotExist_ReturnsNull()
    {
        _employeeRepositoryMock.Setup(repository => repository.GetById(999)).Returns((Employee?)null);

        var result = _employeeService.GetById(999);

        Assert.Null(result);
    }

    #endregion

    #region GetEmployeeDetails
    [Fact]
    public void GetEmployeeDetails_WhenEmployeeExists_ReturnsEmployeeDetails()
    {
        // Arrange
        var employee = new Employee
        {
            Id = 1,
            Name = "Ali Khan",
            Email = "ali.khan@example.com",
            Salary = 100000,
            JoiningDate = new DateTime(2024, 1, 1),
            DesignationId = 2
        };

        var designation = new Designation
        {
            Id = 2,
            Name = "QA Engineer"
        };

        _employeeRepositoryMock.Setup(repository => repository.GetById(1)).Returns(employee);

        _designationRepositoryMock.Setup(repository => repository.GetById(2)).Returns(designation);

        // Act
        var result = _employeeService.GetEmployeeDetails(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(employee, result.Employee);
        Assert.Equal(designation, result.Designation);
    }

    [Fact]
    public void GetEmployeeDetails_WhenEmployeeDoesNotExist_ReturnsNull()
    {
        // Arrange
        _employeeRepositoryMock.Setup(repository => repository.GetById(999)).Returns((Employee?)null);

        // Act
        var result = _employeeService.GetEmployeeDetails(999);

        // Assert
        Assert.Null(result);

        _designationRepositoryMock.Verify(repository => repository.GetById(It.IsAny<int>()),Times.Never);
    }

    #endregion

    #region Create

    [Fact]
    public void Create_WhenDesignationExists_AddsEmployeeAndReturnsTrue()
    {
        // Arrange
        var employee = new Employee
        {
            Id = 1,
            Name = "Ali Khan",
            Email = "ali.khan@example.com",
            Salary = 100000,
            JoiningDate = new DateTime(2024, 1, 1),
            DesignationId = 1
        };

        var designation = new Designation
        {
            Id = 1,
            Name = "Software Engineer"
        };

        _designationRepositoryMock
            .Setup(repository => repository.GetById(1))
            .Returns(designation);

        // Act
        var result = _employeeService.Create(employee);

        // Assert
        Assert.True(result);

        _employeeRepositoryMock.Verify(
            repository => repository.Add(employee),
            Times.Once);
    }

    [Fact]
    public void Create_WhenDesignationDoesNotExist_ReturnsFalse()
    {
        // Arrange
        var employee = new Employee
        {
            Name = "Ali Khan",
            Email = "ali.khan@example.com",
            Salary = 100000,
            JoiningDate = new DateTime(2024, 1, 1),
            DesignationId = 999
        };

        _designationRepositoryMock
            .Setup(repository => repository.GetById(999))
            .Returns((Designation?)null);

        // Act
        var result = _employeeService.Create(employee);

        // Assert
        Assert.False(result);

        _employeeRepositoryMock.Verify(
            repository => repository.Add(It.IsAny<Employee>()),
            Times.Never);
    }

    #endregion

    #region Update

    [Fact]
    public void Update_WhenDesignationExists_ReturnsRepositoryResult()
    {
        // Arrange
        var employee = new Employee
        {
            Id = 1,
            Name = "Ali Updated",
            Email = "ali.updated@example.com",
            Salary = 120000,
            JoiningDate = new DateTime(2024, 1, 1),
            DesignationId = 1
        };

        var designation = new Designation
        {
            Id = 1,
            Name = "Software Engineer"
        };

        _designationRepositoryMock
            .Setup(repository => repository.GetById(1))
            .Returns(designation);

        _employeeRepositoryMock
            .Setup(repository => repository.Update(employee))
            .Returns(true);

        // Act
        var result = _employeeService.Update(employee);

        // Assert
        Assert.True(result);

        _employeeRepositoryMock.Verify(
            repository => repository.Update(employee),
            Times.Once);
    }

    [Fact]
    public void Update_WhenDesignationDoesNotExist_ReturnsFalse()
    {
        // Arrange
        var employee = new Employee
        {
            Id = 1,
            Name = "Ali Updated",
            Email = "ali.updated@example.com",
            Salary = 120000,
            JoiningDate = new DateTime(2024, 1, 1),
            DesignationId = 999
        };

        _designationRepositoryMock
            .Setup(repository => repository.GetById(999))
            .Returns((Designation?)null);

        // Act
        var result = _employeeService.Update(employee);

        // Assert
        Assert.False(result);

        _employeeRepositoryMock.Verify(
            repository => repository.Update(It.IsAny<Employee>()),
            Times.Never);
    }

    #endregion

    #region Delete

    [Fact]
    public void Delete_WhenEmployeeExists_ReturnsTrue()
    {
        // Arrange
        _employeeRepositoryMock.Setup(repository => repository.Delete(1)).Returns(true);

        // Act
        var result = _employeeService.Delete(1);

        // Assert
        Assert.True(result);

        _employeeRepositoryMock.Verify(repository => repository.Delete(1),Times.Once);
    }

    [Fact]
    public void Delete_WhenEmployeeDoesNotExist_ReturnsFalse()
    {
        // Arrange
        _employeeRepositoryMock.Setup(repository => repository.Delete(999)).Returns(false);

        // Act
        var result = _employeeService.Delete(999);

        // Assert
        Assert.False(result);

        _employeeRepositoryMock.Verify(repository => repository.Delete(999),Times.Once);
    }



    #endregion
}
