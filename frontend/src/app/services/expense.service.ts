import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface Expense {
  id?: number;
  userId: number;
  amount: number;
  category: string;
  description: string;
  expenseDate: string;  // ISO format: 'YYYY-MM-DD'
  isApproved?: boolean;
  createdAt?: string;
}

@Injectable({
  providedIn: 'root'
})
export class ExpenseService {
  private apiUrl = 'http://localhost:5068/api/expense'; //backend endpoint

  constructor(private http: HttpClient) {}

  getExpenses(userId: number): Observable<Expense[]> {
    return this.http.get<Expense[]>(`${this.apiUrl}/user/${userId}`);
  }

  addExpense(expense: Expense) {
    return this.http.post(`${this.apiUrl}/add`, expense);
  }

  deleteExpense(expenseId: number) {
    return this.http.delete(`${this.apiUrl}/${expenseId}`);
  }
}
