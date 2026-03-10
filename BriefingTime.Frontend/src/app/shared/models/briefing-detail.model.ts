import { CommentDetailModel } from "./comment-detail.model";

export interface BriefingDetailModel {
  id: string;
  title: string;
  author: string;
  description: string;
  departmentName: string;
  exiresAt: Date | string;
  fileSizeByte: number;
  FileUrl: string;
  ContentType: string;
  comments: CommentDetailModel[];
}
