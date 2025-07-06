// API Response Types
export interface ApiResult<T = any> {
  success: boolean;
  data?: T;
  message?: string;
  errors?: string[];
  statusCode: number;
  timestamp: string;
}

export interface PagedResult<T> {
  items: T[];
  totalCount: number;
  pageNumber: number;
  pageSize: number;
  totalPages: number;
  hasPreviousPage: boolean;
  hasNextPage: boolean;
}

export interface PagedRequest {
  pageNumber: number;
  pageSize: number;
  searchTerm?: string;
  sortField?: string;
  sortDirection?: 'asc' | 'desc';
}

// Purchase Order Types
export interface POHeaderDto {
  id: number;
  poNumber: string;
  poDate: string;
  postingDate: string;
  status: POStatus;
  poType: POType;
  vendorId: string;
  vendorName?: string;
  deptId: number;
  deptName?: string;
  createdById: number;
  createdByName?: string;
  notes?: string;
  deliveryAddress?: string;
  deliveryDate?: string;
  currency: string;
  exchangeRate: number;
  subTotal: number;
  taxAmount: number;
  totalDue: number;
  createdAt: string;
  updatedAt: string;
  poDetails: PODetailDto[];
}

export interface CreatePOHeaderDto {
  poDate: string;
  postingDate: string;
  poType: POType;
  vendorId: string;
  deptId: number;
  notes?: string;
  deliveryAddress?: string;
  deliveryDate?: string;
  currency: string;
  exchangeRate: number;
  poDetails: CreatePODetailDto[];
}

export interface UpdatePOHeaderDto {
  poDate: string;
  postingDate: string;
  poType: POType;
  vendorId: string;
  deptId: number;
  notes?: string;
  deliveryAddress?: string;
  deliveryDate?: string;
  currency: string;
  exchangeRate: number;
}

export interface PODetailDto {
  id: number;
  poId: number;
  lineNumber: number;
  itemId: number;
  itemCode: string;
  itemDescription: string;
  quantity: number;
  uomId: number;
  uomCode?: string;
  unitPrice: number;
  lineTotal: number;
  taxId: number;
  taxRate?: number;
  taxAmount: number;
  lineTotalIncludingTax: number;
  divisionId?: number;
  divisionName?: string;
}

export interface CreatePODetailDto {
  lineNumber: number;
  itemId: number;
  itemCode: string;
  itemDescription: string;
  quantity: number;
  uomId: number;
  unitPrice: number;
  taxId: number;
  divisionId?: number;
}

// Master Data Types
export interface DepartmentDto {
  id: number;
  deptCode: string;
  deptName: string;
  deptHeadId?: number;
  deptHeadName?: string;
  isActive: boolean;
  createdAt: string;
  updatedAt: string;
}

export interface CreateDepartmentDto {
  deptCode: string;
  deptName: string;
  deptHeadId?: number;
}

export interface UpdateDepartmentDto {
  deptCode: string;
  deptName: string;
  deptHeadId?: number;
  isActive: boolean;
}

export interface DepartmentLookupDto {
  id: number;
  deptCode: string;
  deptName: string;
}

export interface VendorDto {
  id: number;
  vendorId: string;
  vendorName: string;
  address?: string;
  phone?: string;
  email?: string;
  contactPerson?: string;
  taxId?: string;
  isActive: boolean;
  createdAt: string;
  updatedAt: string;
}

export interface CreateVendorDto {
  vendorId: string;
  vendorName: string;
  address?: string;
  phone?: string;
  email?: string;
  contactPerson?: string;
  taxId?: string;
}

export interface UpdateVendorDto {
  vendorId: string;
  vendorName: string;
  address?: string;
  phone?: string;
  email?: string;
  contactPerson?: string;
  taxId?: string;
  isActive: boolean;
}

export interface VendorLookupDto {
  id: number;
  vendorId: string;
  vendorName: string;
}

export interface ItemDto {
  id: number;
  itemCode: string;
  itemName: string;
  description?: string;
  itemType: ItemType;
  brand?: string;
  model?: string;
  specification?: string;
  defaultUOMId?: number;
  defaultUOMCode?: string;
  defaultTaxId?: number;
  defaultTaxRate?: number;
  standardPrice?: number;
  isActive: boolean;
  createdAt: string;
  updatedAt: string;
}

export interface CreateItemDto {
  itemCode: string;
  itemName: string;
  description?: string;
  itemType: ItemType;
  brand?: string;
  model?: string;
  specification?: string;
  defaultUOMId?: number;
  defaultTaxId?: number;
  standardPrice?: number;
}

export interface UpdateItemDto {
  itemCode: string;
  itemName: string;
  description?: string;
  itemType: ItemType;
  brand?: string;
  model?: string;
  specification?: string;
  defaultUOMId?: number;
  defaultTaxId?: number;
  standardPrice?: number;
  isActive: boolean;
}

export interface ItemLookupDto {
  id: number;
  itemCode: string;
  itemName: string;
  standardPrice?: number;
  defaultUOMId?: number;
  defaultTaxId?: number;
}

export interface UserDto {
  id: number;
  employeeCode: string;
  fullName: string;
  email: string;
  phone?: string;
  roleId: number;
  roleName?: string;
  deptId: number;
  deptName?: string;
  managerId?: number;
  managerName?: string;
  isActive: boolean;
  createdAt: string;
  updatedAt: string;
}

export interface CreateUserDto {
  employeeCode: string;
  fullName: string;
  email: string;
  phone?: string;
  roleId: number;
  deptId: number;
  managerId?: number;
}

export interface UpdateUserDto {
  employeeCode: string;
  fullName: string;
  email: string;
  phone?: string;
  roleId: number;
  deptId: number;
  managerId?: number;
  isActive: boolean;
}

export interface UserLookupDto {
  id: number;
  employeeCode: string;
  fullName: string;
  roleName?: string;
}

export interface UOMDto {
  id: number;
  uomCode: string;
  uomDescription: string;
  baseUnit: string;
  conversionFactor: number;
  isActive: boolean;
  createdAt: string;
  updatedAt: string;
}

export interface CreateUOMDto {
  uomCode: string;
  uomDescription: string;
  baseUnit: string;
  conversionFactor: number;
}

export interface UpdateUOMDto {
  uomCode: string;
  uomDescription: string;
  baseUnit: string;
  conversionFactor: number;
  isActive: boolean;
}

export interface UOMLookupDto {
  id: number;
  uomCode: string;
  uomDescription: string;
}

export interface TaxDto {
  id: number;
  taxCode: string;
  taxRate: number;
  taxDescription: string;
  taxType: string;
  isActive: boolean;
  createdAt: string;
  updatedAt: string;
}

export interface CreateTaxDto {
  taxCode: string;
  taxRate: number;
  taxDescription: string;
  taxType: string;
}

export interface UpdateTaxDto {
  taxCode: string;
  taxRate: number;
  taxDescription: string;
  taxType: string;
  isActive: boolean;
}

export interface TaxLookupDto {
  id: number;
  taxCode: string;
  taxRate: number;
  taxDescription: string;
}

// Approval Types
export interface ApprovalRequestDto {
  notes?: string;
  reason?: string;
}

// Notification Types
export interface NotificationDto {
  id: number;
  userId: number;
  type: NotificationType;
  priority: NotificationPriority;
  title: string;
  message?: string;
  isRead: boolean;
  readAt?: string;
  relatedEntityId?: number;
  actionUrl?: string;
  createdAt: string;
}

// Enums
export enum POStatus {
  Draft = 0,
  PendingApprovalLevel1 = 1,
  PendingApprovalLevel2 = 2,
  PendingApprovalLevel3 = 3,
  Approved = 4,
  Rejected = 5,
  ReopenedForCorrection = 6
}

export enum POType {
  Local = 0,
  Import = 1
}

export enum ItemType {
  Barang = 0,
  Jasa = 1
}

export enum NotificationType {
  Info = 0,
  Warning = 1,
  Error = 2,
  Success = 3
}

export enum NotificationPriority {
  Low = 0,
  Medium = 1,
  High = 2
}

export enum ApprovalLevel {
  None = 0,
  Checker = 1,
  Acknowledge = 2,
  Approver = 3
}

export enum ApprovalStatus {
  Pending = 0,
  Approved = 1,
  Rejected = 2
}