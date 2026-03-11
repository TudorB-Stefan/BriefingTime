import { HttpClient } from "@angular/common/http";
import {inject, Injectable, signal } from '@angular/core';
import { BriefingListModel } from "../../shared/models/briefing-list.model";
import { BriefingDetailModel } from "../../shared/models/briefing-detail.model";
import { BriefingCreateModel } from "../../shared/models/briefing-create.model";
import { DepartmentService } from "./department-service";

@Injectable({
  providedIn: 'root',
})
export class BriefingService {
  private http = inject(HttpClient);
  private baseUrl = 'http://localhost:8080/api/';
  getMyBriefings(){
    return this.http.get<BriefingListModel[]>(this.baseUrl + 'briefing/my-briefings');
  }
  getBriefingById(id: string){
    return this.http.get<BriefingDetailModel>(this.baseUrl + `briefing/${id}`);
  }
  createBriefing(model: BriefingCreateModel){
    const formData = new FormData();
    formData.append('title', model.title);
    formData.append('description', model.description);
    formData.append('departmentId', model.departmentId);
    if (model.File) {
      formData.append('File', model.File, model.File.name);
    }
    return this.http.post(this.baseUrl + 'briefing',formData);
  }
}
