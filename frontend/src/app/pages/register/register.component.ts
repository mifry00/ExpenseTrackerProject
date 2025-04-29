import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './register.component.html',
})
export class RegisterComponent {
  email: string = '';
  password: string = '';
  success: string | null = null;
  error: string | null = null;

  constructor(private http: HttpClient, private router: Router) {}

  register() {
    const payload = {
      email: this.email,
      passwordHash: this.password  // Use passwordHash to match the backend
    };
  
    this.http.post('http://localhost:5068/api/user/register', payload).subscribe({
      next: (response) => {
        console.log('Registration successful!', response);
        this.success = 'Registration successful! Please log in.';
        this.error = null;
  
        // Optional redirect after registration:
        // this.router.navigate(['/login']);
      },
      error: (err) => {
        console.error('Registration error:', err);
        this.success = null;
        this.error = 'Registration failed. Please try again.';
      }
    });
  
  }
}
