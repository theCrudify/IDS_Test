export const API_BASE_URL = process.env.REACT_APP_API_BASE_URL || 'https://localhost:5001/api';

export const API_ENDPOINTS = {
  // Purchase Orders
  PURCHASE_ORDERS: '/PurchaseOrders',
  PURCHASE_ORDER_BY_ID: (id: number) => `/PurchaseOrders/${id}`,
  PURCHASE_ORDER_SUBMIT: (id: number) => `/PurchaseOrders/${id}/submit`,
  
  // Approvals
  APPROVALS: '/Approvals',
  APPROVE_PO: (poId: number) => `/Approvals/${poId}/approve`,
  REJECT_PO: (poId: number) => `/Approvals/${poId}/reject`,
  PENDING_APPROVALS: (userId: number) => `/Approvals/pending/${userId}`,
  
  // Master Data - Departments
  DEPARTMENTS: '/Departments',
  DEPARTMENTS_PAGED: '/Departments/paged',
  DEPARTMENT_BY_ID: (id: number) => `/Departments/${id}`,
  DEPARTMENTS_LOOKUP: '/Departments/lookup',
  
  // Master Data - Vendors
  VENDORS: '/Vendors',
  VENDORS_PAGED: '/Vendors/paged',
  VENDOR_BY_ID: (id: number) => `/Vendors/${id}`,
  VENDORS_LOOKUP: '/Vendors/lookup',
  
  // Master Data - Items
  ITEMS: '/Items',
  ITEMS_PAGED: '/Items/paged',
  ITEM_BY_ID: (id: number) => `/Items/${id}`,
  ITEMS_LOOKUP: '/Items/lookup',
  
  // Master Data - Users
  USERS: '/Users',
  USERS_PAGED: '/Users/paged',
  USER_BY_ID: (id: number) => `/Users/${id}`,
  USERS_LOOKUP: '/Users/lookup',
  
  // Master Data - UOMs
  UOMS: '/UOMs',
  UOMS_PAGED: '/UOMs/paged',
  UOM_BY_ID: (id: number) => `/UOMs/${id}`,
  UOMS_LOOKUP: '/UOMs/lookup',
  
  // Master Data - Taxes
  TAXES: '/Taxes',
  TAXES_PAGED: '/Taxes/paged',
  TAX_BY_ID: (id: number) => `/Taxes/${id}`,
  TAXES_LOOKUP: '/Taxes/lookup',
  
  // Master Data - Divisions
  DIVISIONS: '/Divisions',
  DIVISIONS_PAGED: '/Divisions/paged',
  DIVISION_BY_ID: (id: number) => `/Divisions/${id}`,
  DIVISIONS_LOOKUP: '/Divisions/lookup',
  
  // Master Data - Roles
  ROLES: '/Roles',
  ROLES_PAGED: '/Roles/paged',
  ROLE_BY_ID: (id: number) => `/Roles/${id}`,
  ROLES_LOOKUP: '/Roles/lookup',
  
  // Notifications
  NOTIFICATIONS: '/Notifications',
  USER_NOTIFICATIONS: (userId: number, unreadOnly?: boolean) => 
    `/Notifications/user/${userId}${unreadOnly ? '?unreadOnly=true' : ''}`,
  MARK_NOTIFICATION_READ: (notificationId: number) => `/Notifications/${notificationId}/read`,
  
  // Test
  TEST: '/Test',
  HEALTH: '/Test/health'
} as const;

export const DEFAULT_PAGE_SIZE = 10;
export const DEFAULT_PAGE_NUMBER = 1;

export const HTTP_STATUS_CODES = {
  OK: 200,
  CREATED: 201,
  NO_CONTENT: 204,
  BAD_REQUEST: 400,
  UNAUTHORIZED: 401,
  FORBIDDEN: 403,
  NOT_FOUND: 404,
  CONFLICT: 409,
  UNPROCESSABLE_ENTITY: 422,
  INTERNAL_SERVER_ERROR: 500
} as const;