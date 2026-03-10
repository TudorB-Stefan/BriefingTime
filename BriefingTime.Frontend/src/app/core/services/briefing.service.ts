import { HttpClient } from "@angular/common/http";
import {inject, Injectable } from '@angular/core';
import { BriefingListModel } from "../../shared/models/briefing-list.model";
import { BriefingDetailModel } from "../../shared/models/briefing-detail.model";

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
}
