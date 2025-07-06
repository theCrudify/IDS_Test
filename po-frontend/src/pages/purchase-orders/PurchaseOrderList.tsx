import React, { useState, useEffect } from 'react';
import {
  Box,
  Card,
  CardContent,
  Typography,
  Button,
  Table,
  TableBody,
  TableCell,
  TableContainer,
  TableHead,
  TableRow,
  Paper,
  Chip,
  IconButton,
  TextField,
  Grid,
  TablePagination,
  Dialog,
  DialogTitle,
  DialogContent,
  DialogActions,
} from '@mui/material';
import {
  Add,
  Visibility,
  Edit,
  Delete,
  Send,
  Search,
} from '@mui/icons-material';
import { useNavigate } from 'react-router-dom';
import { POStatus, POHeaderDto, PagedRequest, PagedResult } from '../../types/api.types';
import { purchaseOrderService } from '../../services/purchase-order.service';

const PurchaseOrderList: React.FC = () => {
  const navigate = useNavigate();
  const [purchaseOrders, setPurchaseOrders] = useState<POHeaderDto[]>([]);
  const [loading, setLoading] = useState(true);
  const [deleteDialogOpen, setDeleteDialogOpen] = useState(false);
  const [selectedPO, setSelectedPO] = useState<POHeaderDto | null>(null);
  
  // Pagination state
  const [page, setPage] = useState(0);
  const [rowsPerPage, setRowsPerPage] = useState(10);
  const [totalCount, setTotalCount] = useState(0);
  
  // Search state
  const [searchTerm, setSearchTerm] = useState('');

  useEffect(() => {
    fetchPurchaseOrders();
  }, [page, rowsPerPage, searchTerm]);

  const fetchPurchaseOrders = async () => {
    try {
      setLoading(true);
      
      const request: PagedRequest = {
        pageNumber: page + 1, // API uses 1-based pagination
        pageSize: rowsPerPage,
        searchTerm: searchTerm || undefined,
        sortField: 'CreatedAt',
        sortDirection: 'desc',
      };
      
      const result = await purchaseOrderService.getPurchaseOrdersPaged(request);
      
      if (result.success && result.data) {
        setPurchaseOrders(result.data.items);
        setTotalCount(result.data.totalCount);
      } else {
        console.error('Failed to fetch purchase orders:', result.message);
      }
    } catch (error) {
      console.error('Error fetching purchase orders:', error);
    } finally {
      setLoading(false);
    }
  };

  const handleChangePage = (event: unknown, newPage: number) => {
    setPage(newPage);
  };

  const handleChangeRowsPerPage = (event: React.ChangeEvent<HTMLInputElement>) => {
    setRowsPerPage(parseInt(event.target.value, 10));
    setPage(0);
  };

  const handleSearch = (event: React.ChangeEvent<HTMLInputElement>) => {
    setSearchTerm(event.target.value);
    setPage(0); // Reset to first page when searching
  };

  const handleCreatePO = () => {
    navigate('/purchase-orders/create');
  };

  const handleViewPO = (id: number) => {
    navigate(`/purchase-orders/${id}`);
  };

  const handleEditPO = (id: number) => {
    navigate(`/purchase-orders/${id}/edit`);
  };

  const handleDeletePO = (po: POHeaderDto) => {
    setSelectedPO(po);
    setDeleteDialogOpen(true);
  };

  const confirmDelete = async () => {
    if (!selectedPO) return;
    
    try {
      const result = await purchaseOrderService.deletePurchaseOrder(selectedPO.id);
      if (result.success) {
        fetchPurchaseOrders(); // Refresh the list
      } else {
        console.error('Failed to delete purchase order:', result.message);
      }
    } catch (error) {
      console.error('Error deleting purchase order:', error);
    } finally {
      setDeleteDialogOpen(false);
      setSelectedPO(null);
    }
  };

  const handleSubmitPO = async (id: number) => {
    try {
      const result = await purchaseOrderService.submitPurchaseOrder(id);
      if (result.success) {
        fetchPurchaseOrders(); // Refresh the list
      } else {
        console.error('Failed to submit purchase order:', result.message);
      }
    } catch (error) {
      console.error('Error submitting purchase order:', error);
    }
  };

  const getStatusColor = (status: POStatus): 'default' | 'primary' | 'secondary' | 'error' | 'info' | 'success' | 'warning' => {
    switch (status) {
      case POStatus.Draft:
        return 'default';
      case POStatus.PendingApprovalLevel1:
      case POStatus.PendingApprovalLevel2:
      case POStatus.PendingApprovalLevel3:
        return 'warning';
      case POStatus.Approved:
        return 'success';
      case POStatus.Rejected:
        return 'error';
      case POStatus.ReopenedForCorrection:
        return 'info';
      default:
        return 'default';
    }
  };

  const getStatusText = (status: POStatus): string => {
    switch (status) {
      case POStatus.Draft:
        return 'Draft';
      case POStatus.PendingApprovalLevel1:
        return 'Pending Level 1';
      case POStatus.PendingApprovalLevel2:
        return 'Pending Level 2';
      case POStatus.PendingApprovalLevel3:
        return 'Pending Level 3';
      case POStatus.Approved:
        return 'Approved';
      case POStatus.Rejected:
        return 'Rejected';
      case POStatus.ReopenedForCorrection:
        return 'Reopened';
      default:
        return 'Unknown';
    }
  };

  const formatCurrency = (amount: number): string => {
    return new Intl.NumberFormat('id-ID', {
      style: 'currency',
      currency: 'IDR',
    }).format(amount);
  };

  const formatDate = (dateString: string): string => {
    return new Date(dateString).toLocaleDateString('id-ID');
  };

  const canEdit = (status: POStatus): boolean => {
    return status === POStatus.Draft || status === POStatus.ReopenedForCorrection;
  };

  const canSubmit = (status: POStatus): boolean => {
    return status === POStatus.Draft || status === POStatus.ReopenedForCorrection;
  };

  const canDelete = (status: POStatus): boolean => {
    return status === POStatus.Draft;
  };

  return (
    <Box>
      <Box display="flex" justifyContent="space-between" alignItems="center" mb={3}>
        <Typography variant="h4">Purchase Orders</Typography>
        <Button
          variant="contained"
          startIcon={<Add />}
          onClick={handleCreatePO}
        >
          Create Purchase Order
        </Button>
      </Box>

      <Card>
        <CardContent>
          {/* Search and Filters */}
          <Grid container spacing={2} sx={{ mb: 3 }}>
            <Grid item xs={12} md={6}>
              <TextField
                fullWidth
                variant="outlined"
                placeholder="Search by PO Number, Vendor..."
                value={searchTerm}
                onChange={handleSearch}
                InputProps={{
                  startAdornment: <Search sx={{ mr: 1, color: 'text.secondary' }} />,
                }}
              />
            </Grid>
          </Grid>

          {/* Purchase Orders Table */}
          <TableContainer component={Paper} variant="outlined">
            <Table>
              <TableHead>
                <TableRow>
                  <TableCell>PO Number</TableCell>
                  <TableCell>Date</TableCell>
                  <TableCell>Vendor</TableCell>
                  <TableCell>Department</TableCell>
                  <TableCell>Total Amount</TableCell>
                  <TableCell>Status</TableCell>
                  <TableCell>Actions</TableCell>
                </TableRow>
              </TableHead>
              <TableBody>
                {loading ? (
                  <TableRow>
                    <TableCell colSpan={7} align="center">
                      <Typography>Loading...</Typography>
                    </TableCell>
                  </TableRow>
                ) : purchaseOrders.length === 0 ? (
                  <TableRow>
                    <TableCell colSpan={7} align="center">
                      <Typography color="textSecondary">
                        No purchase orders found
                      </Typography>
                    </TableCell>
                  </TableRow>
                ) : (
                  purchaseOrders.map((po) => (
                    <TableRow key={po.id} hover>
                      <TableCell>
                        <Typography variant="body2" fontWeight="medium">
                          {po.poNumber}
                        </Typography>
                      </TableCell>
                      <TableCell>{formatDate(po.poDate)}</TableCell>
                      <TableCell>{po.vendorName || po.vendorId}</TableCell>
                      <TableCell>{po.deptName}</TableCell>
                      <TableCell>{formatCurrency(po.totalDue)}</TableCell>
                      <TableCell>
                        <Chip
                          label={getStatusText(po.status)}
                          color={getStatusColor(po.status)}
                          size="small"
                        />
                      </TableCell>
                      <TableCell>
                        <Box display="flex" gap={1}>
                          <IconButton
                            size="small"
                            onClick={() => handleViewPO(po.id)}
                            title="View"
                          >
                            <Visibility />
                          </IconButton>
                          
                          {canEdit(po.status) && (
                            <IconButton
                              size="small"
                              onClick={() => handleEditPO(po.id)}
                              title="Edit"
                            >
                              <Edit />
                            </IconButton>
                          )}
                          
                          {canSubmit(po.status) && (
                            <IconButton
                              size="small"
                              onClick={() => handleSubmitPO(po.id)}
                              title="Submit for Approval"
                              color="primary"
                            >
                              <Send />
                            </IconButton>
                          )}
                          
                          {canDelete(po.status) && (
                            <IconButton
                              size="small"
                              onClick={() => handleDeletePO(po)}
                              title="Delete"
                              color="error"
                            >
                              <Delete />
                            </IconButton>
                          )}
                        </Box>
                      </TableCell>
                    </TableRow>
                  ))
                )}
              </TableBody>
            </Table>
          </TableContainer>

          {/* Pagination */}
          <TablePagination
            rowsPerPageOptions={[5, 10, 25, 50]}
            component="div"
            count={totalCount}
            rowsPerPage={rowsPerPage}
            page={page}
            onPageChange={handleChangePage}
            onRowsPerPageChange={handleChangeRowsPerPage}
          />
        </CardContent>
      </Card>

      {/* Delete Confirmation Dialog */}
      <Dialog open={deleteDialogOpen} onClose={() => setDeleteDialogOpen(false)}>
        <DialogTitle>Confirm Delete</DialogTitle>
        <DialogContent>
          <Typography>
            Are you sure you want to delete Purchase Order "{selectedPO?.poNumber}"?
            This action cannot be undone.
          </Typography>
        </DialogContent>
        <DialogActions>
          <Button onClick={() => setDeleteDialogOpen(false)}>Cancel</Button>
          <Button onClick={confirmDelete} color="error" variant="contained">
            Delete
          </Button>
        </DialogActions>
      </Dialog>
    </Box>
  );
};

export default PurchaseOrderList;