import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthenticationService } from '@app/core/auth/services/authentication.service';

@Component({
  template: ''
})

export class CallbackComponent implements OnInit {
  constructor(private authenticationService: AuthenticationService,
    private router: Router) { }

  async ngOnInit() {
    await this.authenticationService.redirectFromCallback();
    this.router.navigate(['/']);
  }
}
