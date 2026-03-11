import { Component, inject, OnInit, signal } from '@angular/core';
import { ActivatedRoute, RouterLink } from "@angular/router";
import { BriefingService } from "../../../../core/services/briefing.service";
import { BriefingDetailModel } from "../../../models/briefing-detail.model";
import { SavedService } from "../../../../core/services/saved.service";

@Component({
  selector: 'app-briefing-detail',
  imports: [RouterLink],
  templateUrl: './briefing-detail.html',
  styleUrl: './briefing-detail.css',
})
export class BriefingDetail implements OnInit {
  private route = inject(ActivatedRoute);
  private briefingService = inject(BriefingService);
  private savedService = inject(SavedService);
  protected briefing = signal<BriefingDetailModel|null>(null);
  protected isLoading = signal<boolean>(true);
  ngOnInit(){
    const id = this.route.snapshot.paramMap.get('id');
    if(id){
      this.briefingService.getBriefingById(id).subscribe({
        next: (data) => {
          this.briefing.set(data);
          this.isLoading.set(false);
        },
        error: (err) => {
          console.error(err);
          this.isLoading.set(false);
        }
      });
    }
  }
  saveBrief(id: string){
    this.savedService.createSave(id).subscribe({
      next: () => {
        alert('Briefing saved successfully!');
      },
      error: (err) => {
        console.error('Failed to save briefing', err);
        alert('Could not save the briefing.');
      }
    });
  }
  downlaodBrief(id: string, title: string){
    this.briefingService.downloadBriefing(id).subscribe({
    next: (blob: Blob) => {
      const url = window.URL.createObjectURL(blob);
      const a = document.createElement('a');
      a.href = url;
      a.download = `${title}.pdf`;
      document.body.appendChild(a);
      a.click();
      window.URL.revokeObjectURL(url);
      document.body.removeChild(a);
    },
    error: (err) => {
      console.error('Download failed', err);
      alert('Failed to download the PDF.');
    }
  });
  }
}
