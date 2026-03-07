import { AuthConfig } from 'angular-oauth2-oidc';
import { environment } from '../../../environments/environment';

export const authConfig: AuthConfig = {
  issuer: environment.auth.issuer,
  loginUrl: 'https://accounts.google.com/o/oauth2/v2/auth',
  tokenEndpoint: 'https://oauth2.googleapis.com/token',
  userinfoEndpoint: 'https://openidconnect.googleapis.com/v1/userinfo',
  redirectUri: window.location.origin,
  clientId: environment.auth.clientId,
  dummyClientSecret: environment.auth.clientSecret,
  responseType: 'code',
  scope: 'openid profile email',
  showDebugInformation: !environment.production,
  strictDiscoveryDocumentValidation: false,
  skipIssuerCheck: true,
};
