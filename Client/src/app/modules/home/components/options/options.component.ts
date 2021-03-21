import { Component, OnDestroy } from '@angular/core';
import { Router } from '@angular/router';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { AuthenticationService } from '@app/core/auth/services/authentication.service';

@Component({
  selector: 'app-options',
  templateUrl: './options.component.html'
})
export class OptionsComponent {
  public apiResponse: string = null;

  constructor(
    private authenticationService: AuthenticationService,
    private router: Router,
    private http: HttpClient) {
  }

  public async logout(): Promise<void> {
    await this.authenticationService.logout();
    this.router.navigate(['/']);
  }

  public async renewToken(): Promise<void> {
    await this.authenticationService.renewToken();
  }

  public callAuthorized(): void {
    this.callApi('authorized');
  }

  public callAdministrator(): void {
    this.callApi('administrator');
  }

  public callUser(): void {
    this.callApi('user');
  }

  public callSendEmail(): void {
    this.callApi('send-email');
  }

  public callOlderThan18(): void {
    this.callApi('older-than-18');
  }

  public callMigrator(): void {
    this.callApi('migrator');
  }

  private callApi(endpoint: string): void {
    this.http.get<Response>(`api/policies/${endpoint}`).subscribe(
      (response: Response) => { this.apiResponse = response.message;},
      (error: HttpErrorResponse) => {this.apiResponse = `${error.status} - ${error.statusText}`;}
    );
  }
}

interface Response {
  message: string;
}
