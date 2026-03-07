import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { OAuthService } from 'angular-oauth2-oidc';

export const authInterceptor: HttpInterceptorFn = (req, next) => {
  const oauthService = inject(OAuthService);
  const idToken = oauthService.getIdToken();

  if (idToken) {
    const authorizedRequest = req.clone({
      setHeaders: { Authorization: `Bearer ${idToken}` },
    });
    return next(authorizedRequest);
  }

  return next(req);
};
