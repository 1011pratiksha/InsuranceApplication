export interface Customer {
  customerId: number;
  customerCode: string;
  firstName: string;
  middleName?: string;
  lastName: string;
  email: string;
  mobileNumber: string;
  occupation: string;
  customerStatus: string;
  createdAt?: string;
}

export interface CustomerDetail {
  customerId: number;
  customerCode: string;
  firstName: string;
  middleName?: string;
  lastName: string;
  dateOfBirth: string;
  gender: string;
  maritalStatus: string;
  occupation: string;
  annualIncome: number;
  mobileNumber: string;
  email: string;
  customerStatus: string;
  createdAt: string;
  updatedAt: string;
  addresses: CustomerAddress[];
  contacts: CustomerContact[];
  nominees: Nominee[];
  documents: CustomerDocument[];
}

export interface CreateCustomerRequest {
  firstName: string;
  middleName?: string;
  lastName: string;
  dateOfBirth: string;
  gender: string;
  maritalStatus: string;
  occupation: string;
  annualIncome: number;
  mobileNumber: string;
  email: string;
}

export interface UpdateCustomerRequest {
  firstName?: string;
  middleName?: string;
  lastName?: string;
  dateOfBirth?: string;
  gender?: string;
  maritalStatus?: string;
  occupation?: string;
  annualIncome?: number;
  mobileNumber?: string;
  email?: string;
  customerStatus?: string;
}

export interface CustomerAddress {
  addressId?: number;
  customerId?: number;
  addressType: string;
  addressLine1: string;
  addressLine2?: string;
  city: string;
  district: string;
  state: string;
  country: string;
  pincode: string;
}

export interface CreateAddressRequest {
  addressType: string;
  addressLine1: string;
  addressLine2?: string;
  city: string;
  district: string;
  state: string;
  country: string;
  pincode: string;
}

export interface CustomerContact {
  contactId?: number;
  customerId?: number;
  alternateMobile?: string;
  officePhone?: string;
  preferredContactMode: string;
}

export interface Nominee {
  nomineeId?: number;
  customerId?: number;
  nomineeName: string;
  relationship: string;
  dateOfBirth: string;
  mobileNumber?: string;
  sharePercentage: number;
  guardianName?: string;
}

export interface CreateNomineeRequest {
  nomineeName: string;
  relationship: string;
  dateOfBirth: string;
  mobileNumber?: string;
  sharePercentage: number;
  guardianName?: string;
}

export interface CustomerDocument {
  documentId?: number;
  customerId?: number;
  documentType: string;
  documentNumber?: string;
  fileName: string;
  filePath: string;
  uploadDate?: string;
  verificationStatus?: string;
}

export interface UploadDocumentRequest {
  documentType: string;
  documentNumber?: string;
  fileName: string;
  filePath: string;
}

export interface CustomerSearchResponse {
  message: string;
  data: Customer[];
}
