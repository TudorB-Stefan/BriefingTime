import { Routes } from '@angular/router';
import { authGuard } from "./core/guards/auth-guard";
import { Login } from "./shared/components/auth/login/login";
import { Register } from "./shared/components/auth/register/register";
import { BriefingDetail } from "./shared/components/briefings/briefing-detail/briefing-detail";
import { MyAccount } from "./shared/components/account/my-account/my-account";
import { BriefingCreate } from "./shared/components/briefings/briefing-create/briefing-create";
import { BriefingEdit } from "./shared/components/briefings/briefing-edit/briefing-edit";

export const routes: Routes = [
  {path: 'login', component: Login, title: 'Log In'},
  {path: 'register', component: Register, title: 'Register'},
  {path: 'my-account', component: MyAccount, title: 'My Account', canActivate: [authGuard]},
  {path: 'create-briefing', component: BriefingCreate, title: 'Create Briefing', canActivate: [authGuard]},
  { path: 'briefing/:id', component: BriefingDetail, title: 'Briefing' },
  { path: 'edit/:id', component: BriefingEdit, title: 'Edit Briefing' },
  {path: '', redirectTo: '/login', pathMatch: 'full'},
];
