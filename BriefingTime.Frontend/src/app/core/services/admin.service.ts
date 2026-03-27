import {inject, Injectable } from '@angular/core';
import { MemberDepartmentModel } from "../../shared/models/member-department.model";
import { HttpClient } from "@angular/common/http";

@Injectable({
  providedIn: 'root',
})
export class AdminService {
  private http = inject(HttpClient);
  private baseUrl = 'http://localhost:8080/api/';
  public toggleAdminRole(userId: string){
    return this.http.post<string>(`${this.baseUrl}Admin/${userId}/make-admin`,{});
  }
  public untoggleAdminRole(userId: string){
    return this.http.delete<string>(`${this.baseUrl}Admin/${userId}/remove-admin`);
  }
  public toggleDepartment(userId: string, departmentId: string){
    return this.http.post<MemberDepartmentModel[]>(this.baseUrl + 'Admin/assign-department',{
      userId: userId,
      departmentId: departmentId
    });
  }
  public untoggleDepartment(userId: string, departmentId: string){
    return this.http.delete<MemberDepartmentModel[]>(`${this.baseUrl}Admin/department/${userId}/${departmentId}`);
  }
}
