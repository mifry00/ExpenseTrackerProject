import { Component } from '@angular/core';
import { ExpenseService, Expense } from '../../services/expense.service';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';

@Component({
  selector: 'app-add-expense',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './add-expense.component.html',
})
export class AddExpenseComponent {
  amount: number = 0;
  category: string = '';
  description: string = '';
  expenseDate: string = '';
  message: string = '';
  success: string | null = null;
  error: string | null = null;

  constructor(private expenseService: ExpenseService, private router: Router) {}

  submitExpense() {
    const userId = Number(localStorage.getItem('userId'));

    const newExpense: Expense = {
      userId,
      amount: this.amount,
      category: this.category,
      description: this.description,
      expenseDate: this.expenseDate,
    };

    this.expenseService.addExpense(newExpense).subscribe({
      next: () => {
        this.message = 'Expense added successfully!';
        // Optionally navigate back
        this.router.navigate(['/expenses']);
      },
      error: (err) => {
        console.error('Failed to add expense', err);
        this.message = 'Failed to add expense.';
      }
    });
  }
}