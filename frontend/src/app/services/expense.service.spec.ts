import { TestBed } from '@angular/core/testing';
import { HttpTestingController, provideHttpClientTesting } from '@angular/common/http/testing';
import { ExpenseService } from './expense.service';
import { provideHttpClient } from '@angular/common/http';

// Test suite for the ExpenseService
describe('ExpenseService', () => {
  let service: ExpenseService;
  let httpMock: HttpTestingController;

  // Initialize the service before each test
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [
        ExpenseService,
        provideHttpClient(),
        provideHttpClientTesting()
      ]
    });
    service = TestBed.inject(ExpenseService);
    httpMock = TestBed.inject(HttpTestingController); 
  });

  // Test 1 to check if the service is created
  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  // Test 2 to get expenses for a user
  it('should get expenses for a user', () => {
    // Arrange setting up test data
    const userId = 1;
    const mockExpenses = [
      { id: 1, userId: 1, amount: 100, category: 'Food', description: 'Lunch', expenseDate: '2024-03-20' }
    ];

    // Act calling the service
    service.getExpenses(userId).subscribe(expenses => {
      // Assert checking the result
      expect(expenses).toEqual(mockExpenses);
    });

    // Verify HTTP request is made to the correct endpoint
    const req = httpMock.expectOne(`http://localhost:5068/api/expense/user/${userId}`);
    expect(req.request.method).toBe('GET');
    req.flush(mockExpenses);
  });
});
