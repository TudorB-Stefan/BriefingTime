import { HttpClient } from "@angular/common/http";
import {inject, Injectable } from '@angular/core';
import { SavedBriefingListModel } from "../../shared/models/saved-briefing-list.model";

@Injectable({
  providedIn: 'root',
})
export class SavedService {
  private http = inject(HttpClient);
  private baseUrl = 'http://localhost:8080/api/';
  getMySaved(){
    return this.http.get<SavedBriefingListModel[]>(this.baseUrl + 'SavedBriefing/my-saved');
  }
  createSave(id: string){
    return this.http.post(this.baseUrl + 'SavedBriefing',{ briefingId: id });
  }
  deleteSave(id: string){
    return this.http.delete(`${this.baseUrl}SavedBriefing/${id}`);
  }
}
