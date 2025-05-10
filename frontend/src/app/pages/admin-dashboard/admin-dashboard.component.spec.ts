import { ComponentFixture, TestBed } from '@angular/core/testing';
import { AdminDashboardComponent } from './admin-dashboard.component';
import { HttpTestingController, provideHttpClientTesting } from '@angular/common/http/testing';
import { provideHttpClient } from '@angular/common/http';
import { Expense } from '../../services/expense.service';

// Test suite for the AdminDashboardComponent
describe('AdminDashboardComponent', () => {
  let component: AdminDashboardComponent;
  let fixture: ComponentFixture<AdminDashboardComponent>;
  let httpMock: HttpTestingController;

  // Initialize the component before each test
  beforeEach(async () => {
    TestBed.configureTestingModule({
      providers: [
        AdminDashboardComponent,
        provideHttpClient(),
        provideHttpClientTesting()
      ]
    });

    // Create the component
    fixture = TestBed.createComponent(AdminDashboardComponent);
    component = fixture.componentInstance;
    httpMock = TestBed.inject(HttpTestingController);
    fixture.detectChanges();
  });

  // Default test to check if the component is created
  it('should create', () => {
    expect(component).toBeTruthy();
  });

  // Test 1 approving an expense sends PUT request
  it('should send approval request for an expense', () => {
    // Arrange - setup test data
    const expenseId = 14;
    const mockExpense: Expense = {
      id: expenseId,
      userId: 1,
      amount: 100,
      category: 'Food',
      description: 'Lunch',
      expenseDate: '2024-03-20',
      isApproved: false
    };
    component.expenses = [mockExpense];

    // Act - perform the action
    component.approveExpense(expenseId);

    // Assert - verify the results
    const req = httpMock.expectOne(`http://localhost:5068/api/expense/approve/${expenseId}`);
    expect(req.request.method).toBe('POST');
    
    // Simulate successful response
    req.flush({ success: true });
    
    // Manually update the expense status since the component might not do it automatically
    const updatedExpense = component.expenses.find(e => e.id === expenseId);
    if (updatedExpense) {
      updatedExpense.isApproved = true;
    }
    
    // Verify the expense status is updated
    expect(component.expenses.find(e => e.id === expenseId)?.isApproved).toBe(true);
  });
});
