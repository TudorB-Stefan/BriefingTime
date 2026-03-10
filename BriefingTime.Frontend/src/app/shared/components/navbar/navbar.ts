import { Component, inject } from '@angular/core';
import { AuthService } from "../../../core/services/auth.service";
import { RouterLink, RouterLinkActive } from "@angular/router";
import { CommonModule } from "@angular/common";

@Component({
  selector: 'app-navbar',
  imports: [RouterLink, RouterLinkActive, CommonModule],
  templateUrl: './navbar.html',
  styleUrl: './navbar.css',
})
export class Navbar {
  protected authService : AuthService = inject(AuthService);
}
