import { Contact } from "./Contact";

export interface saveVehicle {
  id: number,
  makeId: number,
  modelId: number,
  isRegistered: boolean,
  features: number[],
  contact: Contact
}
