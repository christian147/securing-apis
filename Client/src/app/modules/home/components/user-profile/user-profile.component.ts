import { Component, OnDestroy, OnInit } from '@angular/core';
import { AuthenticationService } from '@app/core/auth/services/authentication.service';
import { User} from 'oidc-client';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-user-profile',
  templateUrl: './user-profile.component.html'
})
export class UserProfileComponent implements OnInit, OnDestroy {

  private userInfo: User;
  private subscription = new Subscription();
  constructor(private authenticationService: AuthenticationService) { }

  ngOnInit() {
    this.subscription = this.authenticationService.user$.subscribe(user => {
      this.userInfo = user;
    })
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

  get user(): User {
    return this.userInfo;
  }
}
