import { Component, OnInit, signal } from '@angular/core';
import { inject } from "@angular/core";
import { RouterLink } from "@angular/router";
import { BriefingService } from "../../../../core/services/briefing.service";
import { BriefingListModel } from "../../../models/briefing-list.model";

@Component({
  selector: 'app-all-briefings',
  imports: [RouterLink],
  templateUrl: './all-briefings.html',
  styleUrl: './all-briefings.css',
})
export class AllBriefings implements OnInit {
  protected briefingService = inject(BriefingService);
  liveBriefings = signal<BriefingListModel[]>([]);
  isLoading = signal<boolean>(true);
  ngOnInit(){
    this.loadLiveBriefings();
  }
  loadLiveBriefings(){
    this.isLoading.set(true);
    this.briefingService.getAllBriefings().subscribe({
      next: (data) => {
        this.liveBriefings.set(data);
        this.isLoading.set(false);
      },
      error: (err) => {
        console.error('Failed to fetch briefings', err);
        this.isLoading.set(false);
      }
    });
  }
}
