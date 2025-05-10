import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Router } from '@angular/router';
import { ExpenseService, Expense } from '../../services/expense.service';

@Component({
  selector: 'app-expenses',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './expenses.component.html',
  styleUrls: ['./expenses.component.css']
})
export class ExpensesComponent implements OnInit {
  expenses: Expense[] = [];

  constructor(private expenseService: ExpenseService, private router: Router) {}

  ngOnInit() {
    console.log('ExpensesComponent initialized');
    this.loadExpenses();
  }

  loadExpenses() {
    const userId = Number(localStorage.getItem('userId'));
    console.log('Loading expenses for userId:', userId);
    
    if (!userId) {
      console.error('No user ID found');
      this.router.navigate(['/']); // Redirect to login if no userId
      return;
    }

    console.log('Making API call to get expenses...');
    this.expenseService.getExpenses(userId).subscribe({
      next: (data) => {
        console.log('Received expenses:', data);
        this.expenses = data;
      },
      error: (err) => {
        console.error('Failed to fetch expenses:', err);
      }
    });
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
    this.router.navigate(['/edit-expense', expense.id]);
  }

  goToAddExpense() {
    this.router.navigate(['/add-expense']);
  }
}
