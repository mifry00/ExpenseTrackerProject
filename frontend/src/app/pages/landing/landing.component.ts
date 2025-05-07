import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-landing',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './landing.component.html',
  styleUrls: ['./landing.component.css']
})
export class LandingComponent {
  isLoginMode: boolean = true; // login or register view

  email: string = '';
  password: string = '';
  error: string | null = null;
  success: string | null = null;

  constructor(private http: HttpClient, private router: Router) {}

  toggleMode() {
    this.isLoginMode = !this.isLoginMode;
    this.error = null;
    this.success = null;
    this.email = '';
    this.password = '';
  }

  login() {
    const payload = {
      email: this.email,
      passwordHash: this.password,
    };

    this.http.post<any>('http://localhost:5068/api/user/login', payload).subscribe({
      next: (res) => {
        localStorage.setItem('userId', res.userId);
        localStorage.setItem('isAdmin', res.isAdmin);
        this.router.navigate([res.isAdmin ? '/admin' : '/expenses']);
      },
      error: (err) => {
        console.error('Login error:', err);
        this.error = 'Login failed. Please check your email and password.';
      }
    });
  }

  register() {
    const payload = {
      email: this.email,
      passwordHash: this.password,
    };

    this.http.post<any>('http://localhost:5068/api/user/register', payload).subscribe({
      next: (res) => {
        console.log('Registration success', res);
        this.success = 'Registration successful! Please log in.';
        this.error = null;
        this.isLoginMode = true; // switch to login mode automatically
      },
      error: (err) => {
        console.error('Registration error:', err);
        this.error = 'Registration failed. Please try again.';
        this.success = null;
      }
    });
  }
}
