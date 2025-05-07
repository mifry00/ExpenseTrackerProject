import { Routes } from '@angular/router';
import { ExpensesComponent } from './pages/expenses/expenses.component';

export const routes: Routes = [
  { path: '', loadComponent: () => import('./pages/landing/landing.component').then(m => m.LandingComponent)},
  { path: 'expenses', component: ExpensesComponent },
  { path: 'add-expense', loadComponent: () => import('./pages/add-expense/add-expense.component').then(m => m.AddExpenseComponent)},
  { path: 'edit-expense/:id', loadComponent: () => import('./pages/edit-expense/edit-expense.component').then(c => c.EditExpenseComponent)},
  { path: 'admin', loadComponent: () => import('./pages/admin-dashboard/admin-dashboard.component').then(c => c.AdminDashboardComponent)}
];
