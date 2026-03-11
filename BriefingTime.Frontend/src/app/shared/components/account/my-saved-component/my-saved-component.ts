import { Component, inject, OnInit, signal } from '@angular/core';
import { SavedService } from "../../../../core/services/saved.service";
import { RouterLink } from "@angular/router";
import { SavedBriefingListModel } from "../../../models/saved-briefing-list.model";

@Component({
  selector: 'app-my-saved-component',
  imports: [RouterLink],
  templateUrl: './my-saved-component.html',
  styleUrl: './my-saved-component.css',
})
export class MySavedComponent implements OnInit {
  protected savedService = inject(SavedService);
  savedBriefings = signal<SavedBriefingListModel[]>([]);
  isLoading = signal<boolean>(true);
  ngOnInit(){
    this.loadLiveBriefings();
  }

  loadLiveBriefings(){
    this.isLoading.set(true);
    this.savedService.getMySaved().subscribe({
      next: (data) => {
        this.savedBriefings.set(data);
        this.isLoading.set(false);
      },
      error: (err) => {
        console.error('Failed to fetch publishings', err);
        this.isLoading.set(false);
      }
    });
  }
}
