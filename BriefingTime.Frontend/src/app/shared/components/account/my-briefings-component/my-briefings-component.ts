import { Component, inject, OnInit, signal } from '@angular/core';
import { BriefingService } from "../../../../core/services/briefing.service";
import { BriefingListModel } from "../../../models/briefing-list.model";
import { AuthService } from "../../../../core/services/auth.service";
import { RouterLink } from "@angular/router";

@Component({
  selector: 'app-my-briefings-component',
  imports: [RouterLink],
  templateUrl: './my-briefings-component.html',
  styleUrl: './my-briefings-component.css',
})
export class MyBriefingsComponent implements OnInit {
  protected briefingService:  BriefingService = inject(BriefingService);
  liveBriefings = signal<BriefingListModel[]>([]);
  isLoading = signal<boolean>(true);
  ngOnInit(){
    this.loadLiveBriefings();
  }

  loadLiveBriefings(){
    this.isLoading.set(true);
    this.briefingService.getMyBriefings().subscribe({
      next: (data) => {
        this.liveBriefings.set(data);
        this.isLoading.set(false);
      },
      error: (err) => {
        console.error('Failed to fetch publishings', err);
        this.isLoading.set(false);
      }
    });
  }
}
