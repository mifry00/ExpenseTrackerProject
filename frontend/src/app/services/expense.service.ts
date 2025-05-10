import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface Expense {
  id?: number;
  userId: number;
  amount: number;
  category: string;
  description: string;
  expenseDate: string;  
  isApproved?: boolean;
  createdAt?: string;
}

@Injectable({
  providedIn: 'root'
})
export class ExpenseService {
  private apiUrl = 'http://localhost:5068/api/expense'; // base URL for API endpoints

  constructor(private http: HttpClient) {}

  // HTTP request initiated to get all expenses for a user
  // Returns observable of expense array
  getExpenses(userId: number): Observable<Expense[]> {
    return this.http.get<Expense[]>(`${this.apiUrl}/user/${userId}`);
  }

  // HTTP request initiated to add a new expense
  addExpense(expense: Expense) {
    return this.http.post(`${this.apiUrl}/add`, expense);
  }

  // HTTP request initiated to delete an expense
  deleteExpense(expenseId: number) {
    return this.http.delete(`${this.apiUrl}/${expenseId}`);
  }

  // HTTP request initiated to get an expense by ID
  getExpenseById(expenseId: number): Observable<Expense> {
    return this.http.get<Expense>(`${this.apiUrl}/${expenseId}`);
  }

  // HTTP request initiated to update an expense
  updateExpense(expense: Expense) {
    return this.http.put(`${this.apiUrl}/update`, expense);
  }  

  // HTTP request initiated to get all unapproved expenses
  getUnapprovedExpenses(): Observable<Expense[]> {
    console.log('Making request to get unapproved expenses');
    const request = this.http.get<Expense[]>(`${this.apiUrl}/unapproved`);
    request.subscribe({
      next: (data) => console.log('Unapproved expenses response:', data),
      error: (error) => {
        console.error('Error getting unapproved expenses:', error);
        console.error('Error status:', error.status);
        console.error('Error message:', error.message);
        if (error.error) {
          console.error('Error details:', error.error);
        }
      }
    });
    return request;
  }

  // HTTP request initiated to get all approved expenses
  getApprovedExpenses(): Observable<Expense[]> {
    console.log('Making request to get approved expenses');
    const request = this.http.get<Expense[]>(`${this.apiUrl}/approved`);
    request.subscribe({
      next: (data) => console.log('Approved expenses response:', data),
      error: (error) => {
        console.error('Error getting approved expenses:', error);
        console.error('Error status:', error.status);
        console.error('Error message:', error.message);
        if (error.error) {
          console.error('Error details:', error.error);
        }
      }
    });
    return request;
  }

  // HTTP request initiated to approve an expense
  approveExpense(expenseId: number) {
    return this.http.post(`${this.apiUrl}/approve/${expenseId}`, null);
  }

  // HTTP request initiated to unapprove/delete an expense
  unapproveExpense(expenseId: number) {
    return this.http.post(`${this.apiUrl}/unapprove/${expenseId}`, null);
  }
}
