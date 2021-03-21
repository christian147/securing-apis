import { Injectable } from '@angular/core';
import { UserManager, User, WebStorageStateStore, UserManagerSettings } from 'oidc-client';
import { BehaviorSubject } from 'rxjs';
import { filter } from 'rxjs/operators';
import { environment } from '@environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {
  private userManager: UserManager;
  private userSource = new BehaviorSubject<User>(null);
  public user$ = this.userSource.asObservable().pipe(filter(userdata => !!userdata));

  public async init(): Promise<void> {
    const settings: UserManagerSettings = {
      userStore: new WebStorageStateStore({ store: window.localStorage }),
      authority: environment.authority,
      client_id: environment.clientId,
      scope: environment.scope,
      redirect_uri: `${window.location.origin}/callback`,
      post_logout_redirect_uri: `${window.location.origin}/login`,
      silent_redirect_uri: `${window.location.origin}/silent-renew-oidc.html`,
      monitorSession: true,
      automaticSilentRenew: true,
      filterProtocolClaims: true,
      loadUserInfo: false,
      response_type: 'code'
    };

    this.userManager = new UserManager(settings);
    this.userManager.clearStaleState();
    this.userManager.events.addUserLoaded((user) => { 
      this.userSource.next(user); 
    });
    this.userManager.events.addUserSignedOut(() => {
      this.userSource.next(null);
      this.userManager.removeUser();
    });
  }

  public async getAccessToken(): Promise<string> {
    return this.userSource.value?.access_token;
  }

  public isLoggedIn(): boolean {
    return this.userSource.value != null && !this.userSource.value?.expired;
  }

  public login(): Promise<void> {
    return this.userManager.signinRedirect();
  }

  public async renewToken(): Promise<void> {
    if (this.isLoggedIn()) {
      await this.userManager.signinSilent();
    }
  }

  public logout(): Promise<void> {
    return this.userManager.signoutRedirect();
  }

  public async redirectFromCallback(): Promise<void> {
    await this.userManager.signinRedirectCallback();
  }
}
