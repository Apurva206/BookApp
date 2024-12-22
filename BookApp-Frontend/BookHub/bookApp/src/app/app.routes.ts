import { Routes } from '@angular/router';
import { RegisterComponent } from './register/register.component';
import { LoginComponent } from './login/login.component';
import { DashboardComponent } from './dashboard/dashboard.component';
export const routes: Routes = [
    {
        path: 'app-register',
        component: RegisterComponent
      },
      {
        path: 'app-login',
        component: LoginComponent
      },
      {
        path:'app-dashboard',
        component: DashboardComponent,
        
      }
];
