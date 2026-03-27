import { Component, inject, signal } from '@angular/core';
import { BriefingService } from "../../../../core/services/briefing.service";
import { BriefingListModel } from "../../../models/briefing-list.model";
import { AdminService } from "../../../../core/services/admin.service";
import { MemberService } from "../../../../core/services/member-service";
import { MemberDepartmentModel } from "../../../models/member-department.model";
import { DepartmentService } from "../../../../core/services/department-service";
import { DepartmentModel } from "../../../models/department.model";

@Component({
  selector: 'app-admin-controller',
  imports: [],
  templateUrl: './admin-controller.html',
  styleUrl: './admin-controller.css',
})
export class AdminController {
  private briefingService = inject(BriefingService);
  private departmentService = inject(DepartmentService);
  private memberService = inject(MemberService);
  private adminService = inject(AdminService);
  protected allBriefings = signal<BriefingListModel[]>([]);
  protected allUsers = signal<MemberDepartmentModel[]>([]);
  protected allDepartments = signal<DepartmentModel[]>([]);
  isLoading = signal<boolean>(true);
  ngOnInit(){
    this.getAllBriefings();
    this.getAllMembers();
    this.loadDepartments();
  }

  toggleAdminRole(user: MemberDepartmentModel){
    const currentlyAdmin = user.isAdmin;
    user.isAdmin = !currentlyAdmin;
    if (currentlyAdmin) {
      this.adminService.untoggleAdminRole(user.id).subscribe({
        next: () => console.log('Admin rights revoked'),
        error: (err) => {
          console.error('Failed to remove admin', err);
          user.isAdmin = true; // Rollback UI
          alert(err.error?.message || 'Could not revoke admin rights.');
        }
      });
    } else {
      this.adminService.toggleAdminRole(user.id).subscribe({
        next: () => console.log('Admin rights granted'),
        error: (err) => {
          console.error('Failed to grant admin', err);
          user.isAdmin = false;
          alert('Could not grant admin rights.');
        }
      });
    }
  }
  loadDepartments(){
    this.departmentService.getAllDepartments().subscribe({
      next: (data) => {
        this.allDepartments.set(data);
      },
      error: (err) => {
        console.error('Failed to load departments', err);
      }
    });
  }
  getAllMembers(){
    this.isLoading.set(true);
    this.memberService.getAllMembers().subscribe({
      next: (data) => {
        this.allUsers.set(data);
        this.isLoading.set(false);
      },
      error: (err) => {
        console.error('Failed to fetch briefings', err);
        this.isLoading.set(false);
      }
    });
  }
  getAllBriefings(){
    this.isLoading.set(true);
    this.briefingService.getAllBriefingsAdmin().subscribe({
      next: (data) => {
        this.allBriefings.set(data);
        this.isLoading.set(false);
      },
      error: (err) => {
        console.error('Failed to fetch briefings', err);
        this.isLoading.set(false);
      }
    });
  }
  deleteBriefing(id: string){
    const confirmDelete = confirm('Are you sure you want to delete this briefing?');
    if (!confirmDelete) return;
    this.isLoading.set(true);
    this.briefingService.deleteBriefing(id).subscribe({
      next: () => {
        this.allBriefings.update(currentBriefings =>
          currentBriefings.filter(briefing => briefing.id !== id)
        );
        this.isLoading.set(false);
      },
      error: (err) => {
        console.error('Failed to delete briefing', err);
        this.isLoading.set(false);
        alert('Could not delete the briefing. Please try again.');
      }
    });
  }
  userHasDept(user: MemberDepartmentModel, departmentId: string): boolean {
    return user.departmentIds?.includes(departmentId) ?? false;
  }
  toggleUserDepartment(user: MemberDepartmentModel, departmentId: string) {
    if (!user.departmentIds) {
      user.departmentIds = [];
    }
    const currentlyHasAccess = this.userHasDept(user, departmentId);

    if (currentlyHasAccess) {
      user.departmentIds = user.departmentIds.filter(id => id !== departmentId);
    } else {
      user.departmentIds.push(departmentId);
    }

    if (currentlyHasAccess) {
      this.adminService.untoggleDepartment(user.id, departmentId).subscribe({
        next: () => console.log(`Removed department ${departmentId} from user.`),
        error: (err) => {
          console.error('Failed to remove department access', err);
          user.departmentIds.push(departmentId);
          alert('Could not remove the department. Please try again.');
        }
      });

    } else {
      this.adminService.toggleDepartment(user.id, departmentId).subscribe({
        next: () => console.log(`Assigned department ${departmentId} to user.`),
        error: (err) => {
          console.error('Failed to assign department access', err);
          user.departmentIds = user.departmentIds.filter(id => id !== departmentId);
          alert('Could not assign the department. Please try again.');
        }
      });
    }
  }
}
