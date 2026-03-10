import { Component, inject, OnInit, signal } from '@angular/core';
import { ActivatedRoute } from "@angular/router";
import { BriefingService } from "../../../../core/services/briefing.service";
import { BriefingDetailModel } from "../../../models/briefing-detail.model";

@Component({
  selector: 'app-briefing-detail',
  imports: [],
  templateUrl: './briefing-detail.html',
  styleUrl: './briefing-detail.css',
})
export class BriefingDetail implements OnInit {
  private route = inject(ActivatedRoute);
  private briefingService = inject(BriefingService);
  briefing = signal<BriefingDetailModel|null>(null);
  isLoading = signal<boolean>(true);
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
}
