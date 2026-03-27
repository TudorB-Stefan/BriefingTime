import { Component, inject, signal } from '@angular/core';
import { AuthService } from "../../../../core/services/auth.service";
import { BriefingService } from "../../../../core/services/briefing.service";
import { MyBriefingsComponent } from "../my-briefings-component/my-briefings-component";
import { MyDownloadsComponent } from "../my-downloads-component/my-downloads-component";
import { MySavedComponent } from "../my-saved-component/my-saved-component";
import { DepartmentService } from "../../../../core/services/department-service";
import { Router } from "@angular/router";

@Component({
  selector: 'app-my-account',
  imports: [MyBriefingsComponent,MySavedComponent,MyDownloadsComponent],
  templateUrl: './my-account.html',
  styleUrl: './my-account.css',
})
export class MyAccount {
  protected router = inject(Router);
  private departmentService: DepartmentService = inject(DepartmentService);
  protected authService: AuthService = inject(AuthService);
  protected briefingService:  BriefingService = inject(BriefingService);
  protected departments = signal<any[]>([]);
  activeTab = signal<'publishings' | 'saved' | 'downloads'>('publishings');
  ngOnInit(){
    this.departmentService.getDepartments().subscribe({
      next: (data) => this.departments.set(data),
      error: (err) => console.error('Failed to load departments', err)
    })
  }
  deleteSelf(){
    const confirmDelete = confirm('Are you absolutely sure you want to delete your account? This action cannot be undone.');
    if (!confirmDelete) return;
    this.authService.deleteSelf().subscribe({
      next: (data) => {
        localStorage.removeItem('token');
        alert('Your account has been successfully deleted.');
        this.router.navigate(['/login']);
      },
      error: (err) => {
        console.error('Failed to delete account', err);
        alert(err.error?.message || 'Failed to delete your account. Please try again.');
      }
    })
  }
}
