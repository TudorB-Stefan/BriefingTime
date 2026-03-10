import { SelfModel } from "./self.model";

export interface AuthResponseModel {
  token: string;
  refreshToken: string;
  refreshTokenExpiry: string;
  selfDto: SelfModel;
}
