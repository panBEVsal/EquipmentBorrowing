using EquipmentBorrowing.Application.Services;
using EquipmentBorrowing.Domain;
using EquipmentBorrowing.Infrastructure.Repositories;
using Xunit;

namespace EquipmentBorrowing.Tests;

public class BorrowEquipmentServiceTests
{
    [Fact]
    public async Task Borrow_Succeeds_When_All_Rules_Satisfied()
    {
        var students = new List<Student> { new Student(1, "Test Student") };
        var equipment = new List<Equipment> { new Equipment(101, "Drill") };

        var service = new BorrowEquipmentService(
            new InMemoryStudentRepository(students),
            new InMemoryEquipmentRepository(equipment),
            new InMemoryBorrowingRepository());

        var result = await service.ExecuteAsync(1, 101, DateTime.UtcNow.AddDays(3));

        Assert.True(result.Success);
        Assert.NotNull(result.Borrowing);
    }

    [Fact]
    public async Task Borrow_Fails_When_Equipment_Unavailable()
    {
        var students = new List<Student> { new Student(1, "Test Student") };
        var equipment = new List<Equipment> { new Equipment(101, "Drill", isAvailable: false) };

        var service = new BorrowEquipmentService(
            new InMemoryStudentRepository(students),
            new InMemoryEquipmentRepository(equipment),
            new InMemoryBorrowingRepository());

        var result = await service.ExecuteAsync(1, 101, DateTime.UtcNow.AddDays(3));

        Assert.False(result.Success);
        Assert.Equal("Equipment is not available.", result.FailureReason);
    }
}