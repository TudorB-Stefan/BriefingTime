import { Component, inject, signal } from '@angular/core';
import { AuthService } from "../../../../core/services/auth.service";
import { BriefingService } from "../../../../core/services/briefing.service";
import { MyBriefingsComponent } from "../my-briefings-component/my-briefings-component";
import { MyDownloadsComponent } from "../my-downloads-component/my-downloads-component";
import { MySavedComponent } from "../my-saved-component/my-saved-component";

@Component({
  selector: 'app-my-account',
  imports: [MyBriefingsComponent,MySavedComponent,MyDownloadsComponent],
  templateUrl: './my-account.html',
  styleUrl: './my-account.css',
})
export class MyAccount {
  protected authService: AuthService = inject(AuthService);
  protected briefingService:  BriefingService = inject(BriefingService);
  activeTab = signal<'publishings' | 'saved' | 'downloads'>('publishings');
  ngOnInit(){
  }
}
