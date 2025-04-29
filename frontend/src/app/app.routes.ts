import { Routes } from '@angular/router';
import { LoginComponent } from './pages/login/login.component';
import { RegisterComponent } from './pages/register/register.component';
import { ExpensesComponent } from './pages/expenses/expenses.component';

export const routes: Routes = [
  { path: '', loadComponent: () => import('./pages/landing/landing.component').then(m => m.LandingComponent)},
  { path: '', redirectTo: 'login', pathMatch: 'full' },
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'expenses', component: ExpensesComponent },
  { path: 'dashboard',
  loadComponent: () => import('./pages/dashboard.component').then(m => m.DashboardComponent)},
  {path: 'add-expense',
    loadComponent: () =>
      import('./pages/add-expense/add-expense.component').then(m => m.AddExpenseComponent)
  }

];
