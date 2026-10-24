
export interface Policy {
  id: number;
  policyNumber?: string;
  quoteId?: number;
  customerId?: number;
  customerName?: string;
  productName?: string;
  sumInsured?: number;
  premium?: number;
  issueDate?: string;
  effectiveDate?: string;
  expiryDate?: string;
  nextDueDate?: string;
  status?: string;
  nomineeName?: string;
  riders?: string[];
  documentReady?: boolean;
}
