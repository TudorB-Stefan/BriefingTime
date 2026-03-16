import { HttpClient } from "@angular/common/http";
import {inject, Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class DepartmentService {
  private http = inject(HttpClient);
  private baseUrl = 'http://localhost:8080/api/';
  getDepartments(){
    return this.http.get<any[]>(this.baseUrl + 'department');
  }
}
