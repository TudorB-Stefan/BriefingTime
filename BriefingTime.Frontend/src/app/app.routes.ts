import { Routes } from '@angular/router';
import { Login } from "./shared/components/login/login";
import { Register } from "./shared/components/register/register";
import { MyAccount } from "./shared/components/my-account/my-account";
import { authGuard } from "./core/guards/auth-guard";

export const routes: Routes = [
  {path: 'login', component: Login, title: 'Log In'},
  {path: 'register', component: Register, title: 'Register'},
  {path: 'my-account', component: MyAccount, title: 'My Account', canActivate: [authGuard]},
  {path: '', redirectTo: '/login', pathMatch: 'full'},
];
