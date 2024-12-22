import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';
@Component({
  selector: 'app-register',
  imports: [FormsModule, CommonModule],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent {
  user = {
    Username: '',
    Password:'',
    confirmPassword:'',
    email: ''
  };
  // Role: string = 'Doctor'; // Default role
  errorMessage: string = '';
  successMessage: string = '';
  isRegistered: boolean = false;
   fetcheduser: any = null;

   constructor(private http: HttpClient) {}  // Inject HttpClient here
  
  onSubmit() {
    
    // console.log('User Details:', this.user);
   
    //   localStorage.setItem('UserDetails', JSON.stringify(this.user));

    //   if (this.user.password === this.user.confirmPassword) {
    //     // Proceed with form submission (e.g., send data to API or store data)
    //     console.log('Form submitted successfully:', this.user);
    //   }
    // Check if passwords match
    if (this.user.Password !== this.user.confirmPassword) {
      this.errorMessage = 'Passwords do not match!';
      return;
    }

    // Prepare the data to be sent to the backend
    const payload = {
      username: this.user.Username,
      password: this.user.Password,
      Email: this.user.email // Default role sent to the backend
    };

    // Clear previous messages
    this.errorMessage = '';
    this.successMessage = '';

    // Send data to the backend via POST API
    this.http.post('https://localhost:7162/api/User/register', payload)
      .subscribe({
        next: (response) => {
          // Success: Display success message and store data in local storage
          
          this.successMessage = 'User registered successfully!';
          this.isRegistered = true;  // Set the flag for successful registration
          localStorage.setItem('UserDetails', JSON.stringify(this.user));  // Store user data in localStorage
          console.log('Registration response:', response);  // Log the response for debugging
          alert(this.successMessage);
        },
        // error: (err) => {
        //   // Error: Display error message
        //   this.errorMessage = err.error?.message || 'Registration failed. Please try again.';
        //   console.error('Error during registration:', err);  // Log the error for debugging
        // }
      });
      
   }
   fetchData() {
    
    const storedEmployee = JSON.parse(localStorage.getItem('UserDetails') || '{}');
    this.fetcheduser = storedEmployee;
    console.log('Fetched User Data:', this.fetcheduser);
    }
    resetForm() {
      console.log('Resetting form'); // Debug log
    this.isRegistered = false; // Hide the success message
    this.successMessage = ''; // Clear success message
    this.errorMessage = ''; // Clear error message
    this.user = { Username: '', Password: '', confirmPassword: '', email:''}; // Reset user object
    }
}
