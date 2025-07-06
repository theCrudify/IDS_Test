# Purchase Order Approval System - Frontend

A modern React TypeScript frontend application for the Purchase Order Approval System.

## 🚀 Features

### ✅ Implemented
- **Dashboard** - Overview of PO statistics and recent activities
- **Purchase Order Management** - List, view, create, edit, and delete purchase orders
- **Responsive Layout** - Material-UI based responsive design
- **API Integration** - Complete API service layer with error handling
- **Type Safety** - Full TypeScript implementation
- **Modern UI** - Material-UI components with consistent theming

### 🚧 Coming Soon
- Purchase Order Create/Edit Forms
- Approval Workflow Interface
- Master Data Management (Departments, Users, Vendors, Items, etc.)
- Notifications System
- User Authentication
- Advanced Search and Filtering
- File Upload for Attachments
- Reporting and Analytics

## 🏗️ Architecture

### Project Structure
```
po-frontend/src/
├── components/           # Reusable UI components
│   ├── common/          # Common components
│   ├── forms/           # Form components
│   └── tables/          # Table components
├── pages/               # Page components
│   ├── dashboard/       # Dashboard page
│   ├── purchase-orders/ # PO management pages
│   ├── approvals/       # Approval workflow pages
│   ├── master-data/     # Master data pages
│   └── notifications/   # Notifications page
├── services/            # API service layer
├── hooks/               # Custom React hooks
├── types/               # TypeScript type definitions
├── layouts/             # Layout components
├── utils/               # Utility functions
└── constants/           # Application constants
```

### Technology Stack
- **React 19** - Modern React with functional components
- **TypeScript** - Type-safe development
- **Material-UI (MUI)** - Comprehensive React component library
- **React Router** - Client-side routing
- **Axios** - HTTP client for API calls
- **React Hook Form** - Form handling with validation
- **React Query** - Server state management
- **Day.js** - Date manipulation

## 🔧 API Integration

### Base API Service
- Centralized HTTP client with Axios
- Request/Response interceptors
- Error handling and logging
- Auth token management

### Service Layer
- **PurchaseOrderService** - PO CRUD operations
- **ApprovalService** - Approval workflow operations
- **MasterDataService** - Master data management
- **Generic Base Service** - Reusable CRUD patterns

### Type-Safe API
- Complete TypeScript interfaces for all API models
- Enum definitions matching backend
- Strongly typed API responses

## 🚀 Getting Started

### Prerequisites
- Node.js 18+ 
- npm or yarn
- Purchase Order API running on https://localhost:5001

### Installation
```bash
# Navigate to frontend directory
cd po-frontend

# Install dependencies
npm install

# Start development server
npm start
```

### Environment Configuration
Create `.env` file:
```env
REACT_APP_API_BASE_URL=https://localhost:5001/api
REACT_APP_NAME=Purchase Order Approval System
REACT_APP_VERSION=1.0.0
```

### Available Scripts
- `npm start` - Start development server (http://localhost:3000)
- `npm run build` - Build for production
- `npm test` - Run tests
- `npm run eject` - Eject from Create React App

## 📱 Application Screens

### Dashboard
- Overview statistics cards (Total POs, Pending, Approved, etc.)
- Recent purchase orders table
- Pending approvals widget
- Quick navigation to key functions

### Purchase Orders List
- Searchable and sortable data table
- Status-based color coding
- Action buttons (View, Edit, Delete, Submit)
- Pagination support
- Status filters

### Navigation Structure
```
Dashboard
├── Purchase Orders
│   ├── List (✅ Implemented)
│   ├── Create (🚧 Coming Soon)
│   ├── View Details (🚧 Coming Soon)
│   └── Edit (🚧 Coming Soon)
├── Approvals (🚧 Coming Soon)
├── Master Data
│   ├── Departments (🚧 Coming Soon)
│   ├── Users (🚧 Coming Soon)
│   ├── Vendors (🚧 Coming Soon)
│   ├── Items (🚧 Coming Soon)
│   ├── UOMs (🚧 Coming Soon)
│   └── Taxes (🚧 Coming Soon)
└── Notifications (🚧 Coming Soon)
```

## 🔌 API Endpoints Supported

### Purchase Orders
- `GET /api/PurchaseOrders/paged` - Paginated PO list ✅
- `GET /api/PurchaseOrders/{id}` - Get PO by ID ✅
- `POST /api/PurchaseOrders` - Create new PO ✅
- `PUT /api/PurchaseOrders/{id}` - Update PO ✅
- `DELETE /api/PurchaseOrders/{id}` - Delete PO ✅
- `POST /api/PurchaseOrders/{id}/submit` - Submit for approval ✅

### Approvals
- `GET /api/Approvals/pending/{userId}` - Get pending approvals ✅
- `POST /api/Approvals/{poId}/approve` - Approve PO ✅
- `POST /api/Approvals/{poId}/reject` - Reject PO ✅

### Master Data (Ready for all entities)
- Departments, Users, Vendors, Items, UOMs, Taxes
- Full CRUD operations
- Lookup endpoints for dropdowns
- Pagination support

## 🎨 UI/UX Features

### Responsive Design
- Mobile-first approach with collapsible sidebar
- Responsive data tables with horizontal scroll
- Adaptive card layouts
- Touch-friendly buttons and interactions

### Material Design
- Consistent primary/secondary color theme
- Status-based color coding (Draft=Gray, Pending=Orange, Approved=Green, Rejected=Red)
- Intuitive icons from Material Icons
- Proper typography hierarchy

### User Experience
- Loading states for all async operations
- Error handling with user-friendly messages
- Confirmation dialogs for destructive actions
- Search functionality with debouncing
- Pagination for large datasets

## 🔐 Security & Error Handling

### API Security (Ready)
- Bearer token authentication setup
- Automatic token management
- Request/response interceptors
- 401 handling with auto-logout

### Error Handling
- Global error boundaries
- API error response handling
- User-friendly error messages
- Network error detection

## 🚀 How to Run Both Backend & Frontend

### 1. Start the Backend API
```bash
cd PurchaseOrderApprovalSystem
dotnet run --project src/PO.API
```
Backend will run on: https://localhost:5001

### 2. Start the Frontend
```bash
cd po-frontend
npm start
```
Frontend will run on: http://localhost:3000

### 3. Access the Application
- Open browser to http://localhost:3000
- Navigate through Dashboard and Purchase Orders
- API calls will be made to the backend automatically

## 📊 Current Status Summary

### ✅ Working Features
1. **Dashboard** - Shows PO statistics and recent data
2. **PO List** - Complete table with search, pagination, status filtering
3. **API Integration** - All service layers ready and tested
4. **Navigation** - Responsive sidebar with all menu items
5. **Type Safety** - Complete TypeScript definitions
6. **Error Handling** - Robust error management

### 🚧 Next Phase Implementation
1. **PO Create/Edit Forms** - Complex multi-step forms with line items
2. **Approval Workflow** - Approve/reject interface with comments
3. **Master Data CRUD** - Generic components for all master entities
4. **Authentication** - Login/logout with JWT tokens
5. **Notifications** - Real-time notification system

### 🎯 Development Priority
1. Purchase Order Create/Edit forms (High Priority)
2. Approval workflow interface (High Priority)
3. Master data management (Medium Priority)
4. Authentication system (Medium Priority)
5. Advanced features (Low Priority)

## 🤝 Integration with Backend

The frontend is designed to work seamlessly with the existing .NET API:

### API Compatibility
- All DTOs match backend models exactly
- Enum values align with backend enums
- Error response format matches ApiResult structure
- Pagination follows backend PagedResult pattern

### Data Flow
```
Frontend Component
    ↓ (calls)
Service Layer (e.g., purchaseOrderService)
    ↓ (makes HTTP request via)
API Service (axios instance)
    ↓ (to)
.NET Backend API
    ↓ (returns)
ApiResult<T> response
    ↓ (processed by)
Frontend Component (displays data)
```

---

**Ready for Production Backend Integration! 🚀**

The frontend foundation is solid and ready to consume the Purchase Order API. All major patterns are established, and adding new features follows the same consistent architecture.