import { HttpClient } from "@angular/common/http";
import {inject, Injectable } from '@angular/core';
import { DepartmentModel } from "../../shared/models/department.model";

@Injectable({
  providedIn: 'root',
})
export class DepartmentService {
  private http = inject(HttpClient);
  private baseUrl = 'http://localhost:8080/api/';
  getDepartments(){
    return this.http.get<DepartmentModel[]>(this.baseUrl + 'department/my-departments');
  }
  getAllDepartments(){
    return this.http.get<DepartmentModel[]>(this.baseUrl + 'department');
  }
}
