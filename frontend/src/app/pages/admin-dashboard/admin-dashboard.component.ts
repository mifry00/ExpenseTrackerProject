import { Component, OnInit } from '@angular/core';
import { ExpenseService, Expense } from '../../services/expense.service'; 
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-admin-dashboard',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './admin-dashboard.component.html',
})
export class AdminDashboardComponent implements OnInit {
  expenses: Expense[] = [];

  constructor(private expenseService: ExpenseService) {}

  ngOnInit() {
    this.loadUnapprovedExpenses();
  }

  loadUnapprovedExpenses() {
    this.expenseService.getUnapprovedExpenses().subscribe({
      next: (data) => {
        console.log('Received expenses:', data);  
        this.expenses = data;
      },
      error: (err) => {
        console.error('Failed to fetch unapproved expenses', err);
      }
    });
  }

  approveExpense(expenseId: number | undefined) {
    if (!expenseId) return;

    this.expenseService.approveExpense(expenseId).subscribe({
      next: () => {
        console.log('Expense approved successfully!');
        this.loadUnapprovedExpenses(); // Refresh list after approval
      },
      error: (err) => {
        console.error('Failed to approve expense', err);
      }
    });
  }
}


