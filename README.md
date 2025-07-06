# Purchase Order Approval System - Backend API

🏢 **Enterprise Purchase Order Approval System untuk PT Kansai Paint Indonesia**

## 📋 Overview

Sistem backend lengkap untuk mengelola workflow approval Purchase Order dengan 3-level approval (Checker → Acknowledge → Approver), terintegrasi dengan SAP, dan dilengkapi dengan sistem notifikasi real-time.

## 🏗️ Architecture

```
PurchaseOrderApprovalSystem/
├── src/
│   ├── PO.API/                 # 🌐 Web API Layer (Controllers, Middleware)
│   ├── PO.Application/         # 💼 Business Logic Layer (Services, Use Cases)
│   ├── PO.Domain/              # 🏛️ Domain Layer (Entities, Enums, Interfaces)
│   ├── PO.Infrastructure/      # 🗄️ Data Access Layer (EF Core, Repositories)
│   └── PO.Shared/              # 🔗 Shared Layer (DTOs, Common Classes)
├── tests/                      # 🧪 Unit & Integration Tests
├── docs/                       # 📚 Documentation
└── docker/                     # 🐳 Docker Configuration
```

## 🛠️ Technology Stack

- **Framework**: .NET 8.0
- **Database**: MySQL 8.0+
- **ORM**: Entity Framework Core 8.0
- **API Documentation**: Swagger/OpenAPI
- **Validation**: FluentValidation
- **Mapping**: AutoMapper
- **Logging**: Serilog
- **Testing**: xUnit (planned)

## 🚀 Getting Started

### Prerequisites

- .NET 8.0 SDK
- MySQL 8.0+
- Visual Studio 2022 atau VS Code
- Git

### Installation

1. **Clone Repository**
   ```bash
   git clone [repository-url]
   cd PurchaseOrderApprovalSystem
   ```

2. **Update Database Configuration**
   ```bash
   # Edit src/PO.API/appsettings.json
   "ConnectionStrings": {
     "DefaultConnection": "Server=localhost;Database=PurchaseOrderDB;Uid=root;Pwd=YOUR_PASSWORD;CharSet=utf8mb4;"
   }
   ```

3. **Install EF Core Tools**
   ```bash
   dotnet tool install --global dotnet-ef
   ```

4. **Create Database**
   ```bash
   # From solution root directory
   dotnet ef migrations add InitialCreate --project src/PO.Infrastructure --startup-project src/PO.API
   dotnet ef database update --project src/PO.Infrastructure --startup-project src/PO.API
   ```

5. **Run Application**
   ```bash
   cd src/PO.API
   dotnet run
   ```

6. **Access Swagger UI**
   ```
   https://localhost:5001/swagger
   ```

## 📊 Database Schema

### Master Data Tables
- `master_roles` - User roles dan permissions
- `master_departments` - Departemen organisasi
- `master_users` - User dengan approval limits
- `master_vendors` - Supplier data lengkap
- `master_items` - Katalog produk/jasa
- `master_uoms` - Unit of measures
- `master_taxes` - Tax rates dan types
- `master_divisions` - Business divisions
- `master_approval_matrices` - Approval workflow rules

### Transaction Tables
- `t_po_headers` - Purchase Order utama
- `t_po_details` - Line items PO
- `t_po_approval_histories` - Audit trail approval
- `t_po_attachments` - File attachments
- `notifications` - Sistem notifikasi

## 🔄 Approval Workflow

### 3-Level Approval Process
1. **Checker (Level 1)** - Staff Purchasing
   - Validasi dokumen dan compliance
   - Check budget availability
   
2. **Acknowledge (Level 2)** - Assistant Manager
   - Strategic alignment review
   - Department impact assessment
   
3. **Approver (Level 3)** - Manager/GM
   - Final authority approval
   - e-Signature application

### Business Rules
- **0-50M IDR**: Staff → Asst Manager → Manager
- **50M-200M IDR**: Staff → Asst Manager → GM  
- **Above 200M IDR**: Staff → Manager → GM
- **Emergency PO**: Direct to GM with justification

## 📡 API Endpoints

### Master Data APIs
```http
# Roles Management
GET    /api/roles              # Get all roles
GET    /api/roles/paged        # Get paged roles
GET    /api/roles/{id}         # Get role by ID
POST   /api/roles              # Create new role
PUT    /api/roles/{id}         # Update role
DELETE /api/roles/{id}         # Delete role
GET    /api/roles/lookup       # Get roles for dropdown

# Similar patterns for:
# - /api/departments
# - /api/users  
# - /api/vendors
# - /api/items
# - /api/uoms
# - /api/taxes
# - /api/divisions
```

### Purchase Order APIs (Planned)
```http
# PO Management
GET    /api/purchase-orders           # Get POs with filters
POST   /api/purchase-orders           # Create new PO
GET    /api/purchase-orders/{id}      # Get PO details
PUT    /api/purchase-orders/{id}      # Update PO
DELETE /api/purchase-orders/{id}      # Delete PO

# PO Approval Workflow
POST   /api/purchase-orders/{id}/submit    # Submit for approval
POST   /api/purchase-orders/{id}/approve   # Approve PO
POST   /api/purchase-orders/{id}/reject    # Reject PO
GET    /api/purchase-orders/{id}/history   # Get approval history

# File Management
POST   /api/purchase-orders/{id}/attachments     # Upload files
GET    /api/purchase-orders/{id}/attachments     # List files
DELETE /api/purchase-orders/{id}/attachments/{fileId}  # Delete file
```

## 🔧 Configuration

### Environment Variables
```bash
ASPNETCORE_ENVIRONMENT=Development
DATABASE_CONNECTION_STRING=Server=localhost;Database=PurchaseOrderDB;...
JWT_SECRET_KEY=your-secret-key
SAP_API_ENDPOINT=http://sap-server:8000/sap/bc/rest/
```

### Application Settings
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=PurchaseOrderDB;Uid=root;Pwd=password;"
  },
  "FileUpload": {
    "MaxFileSize": 10485760,
    "AllowedExtensions": [".pdf", ".doc", ".docx", ".xls", ".xlsx"],
    "UploadPath": "uploads/po-attachments"
  },
  "SAP": {
    "Enabled": false,
    "BaseUrl": "http://sap-server:8000/sap/bc/rest/",
    "TimeoutSeconds": 30
  }
}
```

## 🧪 Testing

```bash
# Run unit tests
dotnet test

# Run with coverage
dotnet test --collect:"XPlat Code Coverage"
```

## 📝 Development Roadmap

### Phase 1: Foundation ✅
- [x] Project structure setup
- [x] Database configuration  
- [x] Entity models
- [x] Repository pattern
- [x] Master data CRUD APIs
- [x] Swagger documentation

### Phase 2: Core PO Features 🔄
- [ ] PO creation & management
- [ ] File attachment handling
- [ ] 3-level approval workflow
- [ ] Notifications system
- [ ] Email integration

### Phase 3: Advanced Features ⏳
- [ ] SAP integration
- [ ] JWT authentication
- [ ] Role-based authorization
- [ ] Reporting & analytics
- [ ] Mobile app support
- [ ] Real-time notifications

### Phase 4: Production Ready 📋
- [ ] Performance optimization
- [ ] Comprehensive testing
- [ ] CI/CD pipeline
- [ ] Monitoring & logging
- [ ] Documentation completion

## 🐛 Known Issues

- JWT authentication belum diimplementasi (menggunakan dummy user)
- File upload functionality dalam development
- SAP integration interface belum aktif
- Unit tests belum lengkap

## 🤝 Contributing

1. Fork the repository
2. Create feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to branch (`git push origin feature/AmazingFeature`)
5. Open Pull Request

## 📞 Support

- **Email**: it@kansaipaints.com
- **Documentation**: [Link to detailed docs]
- **Issue Tracker**: [GitHub Issues]

## 📄 License

This project is proprietary software of PT Kansai Paint Indonesia.

---

**🎯 Project Status**: Phase 1 Complete - Master Data APIs Ready
**🚀 Next Milestone**: Purchase Order Workflow Implementation
**📅 Last Updated**: July 2025# IDS_Test
