import { ApplicationConfig, importProvidersFrom } from '@angular/core';
import { provideRouter } from '@angular/router';
import { routes } from './app.routes';
import { provideHttpClient, withInterceptors, HttpInterceptorFn, HttpRequest, HttpHandlerFn } from '@angular/common/http';
import { FormsModule } from '@angular/forms';

// Simple header-based authentication (matches backend)
const authInterceptor: HttpInterceptorFn = (req: HttpRequest<unknown>, next: HttpHandlerFn) => {
  const authSecret = 'password'; // This must match what your backend checks for
  const authRequest = req.clone({
    setHeaders: {
      'X-My-Request-Header': authSecret
    }
  });
  return next(authRequest);
};
// Add the interceptor to the HTTP client
export const appConfig: ApplicationConfig = {
  providers: [
    provideRouter(routes),
    provideHttpClient(withInterceptors([authInterceptor])),
    importProvidersFrom(FormsModule)
  ]
};
