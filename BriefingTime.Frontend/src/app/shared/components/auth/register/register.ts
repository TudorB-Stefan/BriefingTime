import { Component, inject } from '@angular/core';
import { FormsModule } from "@angular/forms";
import {Router, RouterLink } from "@angular/router";
import { AuthService } from "../../../../core/services/auth.service";

@Component({
  selector: 'app-register',
  imports: [FormsModule,RouterLink],
  templateUrl: './register.html',
  styleUrl: './register.css',
})
export class Register {
  private authService = inject(AuthService)
  private router = inject(Router);
  protected creds: any = {}
  register(){
    this.authService.register(this.creds).subscribe({
      next: (result: any) => {
        console.log(result);
        this.router.navigate(['/login']);
      },
      error: (error: { message: any; }) => {
        alert(error.message);
      }
    });
  }
}
