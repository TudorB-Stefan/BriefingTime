import { Component, inject, OnInit, signal } from '@angular/core';
import { DownloadService } from "../../../../core/services/download.service";
import { DownloadLogListModel } from "../../../models/download-log-list.model";
import { RouterLink } from "@angular/router";

@Component({
  selector: 'app-my-downloads-component',
  imports: [RouterLink],
  templateUrl: './my-downloads-component.html',
  styleUrl: './my-downloads-component.css',
})
export class MyDownloadsComponent implements OnInit {
  protected downloadService = inject(DownloadService);
  downloadedBriefings = signal<DownloadLogListModel[]>([]);
  isLoading = signal<boolean>(true);
  ngOnInit(){
    this.loadDownloadedBriefings();
  }

  loadDownloadedBriefings(){
    this.isLoading.set(true);
    this.downloadService.getMyDownloadLogs().subscribe({
      next: (data) => {
        this.downloadedBriefings.set(data);
        this.isLoading.set(false);
      },
      error: (err) => {
        console.error('Failed to fetch publishings', err);
        this.isLoading.set(false);
      }
    });
  }

}
