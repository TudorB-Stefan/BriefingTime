import { Component, inject } from '@angular/core';
import { BriefingCreateModel } from "../../../models/briefing-create.model";
import { Router } from "@angular/router";
import { BriefingService } from "../../../../core/services/briefing.service";
import { FormsModule } from "@angular/forms";

@Component({
  selector: 'app-briefing-create',
  imports: [FormsModule],
  templateUrl: './briefing-create.html',
  styleUrl: './briefing-create.css',
})
export class BriefingCreate {
  private briefingService = inject(BriefingService);
  private router = inject(Router);
  creds: BriefingCreateModel = {
    title: '',
    description: '',
    departmentId: '',
    File: null as unknown as File
  };
  onFileSelected(event: any) {
    const file: File = event.target.files[0];
    if (file) {
      this.creds.File = file;
      console.log("File selected:", file.name);
    }
  }
  submitBriefing() {
    if (!this.creds.File) {
      alert('Please select a PDF file to upload.');
      return;
    }
    this.briefingService.createBriefing(this.creds).subscribe({
      next: () => {
        alert('Briefing published successfully!');
        this.router.navigate(['/my-account']);
      },
      error: (err) => {
        console.error('Upload failed', err);
        alert(err.error?.message || 'Failed to upload the briefing.');
      }
    });
  }
}
