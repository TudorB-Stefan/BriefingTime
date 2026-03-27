import { HttpClient } from "@angular/common/http";
import {inject, Inject, Injectable, signal } from '@angular/core';
import { SelfModel } from "../../shared/models/self.model";
import { AuthResponseModel } from "../../shared/models/auth-response.model";
import { tap } from "rxjs";
import { Router } from "@angular/router";
import { jwtDecode } from 'jwt-decode';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private http = inject(HttpClient);
  private baseUrl = 'http://localhost:8080/api/';
  private router = inject(Router);
  public currentUser = signal<SelfModel | null>(this.getUserFromStorage());
  private getUserFromStorage(): SelfModel | null {
    const user = localStorage.getItem('user');
    try{
      return user ? JSON.parse(user) : null;
    }
    catch (e) {
      console.error("Storage corrupted", e);
      return null;
    }
  }
  register(creds: any){
    return this.http.post(this.baseUrl + 'auth/register',creds);
  }
  deleteSelf(){
    return this.http.delete(`${this.baseUrl}user/delete-me`)
  }
  login(creds: any){
    return this.http.post<AuthResponseModel>(this.baseUrl + 'auth/login',creds).pipe(
      tap(response => {
        if(response && response.selfDto) {
          localStorage.setItem('user',JSON.stringify(response.selfDto));
          localStorage.setItem('token', response.token);
          localStorage.setItem('refreshToken', response.refreshToken);
          this.currentUser.set(response.selfDto);
        }
      })
    )
  }
  logout(){
    localStorage.removeItem('user');
    localStorage.removeItem('token');
    this.currentUser.set(null);
    this.router.navigate(['/login']);
  }
  isAdmin():boolean{
    const token = localStorage.getItem('token');
    if (!token) return false;

    try {
      const decoded: any = jwtDecode(token);

      const roleKeys = [
        'role',
        'roles',
        'http://schemas.microsoft.com/ws/2008/06/identity/claims/role',
        'http://schemas.xmlsoap.org/ws/2005/05/identity/claims/role'
      ];

      const actualKey = roleKeys.find(key => decoded[key]);
      const roles = actualKey ? decoded[actualKey] : null;

      if (!roles) {
        console.warn('No roles found in token. Decoded token:', decoded);
        return false;
      }

      const roleArray = Array.isArray(roles) ? roles : [roles];
      return roleArray.some(r => r.toLowerCase() === 'admin');

    } catch (error) {
      console.error('Token decoding failed', error);
      return false;
    }
  }
}


