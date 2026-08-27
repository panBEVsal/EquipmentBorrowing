using EquipmentBorrowing.Application.Services;
using EquipmentBorrowing.Domain;
using EquipmentBorrowing.Infrastructure.Repositories;

// Seed data
var students = new List<Student>
{
    new Student(1, "Juan Dela Cruz", isAllowedToBorrow: true),
    new Student(2, "Maria Santos", isAllowedToBorrow: false) // suspended student
};

var equipment = new List<Equipment>
{
    new Equipment(101, "Digital Multimeter", isAvailable: true),
    new Equipment(102, "Oscilloscope", isAvailable: false) // already borrowed
};

var studentRepository = new InMemoryStudentRepository(students);
var equipmentRepository = new InMemoryEquipmentRepository(equipment);
var borrowingRepository = new InMemoryBorrowingRepository();

var service = new BorrowEquipmentService(studentRepository, equipmentRepository, borrowingRepository);

Console.WriteLine("=== Case 1: Successful borrow ===");
var result1 = await service.ExecuteAsync(
    studentId: 1,
    equipmentId: 101,
    expectedReturnDate: DateTime.UtcNow.AddDays(7));

Console.WriteLine(result1.Success
    ? $"SUCCESS: Borrowing #{result1.Borrowing!.Id} created for student 1."
    : $"FAILED: {result1.FailureReason}");

Console.WriteLine();
Console.WriteLine("=== Case 2: Equipment unavailable ===");
var result2 = await service.ExecuteAsync(
    studentId: 1,
    equipmentId: 102,
    expectedReturnDate: DateTime.UtcNow.AddDays(7));

Console.WriteLine(result2.Success
    ? $"SUCCESS: Borrowing #{result2.Borrowing!.Id} created."
    : $"FAILED: {result2.FailureReason}");

Console.WriteLine();
Console.WriteLine("=== Case 3: Student not allowed to borrow ===");
var result3 = await service.ExecuteAsync(
    studentId: 2,
    equipmentId: 101,
    expectedReturnDate: DateTime.UtcNow.AddDays(7));

Console.WriteLine(result3.Success
    ? $"SUCCESS: Borrowing #{result3.Borrowing!.Id} created."
    : $"FAILED: {result3.FailureReason}");  
