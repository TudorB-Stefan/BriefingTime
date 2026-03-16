import { HttpClient } from "@angular/common/http";
import {inject, Injectable } from '@angular/core';
import { DownloadLogListModel } from "../../shared/models/download-log-list.model";
import { AuthService } from "./auth.service";

@Injectable({
  providedIn: 'root',
})
export class DownloadService {
  private http = inject(HttpClient);
  private baseUrl = 'http://localhost:8080/api/';
  getMyDownloadLogs(){
    return this.http.get<DownloadLogListModel[]>(this.baseUrl + 'DownloadLog/my-downloads');
  }
  createDownloadLog(id: string){
    return this.http.post(this.baseUrl + 'DownloadLog',{ briefingId: id  });
  }

}
