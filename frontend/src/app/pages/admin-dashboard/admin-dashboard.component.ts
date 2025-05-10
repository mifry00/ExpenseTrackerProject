import { Component, OnInit } from '@angular/core';
import { ExpenseService, Expense } from '../../services/expense.service'; 
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-admin-dashboard',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './admin-dashboard.component.html',
  styleUrls: ['./admin-dashboard.component.css']
})


export class AdminDashboardComponent implements OnInit {

  expenses: Expense[] = [];
  showApproved: boolean = true; // Toggle between approved and unapproved views

  constructor(private expenseService: ExpenseService) {}

  ngOnInit() {
    console.log('AdminDashboardComponent initialized');
    // Start with unapproved expenses
    this.showApproved = false;
    this.loadExpenses();
  }

  loadExpenses() {
    console.log('Loading expenses, showApproved:', this.showApproved);
    if (this.showApproved) {
      this.loadApprovedExpenses();
    } else {
      this.loadUnapprovedExpenses();
    }
  }

  loadApprovedExpenses() {
    console.log('Loading approved expenses...');
    this.expenseService.getApprovedExpenses().subscribe({
      next: (data) => {
        console.log('Received approved expenses:', data);
        this.expenses = data;
        // Debug log for each expense's approval status
        this.expenses.forEach(expense => {
          console.log(`Expense ${expense.id} isApproved:`, expense.isApproved, typeof expense.isApproved);
        });
      },
      error: (err) => {
        console.error('Failed to fetch approved expenses:', err);
      }
    });
  }

  loadUnapprovedExpenses() {
    console.log('Loading unapproved expenses...');
    this.expenseService.getUnapprovedExpenses().subscribe({
      next: (data) => {
        console.log('Received unapproved expenses:', data);
        this.expenses = data;
        // Debug log for each expense's approval status
        this.expenses.forEach(expense => {
          console.log(`Expense ${expense.id} isApproved:`, expense.isApproved, typeof expense.isApproved);
        });
      },
      error: (err) => {
        console.error('Failed to fetch unapproved expenses:', err);
      }
    });
  }

  toggleView() {
    console.log('Toggling view. Current showApproved:', this.showApproved);
    this.showApproved = !this.showApproved;
    console.log('New showApproved value:', this.showApproved);
    this.loadExpenses();
  }

  approveExpense(expenseId: number | undefined) {
    if (!expenseId) return;

    console.log('Approving expense:', expenseId);
    this.expenseService.approveExpense(expenseId).subscribe({
      next: () => {
        console.log('Expense approved successfully!');
        this.loadExpenses(); // Refresh list after approval
      },
      error: (err) => {
        console.error('Failed to approve expense:', err);
      }
    });
  }

  unapproveExpense(expenseId: number | undefined) {
    if (!expenseId) return;

    if (confirm('Are you sure you want to remove this expense?')) {
      console.log('Unapproving expense:', expenseId);
      this.expenseService.deleteExpense(expenseId).subscribe({
        next: () => {
          console.log('Expense removed successfully!');
          this.loadExpenses(); // Refresh list after removal
        },
        error: (err) => {
          console.error('Failed to remove expense:', err);
        }
      });
    }
  }

  deleteExpense(expenseId: number | undefined) {
    if (!expenseId) return;

    if (confirm('Are you sure you want to delete this expense?')) {
      console.log('Deleting expense:', expenseId);
      this.expenseService.deleteExpense(expenseId).subscribe({
        next: () => {
          console.log('Expense deleted successfully!');
          this.loadExpenses(); // Refresh list after deletion
        },
        error: (err) => {
          console.error('Failed to delete expense:', err);
        }
      });
    }
  }
}


