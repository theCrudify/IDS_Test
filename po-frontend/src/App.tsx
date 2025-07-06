import React from 'react';
import { BrowserRouter as Router, Routes, Route, Navigate } from 'react-router-dom';
import { ThemeProvider, createTheme } from '@mui/material/styles';
import { CssBaseline } from '@mui/material';
import { QueryClient, QueryClientProvider } from '@tanstack/react-query';

// Layouts
import MainLayout from './layouts/MainLayout';

// Pages
import Dashboard from './pages/dashboard/Dashboard';
import PurchaseOrderList from './pages/purchase-orders/PurchaseOrderList';
import CreatePurchaseOrder from './pages/purchase-orders/CreatePurchaseOrder';
import ViewPurchaseOrder from './pages/purchase-orders/ViewPurchaseOrder';
import ApprovalsPage from './pages/approvals/ApprovalsPage';

// Create theme
const theme = createTheme({
  palette: {
    primary: {
      main: '#1976d2',
    },
    secondary: {
      main: '#dc004e',
    },
  },
  typography: {
    fontFamily: '"Roboto", "Helvetica", "Arial", sans-serif',
  },
});

// Create QueryClient instance
const queryClient = new QueryClient({
  defaultOptions: {
    queries: {
      retry: 3,
      refetchOnWindowFocus: false,
    },
  },
});

function App() {
  return (
    <QueryClientProvider client={queryClient}>
      <ThemeProvider theme={theme}>
        <CssBaseline />
        <Router>
          <Routes>
            {/* Redirect root to dashboard */}
            <Route path="/" element={<Navigate to="/dashboard" replace />} />
            
            {/* Main application routes */}
            <Route path="/dashboard" element={
              <MainLayout>
                <Dashboard />
              </MainLayout>
            } />
            
            <Route path="/purchase-orders" element={
              <MainLayout>
                <PurchaseOrderList />
              </MainLayout>
            } />
            
            <Route path="/purchase-orders/create" element={
              <MainLayout>
                <CreatePurchaseOrder />
              </MainLayout>
            } />
            
            <Route path="/purchase-orders/:id" element={
              <MainLayout>
                <ViewPurchaseOrder />
              </MainLayout>
            } />
            
            <Route path="/purchase-orders/:id/edit" element={
              <MainLayout>
                <div>Edit Purchase Order Page (Coming Soon)</div>
              </MainLayout>
            } />
            
            <Route path="/approvals" element={
              <MainLayout>
                <ApprovalsPage />
              </MainLayout>
            } />
            
            <Route path="/approvals/:id" element={
              <MainLayout>
                <div>Approval Detail Page (Coming Soon)</div>
              </MainLayout>
            } />
            
            {/* Master Data Routes */}
            <Route path="/master-data/departments" element={
              <MainLayout>
                <div>Departments Page (Coming Soon)</div>
              </MainLayout>
            } />
            
            <Route path="/master-data/users" element={
              <MainLayout>
                <div>Users Page (Coming Soon)</div>
              </MainLayout>
            } />
            
            <Route path="/master-data/vendors" element={
              <MainLayout>
                <div>Vendors Page (Coming Soon)</div>
              </MainLayout>
            } />
            
            <Route path="/master-data/items" element={
              <MainLayout>
                <div>Items Page (Coming Soon)</div>
              </MainLayout>
            } />
            
            <Route path="/master-data/uoms" element={
              <MainLayout>
                <div>UOMs Page (Coming Soon)</div>
              </MainLayout>
            } />
            
            <Route path="/master-data/taxes" element={
              <MainLayout>
                <div>Taxes Page (Coming Soon)</div>
              </MainLayout>
            } />
            
            <Route path="/notifications" element={
              <MainLayout>
                <div>Notifications Page (Coming Soon)</div>
              </MainLayout>
            } />
            
            <Route path="/profile" element={
              <MainLayout>
                <div>Profile Page (Coming Soon)</div>
              </MainLayout>
            } />
            
            {/* Catch all route */}
            <Route path="*" element={
              <MainLayout>
                <div>
                  <h2>Page Not Found</h2>
                  <p>The page you are looking for does not exist.</p>
                </div>
              </MainLayout>
            } />
          </Routes>
        </Router>
      </ThemeProvider>
    </QueryClientProvider>
  );
}

export default App;