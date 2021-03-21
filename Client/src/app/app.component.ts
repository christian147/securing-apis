import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from './core/auth/services/authentication.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})
export class AppComponent implements OnInit {
  constructor(
    private authenticationService: AuthenticationService)
  {}

  async ngOnInit(): Promise<void> {
    await this.authenticationService.init();
  }
}