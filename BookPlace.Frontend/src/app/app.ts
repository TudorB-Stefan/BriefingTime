import { HttpClient } from "@angular/common/http";
import { Component, inject, OnInit, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-root',
  imports: [],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App implements OnInit {
  private http = inject(HttpClient);
  protected title = 'BookPlace';
  protected books = signal<any>([]);

  ngOnInit(): void {
    this.http.get('http://localhost:8080/api/Book').subscribe({
      next: response => this.books.set(response),
      error: error => console.log(error),
      complete: () => console.log('Succes')
    })
  }
}
