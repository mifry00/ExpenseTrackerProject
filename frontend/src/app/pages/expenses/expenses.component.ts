import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Router } from '@angular/router';
import { ExpenseService, Expense } from '../../services/expense.service';

@Component({
  selector: 'app-expenses',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './expenses.component.html',
})
export class ExpensesComponent implements OnInit {expenses: Expense[] = [];

  constructor(private expenseService: ExpenseService,private router: Router) {}

  ngOnInit() {this.loadExpenses();}

  loadExpenses() {
    const userId = Number(localStorage.getItem('userId'));
    if (userId) {
      this.expenseService.getExpenses(userId).subscribe({
        next: (data) => {
          this.expenses = data;
        },
        error: (err) => {
          console.error('Failed to fetch expenses', err);
        }
      });
    }
  }

  deleteExpense(expenseId: number) {
    if (confirm('Are you sure you want to delete this expense?')) {
      this.expenseService.deleteExpense(expenseId).subscribe({
        next: () => {
          this.loadExpenses(); // Refresh after delete
        },
        error: (err) => {
          console.error('Failed to delete expense', err);
        }
      });
    }
  }

  editExpense(expense: Expense) {
    this.router.navigate(['/edit-expense', expense.id]);}

  goToAddExpense() {
    this.router.navigate(['/add-expense']);}
}
