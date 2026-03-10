import { HttpClient } from "@angular/common/http";
import {inject, Inject, Injectable, signal } from '@angular/core';
import { SelfModel } from "../../shared/models/self.model";
import { AuthResponseModel } from "../../shared/models/auth-response.model";
import { tap } from "rxjs";
import { Router } from "@angular/router";

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
}
