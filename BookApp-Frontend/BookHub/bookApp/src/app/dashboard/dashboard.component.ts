import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
@Component({
  selector: 'app-dashboard',
  imports: [FormsModule,CommonModule],
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.css'
})
export class DashboardComponent {
  BookObj:any = {
    "id": 0,
    "isbn": "",
    "title": "",
    "author": "",
    "publicationYear": ""
    }
  
    https = inject(HttpClient);
    onSubmit(){
      const token = localStorage.getItem('authToken');  // Retrieve the token
      const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
      const payload = {
        Id: this.BookObj.id,
        Title: this.BookObj.title,
        Author: this.BookObj.author,
        ISBN: this.BookObj.isbn.toString(), // Ensure ISBN is a string
        PublicationYear: this.BookObj.publicationYear
        
      };
      this.https.post("https://localhost:7162/api/Book",payload, { headers }).subscribe((res:any)=>{
        console.log('Book added successfully:', res);
        this.getAllUser(); // Refresh the list of appointments
        this.isAdding = false; 
        this.resetForm(); // Reset the form fields
        alert('Book added successfully!');
      })
    }

    onDelete(isbn:number){
      {
        const token = localStorage.getItem('authToken');  // Retrieve the token
        const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
        // const payload = {
        //   Id: this.BookObj.id,
        //   Title: this.BookObj.title,
        //   Author: this.BookObj.author,
        //   ISBN: this.BookObj.isbn.toString(), // Ensure ISBN is a string
        //   PublicationYear: this.BookObj.publicationYear
          
        // };
        const formData= new FormData();
        formData.append("isbn",isbn.toString());
        this.https.post(`https://localhost:7162/api/Book/DeleteBook`,formData, {headers}).subscribe((res:any)=>{
          console.log('Book deleted successfully:', res);
          this.getAllUser(); // Refresh the list of appointments
          this.isAdding = false; 
          this.resetForm(); // Reset the form fields
          alert('Book deleted successfully!');
        })
      }
    } 
  
    onEdit(data : any){
      this.BookObj=data;
    }
  
    onUpdate(){
      const isbn = this.BookObj.isbn;
      const token = localStorage.getItem('authToken');  // Retrieve the token
      const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
      const payload = {
        // Id: this.BookObj.id,
        Title: this.BookObj.title,
        Author: this.BookObj.author,
        // ISBN: this.BookObj.isbn, // Ensure ISBN is a string
        PublicationYear: this.BookObj.publicationYear
      };
      this.https.put(`https://localhost:7162/api/Book/${isbn}`,payload, { headers }).subscribe((res:any)=>{
        console.log('Book Detail updated successfully:', res);
        this.getAllUser(); // Refresh the list of appointments
        this.isAdding = false; 
        this.resetForm(); // Reset the form fields
        alert('Book Detail updated successfully!');
      }
    )
    }
    userList:any [] = [];
    isAdding: boolean = false;
    toggleForm() {
      this.isAdding = !this.isAdding; // Toggle the form
    }
    constructor(private http: HttpClient){
  
    }
    getAllUser(){
      const token = localStorage.getItem('authToken');  // Retrieve the token
      const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
      this.http.get("https://localhost:7162/api/Book",{ headers }).subscribe((result:any)=>{
        this.userList = result;
      })
    }
    addAppointment() {
      this.isAdding = true;
      this.resetForm();
    }
    resetForm() {
      this.BookObj = {
        id: 0,
        isbn: "",
        title: "",
        author: "",
        publicationYear: ""
      };
    }
}
