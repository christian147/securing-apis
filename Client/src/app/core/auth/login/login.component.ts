import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from '../services/authentication.service';

@Component({
  template: '',
})
export class LoginComponent implements OnInit {

	constructor(
		private authenticationService: AuthenticationService
	) {}

	ngOnInit(): void {
		this.authenticationService.login();
	}
}
