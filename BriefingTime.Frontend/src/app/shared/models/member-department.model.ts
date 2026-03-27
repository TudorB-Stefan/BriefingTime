export interface MemberDepartmentModel {
  id: string;
  isAdmin: boolean;
  userName: string;
  firstName: string;
  lastName: string;
  createdAt: Date | string;
  departmentIds: string[];
}
