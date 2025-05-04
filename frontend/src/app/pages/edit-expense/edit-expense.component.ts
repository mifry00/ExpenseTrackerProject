import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ExpenseService, Expense } from '../../services/expense.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms'; // Correct spelling

@Component({
  selector: 'app-edit-expense',
  standalone: true,
  imports: [CommonModule, FormsModule],  // Correct spelling above
  templateUrl: './edit-expense.component.html',
})
export class EditExpenseComponent implements OnInit {
  expenseId: number | null = null;
  expense: Expense = {
    userId: 0,
    amount: 0,
    category: '',
    description: '',
    expenseDate: '',
    isApproved: false
  };

  constructor(
    private route: ActivatedRoute,
    private expenseService: ExpenseService,
    private router: Router
  ) {}

  ngOnInit() {
    this.expenseId = Number(this.route.snapshot.paramMap.get('id'));
    if (this.expenseId) {
      this.expenseService.getExpenseById(this.expenseId).subscribe({
        next: (data) => this.expense = data,
        error: (err) => console.error('Failed to load expense', err)
      });
    }
  }

  updateExpense() {
    if (this.expenseId) {
      this.expenseService.updateExpense(this.expense).subscribe({
        next: () => {
          console.log('Expense updated!');
          this.router.navigate(['/expenses']);
        },
        error: (err) => console.error('Failed to update', err)
      });
    }
  }
}
