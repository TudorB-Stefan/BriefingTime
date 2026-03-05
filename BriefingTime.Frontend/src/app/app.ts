import { HttpClient } from "@angular/common/http";
import { Component, inject, OnInit, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { Navbar } from "./shared/components/navbar/navbar";
import { AuthService } from "./core/services/auth.service";

@Component({
  selector: 'app-root',
  imports: [Navbar,RouterOutlet],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App implements OnInit {
  private http = inject(HttpClient);
  protected title = 'BriefingTime';
  protected members = signal<any>([]);
  authService = inject(AuthService);

  ngOnInit(): void {}
}
