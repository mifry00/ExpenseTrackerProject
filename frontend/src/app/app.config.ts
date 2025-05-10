import { ApplicationConfig, importProvidersFrom } from '@angular/core';
import { provideRouter } from '@angular/router';
import { routes } from './app.routes';
import { provideHttpClient, withInterceptors, HttpInterceptorFn, HttpRequest, HttpHandlerFn } from '@angular/common/http';
import { FormsModule } from '@angular/forms';

const authInterceptor: HttpInterceptorFn = (req: HttpRequest<unknown>, next: HttpHandlerFn) => {
  const authEmail = 'admin@expense.com';
  const authPassword = 'password';
  const credentials = btoa(`${authEmail}:${authPassword}`);
  const authRequest = req.clone({
    setHeaders: {
      'Authorization': `Basic ${credentials}`
    }
  });
  return next(authRequest);
};

export const appConfig: ApplicationConfig = {
  providers: [
    provideRouter(routes),
    provideHttpClient(withInterceptors([authInterceptor])),
    importProvidersFrom(FormsModule)
  ]
};
