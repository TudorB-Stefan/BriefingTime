import { BriefingListModel } from "./briefing-list.model";
import { CommentDetailModel } from "./comment-detail.model";
import { DownloadLogListModel } from "./download-log-list.model";
import { SavedBriefingListModel } from "./saved-briefing-list.model";

export interface SelfModel {
  id: string;
  email: string;
  userName: string;
  firstName: string;
  lastName: string;
  createdAt: Date | string;
  modifiedAt: Date | string;
  uploadedBriefings: BriefingListModel[];
  downloadLogs: DownloadLogListModel[];
  savedBriefings: SavedBriefingListModel[];
  comments: CommentDetailModel[];
}
