import { HttpClient } from "@angular/common/http";
import {inject, Injectable, signal } from '@angular/core';
import { BriefingListModel } from "../../shared/models/briefing-list.model";
import { BriefingDetailModel } from "../../shared/models/briefing-detail.model";
import { BriefingCreateModel } from "../../shared/models/briefing-create.model";
import { DepartmentService } from "./department-service";
import { DownloadService } from "./download.service";
import { BriefingEditModel } from "../../shared/models/briefing-edit.model";

@Injectable({
  providedIn: 'root',
})
export class BriefingService {
  private http = inject(HttpClient);
  private downlaodService = inject(DownloadService);
  private baseUrl = 'http://localhost:8080/api/';
  getAllBriefings(){
    return this.http.get<BriefingListModel[]>(this.baseUrl + 'briefing/my-list');
  }
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

  downloadBriefing(id: string) {
    this.downlaodService.createDownloadLog(id).subscribe({
      error: (err) => console.error('Failed to log the download silently:', err)
    });
    return this.http.get(`${this.baseUrl}briefing/${id}/download`, {
      responseType: 'blob'
    });
  }

  editBriefing(id:string, model: BriefingEditModel){
    const date = {
      title: model.title,
      description: model.description,
      departmentId: model.departmentId
    };
    return this.http.put(`${this.baseUrl}briefing/${id}`,date);
  }
  deleteBriefing(id: string){
    return this.http.delete(`${this.baseUrl}briefing/${id}`);
  }
}
