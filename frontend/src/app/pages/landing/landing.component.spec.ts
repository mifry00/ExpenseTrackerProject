import { ComponentFixture, TestBed } from '@angular/core/testing';
import { LandingComponent } from './landing.component';
import { FormsModule } from '@angular/forms';
import { provideHttpClient } from '@angular/common/http';
import { HttpTestingController, provideHttpClientTesting } from '@angular/common/http/testing';

// Test suite for the LandingComponent
describe('LandingComponent', () => {
  let component: LandingComponent;
  let fixture: ComponentFixture<LandingComponent>;
  let httpMock: HttpTestingController;

  // Initialize the component before each test
  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [FormsModule],
      providers: [
        LandingComponent,
        provideHttpClient(),
        provideHttpClientTesting()
      ]
    });

    fixture = TestBed.createComponent(LandingComponent);
    component = fixture.componentInstance;
    httpMock = TestBed.inject(HttpTestingController);
    fixture.detectChanges();
  });

  // Default test to check if the component is created
  it('should be created', () => {
    expect(component).toBeTruthy();
  });

  // Test 1 to handle login request
  it('should handle login request', () => {
    
    // Arrange - setup test data
    const testEmail = 'test@example.com';
    const testPassword = 'password123';
    component.email = testEmail;
    component.password = testPassword;

    // Act - perform the action
    component.login();

    // Assert - verify the results
    const req = httpMock.expectOne('http://localhost:5068/api/user/login');
    expect(req.request.method).toBe('POST');
    expect(req.request.body).toEqual({
      email: testEmail,
      passwordHash: testPassword
    });
    req.flush({ userId: 1, isAdmin: false });
  });
});
