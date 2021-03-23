import { Component, OnDestroy } from '@angular/core';
import { Router } from '@angular/router';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { AuthenticationService } from '@app/core/auth/services/authentication.service';
import { User } from 'oidc-client';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-options',
  templateUrl: './options.component.html'
})
export class OptionsComponent {
  public apiResponse: string = null;
  public isAdmin: boolean;
  public isUser: boolean;
  private subscription = new Subscription();
  
  constructor(
    private authenticationService: AuthenticationService,
    private router: Router,
    private http: HttpClient) {
  }

  ngOnInit() {
    this.subscription = this.authenticationService.user$.subscribe(user => {
      this.isAdmin = user.profile.name === "Bob Smith";
      this.isUser = user.profile.name === "Alice Smith";
    })
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }


  public async logout(): Promise<void> {
    await this.authenticationService.logout();
    this.router.navigate(['/']);
  }

  public async renewToken(): Promise<void> {
    await this.authenticationService.renewToken();
  }

  public callAuthorized(): void {
    this.callApi('api/policies/authorized');
  }

  public callAdministrator(): void {
    this.callApi('api/policies/administrator');
  }

  public callUser(): void {
    this.callApi('api/policies/user');
  }

  public callSendEmail(): void {
    this.callApi('api/policies/send-email');
  }

  public callMigrator(): void {
    this.callApi('api/policies/migrator');
  }
  
  public GetClaims(): void {
    this.callApi('api/claims');
  }

  private callApi(endpoint: string): void {
    this.http.get<Response>(endpoint).subscribe(
      (response: Response) => { this.apiResponse = response.message;},
      (error: HttpErrorResponse) => {this.apiResponse = `${error.status} - ${error.statusText}`;}
    );
  }
}

interface Response {
  message: string;
}
