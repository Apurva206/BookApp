import { Component,inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { jwtDecode} from 'jwt-decode';

@Component({
  selector: 'app-login',
  imports: [FormsModule,CommonModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {
  loginObj:any = {
    Username: '',
    Password: ''
  };
  errorMessage: string = '';
  isLoggedIn: boolean = false;

  http=inject(HttpClient);

  constructor(private router:Router){

  }

  onLoginSubmit(){
    this.http.post("https://localhost:7162/api/User/login",this.loginObj).subscribe((res:any)=>{
      console.log('API Response:', res); 
      if(res && res.username && res.token ){
        alert("Login Success");
        localStorage.setItem('authToken', res.token);
         // Decode the token as 'any' or an indexable object
        const decodedToken = jwtDecode(res.token) as { [key: string]: any };
        console.log(decodedToken);
        this.router.navigate(['/app-dashboard']);
      }
      else{
        alert("check username or password");
      }
    })
  }
  
  
}
