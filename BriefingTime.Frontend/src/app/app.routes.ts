import { Routes } from '@angular/router';
import { Login } from "./shared/components/login/login";
import { Register } from "./shared/components/register/register";

export const routes: Routes = [
  {path: 'login', component: Login, title: 'Log In'},
  {path: 'register', component: Register, title: 'Register'},
  {path: '', redirectTo: '/login', pathMatch: 'full'},
];
