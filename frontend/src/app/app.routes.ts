import { Routes } from '@angular/router';
import { LoginComponent } from './pages/login/login.component';
import { RegisterComponent } from './pages/register/register.component';
import { ExpensesComponent } from './pages/expenses/expenses.component';
import { AdminComponent } from './pages/admin/admin.component';

export const routes: Routes = [
  { path: '', loadComponent: () => import('./pages/landing/landing.component').then(m => m.LandingComponent)},
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'expenses', component: ExpensesComponent },
  { path: 'dashboard', loadComponent: () => import('./pages/dashboard.component').then(m => m.DashboardComponent)},
  { path: 'add-expense', loadComponent: () => import('./pages/add-expense/add-expense.component').then(m => m.AddExpenseComponent)},
  { path: 'edit-expense/:id', loadComponent: () => import('./pages/edit-expense/edit-expense.component').then(c => c.EditExpenseComponent)},
  { path: 'admin', loadComponent: () => import('./pages/admin-dashboard/admin-dashboard.component').then(c => c.AdminDashboardComponent)}
];
