import { Component, inject, OnInit, signal } from '@angular/core';
import { BriefingService } from "../../../../core/services/briefing.service";
import { DepartmentService } from "../../../../core/services/department-service";
import {ActivatedRoute, Router } from "@angular/router";
import { BriefingEditModel } from "../../../models/briefing-edit.model";
import { FormsModule } from "@angular/forms";

@Component({
  selector: 'app-briefing-edit',
  imports: [FormsModule],
  templateUrl: './briefing-edit.html',
  styleUrl: './briefing-edit.css',
})
export class BriefingEdit implements OnInit {
  private briefingService = inject(BriefingService);
  private route = inject(ActivatedRoute);
  private departmentService = inject(DepartmentService);
  protected departments = signal<any[]>([]);
  private router = inject(Router);
  briefingId: string = '';
  creds: BriefingEditModel = {
    title: '',
    description: '',
    departmentId: ''
  };
  ngOnInit(){
    this.briefingId = this.route.snapshot.paramMap.get('id') || '';
    this.departmentService.getDepartments().subscribe({
      next: (data) => this.departments.set(data),
      error: (err) => console.error('Failed to load departments', err)
    })
  }
  submitBriefing() {

    this.briefingService.editBriefing(this.briefingId,this.creds).subscribe({
      next: () => {
        alert('Briefing edited successfully!');
        this.router.navigate(['/my-account']);
      },
      error: (err) => {
        console.error('Edit failed', err);
        alert(err.error?.message || 'Failed to edit the briefing.');
      }
    });
  }
}
