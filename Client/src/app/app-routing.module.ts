import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { CallbackComponent } from '@app/core/auth/callback/callback.component';
import { LoginComponent } from '@app/core/auth/login/login.component';
import { AuthGuardService } from '@app/core/auth/services/auth-guard.service';
import { AppRoutes } from '@app/core/constants/app-routes';

const routes: Routes = [
  {
    path: AppRoutes.home,
    loadChildren: () => import('@app/modules/home/home.module').then((m) => m.HomeModule),
    canActivate: [AuthGuardService]
  },
  {
    path: AppRoutes.login,
    component: LoginComponent
  },
  {
    path: AppRoutes.callback,
    component: CallbackComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes, { relativeLinkResolution: 'corrected' })],
  exports: [RouterModule]
})
export class AppRoutingModule { }
