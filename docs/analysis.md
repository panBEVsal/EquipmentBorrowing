Part A - Analysis
A. Actor(s)
Student
  - students wants to request for an equipment to borrow
  - check if the equipment is available
  - return equipment when done borrowing
B. Use Cases

Item | Description
Use Case 1: Borrow Equipment
Primary Actor: Student
Preconditions: Student is registered and equipment record exists
Main Action: Student requests to borrow an equipment
Expected Result: A new borrowing record is created with a status Active and equipment becomes unavailable
Possible failure: Student is not allowed to borrow, the equipment doesn't exist, or max active number of borrowing reached

Use Case 2: Return Equipment
Primary Actor: Student
Preconditions: An active borrowing record exists
Main Action: Student returns borrowed equipment
Expected Result: Borrowing status changes to Returned and equipment becomes available again
Possible Failure: No active borrowing record exists 

Use Case 3: Find Available Equipment
Primary Actor: Student
Preconditions: Equipment record exists in the system
Main Action: Student requests a list of currently available equipment
Expected Result: System returns the list of currently available equipment
Possible Failure: No equipment currently available
C. Domain Concepts

Student
  1. Id, Name, IsAllowedToBorrow
  2. can change its own eligibility
  3. not responsible for knowing or counting how many items it borrowed

Equipment
  1. Id, Name, IsAvailable
  2, can change its own availability 
  3. not responsible for knowing who borrowed it or checking students eligibility

Borrowing
  1. Id, StudentId, EquipmentId, DateBorrowed, ExpectedReturnDate, Status
  2. can mark itself Returned
  3. not responsible for deciding if whether a new borrow should be allowed
