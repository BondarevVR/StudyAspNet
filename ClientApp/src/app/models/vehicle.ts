import { Contact } from "./Contact";
import { KeyValuePair } from "./KeyValuePair";

export interface vehicle {
  id: number,
  make: KeyValuePair,
  model: KeyValuePair,
  isRegistered: boolean,
  features: KeyValuePair[],
  contact: Contact
}
