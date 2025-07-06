# Purchase Order Approval System - Setup Guide

🏢 **Enterprise Purchase Order Approval System untuk PT Kansai Paint Indonesia**

## 📋 Table of Contents

- [System Requirements](#system-requirements)
- [Installation](#installation)
- [Configuration](#configuration)
- [Running the Application](#running-the-application)
- [API Endpoints](#api-endpoints)
- [Business Rules](#business-rules)
- [Database Setup](#database-setup)
- [Troubleshooting](#troubleshooting)

## 🔧 System Requirements

### Prerequisites
- **.NET 8.0 SDK** (Download from [dotnet.microsoft.com](https://dotnet.microsoft.com/download))
- **MySQL 8.0+** (Local installation or cloud instance)
- **Git** for version control
- **Code Editor**: Visual Studio 2022, VS Code, atau JetBrains Rider

### Development Environment
- **OS**: Windows 10+, macOS, atau Linux
- **RAM**: Minimum 4GB (Recommended 8GB+)
- **Storage**: 2GB free space

## 🚀 Installation

### 1. Clone Repository
```bash
git clone [repository-url]
cd PurchaseOrderApprovalSystem
```

### 2. Install .NET Dependencies
```bash
# Restore packages for all projects
dotnet restore

# Install Entity Framework CLI tools (if not already installed)
dotnet tool install --global dotnet-ef
```

### 3. MySQL Database Setup
```sql
-- Create database
CREATE DATABASE TestCase;

-- Create user (optional, dapat menggunakan user existing)
CREATE USER 'jidan'@'localhost' IDENTIFIED BY 'qwer';
GRANT ALL PRIVILEGES ON TestCase.* TO 'jidan'@'localhost';
FLUSH PRIVILEGES;
```

## ⚙️ Configuration

### 1. Database Connection String
Edit file `src/PO.API/appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "server=localhost;port=3306;user=jidan;password=qwer;database=TestCase;"
  }
}
```

**Parameter yang dapat disesuaikan:**
- `server`: MySQL server address (default: localhost)
- `port`: MySQL port (default: 3306)
- `user`: MySQL username
- `password`: MySQL password
- `database`: Database name

### 2. Application Settings
```json
{
  "ApiSettings": {
    "Title": "Purchase Order Approval API",
    "Version": "v1",
    "Description": "API for managing Purchase Order approval workflow"
  },
  "CORS": {
    "AllowedOrigins": [
      "http://localhost:5000",
      "http://localhost:5001"
    ],
    "AllowedMethods": ["GET", "POST", "PUT", "DELETE", "OPTIONS"],
    "AllowedHeaders": ["*"],
    "AllowCredentials": true
  }
}
```

### 3. Port Configuration
File `src/PO.API/Properties/launchSettings.json`:

```json
{
  "profiles": {
    "http": {
      "applicationUrl": "http://localhost:5000"
    },
    "https": {
      "applicationUrl": "https://localhost:5001;http://localhost:5000"
    }
  }
}
```

## 🏃‍♂️ Running the Application

### Method 1: Command Line
```bash
# Navigate to API project
cd src/PO.API

# Run the application
dotnet run
```

### Method 2: Background Process
```bash
# Run in background
cd src/PO.API
nohup dotnet run > api.log 2>&1 &

# Check if running
ps aux | grep "dotnet run"
```

### Method 3: Production Build
```bash
# Build for production
dotnet build --configuration Release

# Run production build
dotnet run --configuration Release
```

### 🔍 Verify Installation
```bash
# Test API endpoints
curl http://localhost:5000/api/test
curl http://localhost:5000/health

# Expected responses:
# /api/test: {"success":true,"message":"Purchase Order API is running successfully!"}
# /health: "Healthy"
```

## 🌐 API Endpoints

### Base URL
```
Development: http://localhost:5000
```

### Available Endpoints

#### Health & Status
```http
GET /health                    # System health check
GET /api/test                  # API test endpoint
GET /api/test/health           # Detailed health information
```

#### Documentation
```http
GET /swagger                   # Swagger UI (Auto-generated API docs)
GET /swagger/v1/swagger.json   # OpenAPI specification
```

#### Master Data (Coming Soon)
```http
GET    /api/roles              # Get all roles
POST   /api/roles              # Create new role
PUT    /api/roles/{id}         # Update role
DELETE /api/roles/{id}         # Delete role
```

## 📋 Business Rules

### 🔄 Purchase Order Approval Workflow

#### 3-Level Approval Process
1. **Checker (Level 1)** - Staff Purchasing
   - Validasi dokumen dan compliance
   - Check budget availability
   - Verify vendor information

2. **Acknowledge (Level 2)** - Assistant Manager
   - Strategic alignment review
   - Department impact assessment
   - Resource availability check

3. **Approver (Level 3)** - Manager/GM
   - Final authority approval
   - e-Signature application
   - Budget final approval

#### Approval Limits by Amount
| Amount Range | Approval Path |
|-------------|---------------|
| **0 - 50M IDR** | Staff → Assistant Manager → Manager |
| **50M - 200M IDR** | Staff → Assistant Manager → GM |
| **Above 200M IDR** | Staff → Manager → GM |
| **Emergency PO** | Direct to GM with justification |

#### Status Flow
```
Draft → Submitted → Checking → Acknowledge → Approved → Sent to SAP
                              ↓
                           Rejected (with reason)
```

### 🔐 User Roles & Permissions

#### Role Types
- **Admin**: Full system access
- **Manager**: Department-level approval authority
- **Assistant Manager**: Mid-level approval authority
- **Staff**: Create and submit POs
- **Viewer**: Read-only access

#### Permission Matrix
| Action | Admin | Manager | Asst Manager | Staff | Viewer |
|--------|-------|---------|--------------|-------|--------|
| Create PO | ✅ | ✅ | ✅ | ✅ | ❌ |
| Approve PO | ✅ | ✅ | ✅ | ❌ | ❌ |
| View All POs | ✅ | ✅ | ✅ | Dept Only | Dept Only |
| Manage Users | ✅ | ❌ | ❌ | ❌ | ❌ |
| System Config | ✅ | ❌ | ❌ | ❌ | ❌ |

### 📊 Master Data Rules

#### Vendor Management
- Unique Vendor ID required
- Vendor must be active for PO creation
- Vendor performance tracking
- Payment terms validation

#### Item Catalog
- Unique Item Code from SAP
- Item type: Barang (Goods) or Jasa (Services)
- UOM (Unit of Measure) validation
- Tax rate association

## 💾 Database Setup

### Automatic Migration
The application automatically applies database migrations on startup in Development mode.

### Manual Migration
```bash
# Add new migration
dotnet ef migrations add MigrationName --project src/PO.Infrastructure --startup-project src/PO.API

# Update database
dotnet ef database update --project src/PO.Infrastructure --startup-project src/PO.API

# Reset database (CAUTION: This will delete all data)
dotnet ef database drop --project src/PO.Infrastructure --startup-project src/PO.API
dotnet ef database update --project src/PO.Infrastructure --startup-project src/PO.API
```

### Database Schema
```sql
-- Master Data Tables
master_roles
master_departments
master_users
master_vendors
master_items
master_uoms
master_taxes
master_divisions
master_approval_matrices

-- Transaction Tables
t_po_headers
t_po_details
t_po_approval_histories
t_po_attachments
notifications
```

## 🐛 Troubleshooting

### Common Issues

#### 1. Database Connection Failed
```
Error: Access denied for user 'root'@'localhost'
```
**Solution:**
- Check MySQL service is running
- Verify username/password in connection string
- Check database exists
- Verify user permissions

#### 2. Port Already in Use
```
Error: Address already in use
```
**Solution:**
```bash
# Check what's using the port
ss -tulpn | grep :5000

# Kill process using the port
sudo kill -9 <PID>

# Or change port in launchSettings.json
```

#### 3. Migration Issues
```
Error: The relationship cannot target the primary key
```
**Solution:**
- Check Entity Framework configurations
- Verify foreign key relationships
- Run `dotnet ef database drop` and recreate

#### 4. Missing Dependencies
```
Error: Could not find project or package
```
**Solution:**
```bash
# Clean and restore
dotnet clean
dotnet restore
dotnet build
```

### Performance Optimization

#### Development Mode
```bash
# Run with specific environment
ASPNETCORE_ENVIRONMENT=Development dotnet run
```

#### Production Mode
```bash
# Set production environment
ASPNETCORE_ENVIRONMENT=Production dotnet run
```

### Logging

#### Log Locations
- **Console**: Real-time logs during development
- **File**: `logs/po-api-YYYY-MM-DD.log`
- **Database**: Error logs stored in database (future feature)

#### Log Levels
```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning",
      "Microsoft.EntityFrameworkCore.Database.Command": "Information"
    }
  }
}
```

## 📞 Support & Contact

### Development Team
- **Email**: it@kansaipaints.com
- **Project Lead**: [Name]
- **Technical Lead**: [Name]

### Documentation
- **API Documentation**: http://localhost:5000/swagger
- **Database Schema**: Located in `/docs/database-schema.md`
- **Architecture Guide**: Located in `/docs/architecture.md`

### Issue Reporting
1. Create detailed issue description
2. Include error logs
3. Specify environment (Development/Production)
4. Include steps to reproduce
5. Submit to project issue tracker

---

**🎯 Current Status**: Phase 1 Complete - Core API & Master Data Ready  
**🚀 Next Phase**: Purchase Order Workflow Implementation  
**📅 Last Updated**: July 2025

---

## 📝 Quick Start Checklist

- [ ] Install .NET 8.0 SDK
- [ ] Install MySQL 8.0+
- [ ] Clone repository
- [ ] Configure database connection string
- [ ] Run `dotnet restore`
- [ ] Run `dotnet run` from `/src/PO.API`
- [ ] Test endpoints: `curl http://localhost:5000/api/test`
- [ ] Access Swagger: `http://localhost:5000/swagger`
- [ ] Verify health: `curl http://localhost:5000/health`

**Happy Development! 🚀**