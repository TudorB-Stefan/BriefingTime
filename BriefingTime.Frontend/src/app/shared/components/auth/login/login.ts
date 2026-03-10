import { Component, inject } from '@angular/core';
import { FormsModule } from "@angular/forms";
import {Router, RouterLink } from "@angular/router";
import { AuthService } from "../../../../core/services/auth.service";

@Component({
  selector: 'app-login',
  imports: [FormsModule,RouterLink],
  templateUrl: './login.html',
  styleUrl: './login.css',
})
export class Login {
  private authService = inject(AuthService)
  private router = inject(Router);
  protected creds: any = {}
  login(){
    this.authService.login(this.creds).subscribe({
      next: (result: any) => {
        console.log(result);
        this.router.navigate(['/my-account']);
      },
      error: (error: { message: any; }) => {
        alert(error.message);
      }
    });
  }
}
