import { CommonModule } from '@angular/common';
import { CUSTOM_ELEMENTS_SCHEMA, NgModule } from '@angular/core';
import { 
  HomeComponent,
  OptionsComponent, 
  UserProfileComponent, 
} from '@app/modules/home';
import { HomeRoutingModule } from './home-routing.module';

@NgModule({
    declarations: [
      HomeComponent,
      OptionsComponent,
      UserProfileComponent
    ],
    imports: [
        CommonModule,
        HomeRoutingModule
    ],
    schemas: [CUSTOM_ELEMENTS_SCHEMA]
  })
  export class HomeModule { }