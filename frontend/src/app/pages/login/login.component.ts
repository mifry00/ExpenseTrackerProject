import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  standalone: true, 
  imports: [CommonModule, FormsModule, RouterModule], 
  templateUrl: './login.component.html',
})
export class LoginComponent {
  email: string = '';
  password: string = '';
  error: string = '';
  success: string = '';


  constructor(private http: HttpClient, private router: Router) {}

  login() {
    const payload = {
      email: this.email,
      passwordHash: this.password
    };

    this.http.post<any>('http://localhost:5068/api/user/login', payload).subscribe({
      next: (res) => {
        localStorage.setItem('userId', res.userId);
        localStorage.setItem('isAdmin', res.isAdmin);
        this.router.navigate(['/dashboard']);
        localStorage.setItem('userEmail', this.email);

      },
      error: () => {
        this.error = 'Login failed. Check your email and password.';
      }
    });
  }
}
