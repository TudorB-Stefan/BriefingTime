import {inject, Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { MemberDepartmentModel } from "../../shared/models/member-department.model";

@Injectable({
  providedIn: 'root',
})
export class MemberService {
  private http = inject(HttpClient);
  private baseUrl = 'http://localhost:8080/api/';
  getAllMembers(){
    return this.http.get<MemberDepartmentModel[]>(this.baseUrl + 'Member');
  }
}
