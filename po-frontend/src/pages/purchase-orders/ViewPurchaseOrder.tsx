import React, { useState, useEffect } from 'react';
import {
  Box,
  Card,
  CardContent,
  Typography,
  Grid,
  Button,
  Table,
  TableBody,
  TableCell,
  TableContainer,
  TableHead,
  TableRow,
  Paper,
  Chip,
  Divider,
  Alert,
  Dialog,
  DialogTitle,
  DialogContent,
  DialogActions,
  TextField,
} from '@mui/material';
import {
  ArrowBack,
  Edit,
  Send,
  CheckCircle,
  Cancel,
  Timeline,
} from '@mui/icons-material';
import { useNavigate, useParams } from 'react-router-dom';
import { POStatus, POHeaderDto, POType, ApprovalRequestDto } from '../../types/api.types';
import { purchaseOrderService } from '../../services/purchase-order.service';
import { approvalService } from '../../services/approval.service';

const ViewPurchaseOrder: React.FC = () => {
  const navigate = useNavigate();
  const { id } = useParams<{ id: string }>();
  const [po, setPO] = useState<POHeaderDto | null>(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);
  const [submitLoading, setSubmitLoading] = useState(false);
  
  // Approval dialogs
  const [approveDialogOpen, setApproveDialogOpen] = useState(false);
  const [rejectDialogOpen, setRejectDialogOpen] = useState(false);
  const [approvalNotes, setApprovalNotes] = useState('');
  const [rejectionReason, setRejectionReason] = useState('');

  const currentUserId = 1; // Mock current user ID

  useEffect(() => {
    if (id) {
      fetchPurchaseOrder(parseInt(id));
    }
  }, [id]);

  const fetchPurchaseOrder = async (poId: number) => {
    try {
      setLoading(true);
      const result = await purchaseOrderService.getPurchaseOrderById(poId);
      
      if (result.success && result.data) {
        setPO(result.data);
      } else {
        setError(result.message || 'Failed to fetch purchase order');
      }
    } catch (error) {
      setError('An unexpected error occurred');
      console.error('Error fetching purchase order:', error);
    } finally {
      setLoading(false);
    }
  };

  const handleSubmitForApproval = async () => {
    if (!po) return;
    
    try {
      setSubmitLoading(true);
      const result = await purchaseOrderService.submitPurchaseOrder(po.id);
      
      if (result.success) {
        // Refresh the PO data
        fetchPurchaseOrder(po.id);
      } else {
        setError(result.message || 'Failed to submit purchase order');
      }
    } catch (error) {
      setError('An unexpected error occurred');
      console.error('Error submitting purchase order:', error);
    } finally {
      setSubmitLoading(false);
    }
  };

  const handleApprove = async () => {
    if (!po) return;
    
    try {
      const result = await approvalService.approvePurchaseOrder(
        po.id,
        currentUserId,
        approvalNotes
      );
      
      if (result.success) {
        setApproveDialogOpen(false);
        setApprovalNotes('');
        // Refresh the PO data
        fetchPurchaseOrder(po.id);
      } else {
        setError(result.message || 'Failed to approve purchase order');
      }
    } catch (error) {
      setError('An unexpected error occurred');
      console.error('Error approving purchase order:', error);
    }
  };

  const handleReject = async () => {
    if (!po || !rejectionReason.trim()) return;
    
    try {
      const result = await approvalService.rejectPurchaseOrder(
        po.id,
        currentUserId,
        rejectionReason
      );
      
      if (result.success) {
        setRejectDialogOpen(false);
        setRejectionReason('');
        // Refresh the PO data
        fetchPurchaseOrder(po.id);
      } else {
        setError(result.message || 'Failed to reject purchase order');
      }
    } catch (error) {
      setError('An unexpected error occurred');
      console.error('Error rejecting purchase order:', error);
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
        return 'Pending Level 1 Approval';
      case POStatus.PendingApprovalLevel2:
        return 'Pending Level 2 Approval';
      case POStatus.PendingApprovalLevel3:
        return 'Pending Level 3 Approval';
      case POStatus.Approved:
        return 'Approved';
      case POStatus.Rejected:
        return 'Rejected';
      case POStatus.ReopenedForCorrection:
        return 'Reopened for Correction';
      default:
        return 'Unknown';
    }
  };

  const getPOTypeText = (type: POType): string => {
    return type === POType.Local ? 'Local' : 'Import';
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

  const canApprove = (status: POStatus): boolean => {
    return status === POStatus.PendingApprovalLevel1 || 
           status === POStatus.PendingApprovalLevel2 || 
           status === POStatus.PendingApprovalLevel3;
  };

  if (loading) {
    return (
      <Box display="flex" justifyContent="center" alignItems="center" minHeight="400px">
        <Typography>Loading...</Typography>
      </Box>
    );
  }

  if (error || !po) {
    return (
      <Box>
        <Alert severity="error" sx={{ mb: 3 }}>
          {error || 'Purchase Order not found'}
        </Alert>
        <Button
          startIcon={<ArrowBack />}
          onClick={() => navigate('/purchase-orders')}
        >
          Back to List
        </Button>
      </Box>
    );
  }

  return (
    <Box>
      <Box display="flex" justifyContent="space-between" alignItems="center" mb={3}>
        <Typography variant="h4">
          Purchase Order: {po.poNumber}
        </Typography>
        <Box display="flex" gap={2}>
          {canEdit(po.status) && (
            <Button
              startIcon={<Edit />}
              variant="outlined"
              onClick={() => navigate(`/purchase-orders/${po.id}/edit`)}
            >
              Edit
            </Button>
          )}
          
          {canSubmit(po.status) && (
            <Button
              startIcon={<Send />}
              variant="contained"
              color="primary"
              onClick={handleSubmitForApproval}
              disabled={submitLoading}
            >
              {submitLoading ? 'Submitting...' : 'Submit for Approval'}
            </Button>
          )}
          
          {canApprove(po.status) && (
            <>
              <Button
                startIcon={<CheckCircle />}
                variant="contained"
                color="success"
                onClick={() => setApproveDialogOpen(true)}
              >
                Approve
              </Button>
              <Button
                startIcon={<Cancel />}
                variant="contained"
                color="error"
                onClick={() => setRejectDialogOpen(true)}
              >
                Reject
              </Button>
            </>
          )}
          
          <Button
            startIcon={<ArrowBack />}
            onClick={() => navigate('/purchase-orders')}
          >
            Back to List
          </Button>
        </Box>
      </Box>

      {error && (
        <Alert severity="error" sx={{ mb: 3 }}>
          {error}
        </Alert>
      )}

      <Grid container spacing={3}>
        {/* Header Information */}
        <Grid item xs={12} lg={8}>
          <Card>
            <CardContent>
              <Typography variant="h6" gutterBottom>
                Purchase Order Information
              </Typography>
              
              <Grid container spacing={3}>
                <Grid item xs={12} md={6}>
                  <Typography variant="body2" color="textSecondary">
                    PO Number
                  </Typography>
                  <Typography variant="body1" fontWeight="medium">
                    {po.poNumber}
                  </Typography>
                </Grid>

                <Grid item xs={12} md={6}>
                  <Typography variant="body2" color="textSecondary">
                    Status
                  </Typography>
                  <Chip
                    label={getStatusText(po.status)}
                    color={getStatusColor(po.status)}
                    size="small"
                  />
                </Grid>

                <Grid item xs={12} md={6}>
                  <Typography variant="body2" color="textSecondary">
                    PO Date
                  </Typography>
                  <Typography variant="body1">
                    {formatDate(po.poDate)}
                  </Typography>
                </Grid>

                <Grid item xs={12} md={6}>
                  <Typography variant="body2" color="textSecondary">
                    Posting Date
                  </Typography>
                  <Typography variant="body1">
                    {formatDate(po.postingDate)}
                  </Typography>
                </Grid>

                <Grid item xs={12} md={6}>
                  <Typography variant="body2" color="textSecondary">
                    PO Type
                  </Typography>
                  <Typography variant="body1">
                    {getPOTypeText(po.poType)}
                  </Typography>
                </Grid>

                <Grid item xs={12} md={6}>
                  <Typography variant="body2" color="textSecondary">
                    Vendor
                  </Typography>
                  <Typography variant="body1">
                    {po.vendorName || po.vendorId}
                  </Typography>
                </Grid>

                <Grid item xs={12} md={6}>
                  <Typography variant="body2" color="textSecondary">
                    Department
                  </Typography>
                  <Typography variant="body1">
                    {po.deptName}
                  </Typography>
                </Grid>

                <Grid item xs={12} md={6}>
                  <Typography variant="body2" color="textSecondary">
                    Created By
                  </Typography>
                  <Typography variant="body1">
                    {po.createdByName}
                  </Typography>
                </Grid>

                <Grid item xs={12} md={6}>
                  <Typography variant="body2" color="textSecondary">
                    Delivery Date
                  </Typography>
                  <Typography variant="body1">
                    {po.deliveryDate ? formatDate(po.deliveryDate) : '-'}
                  </Typography>
                </Grid>

                <Grid item xs={12} md={6}>
                  <Typography variant="body2" color="textSecondary">
                    Currency
                  </Typography>
                  <Typography variant="body1">
                    {po.currency} (Rate: {po.exchangeRate})
                  </Typography>
                </Grid>

                {po.deliveryAddress && (
                  <Grid item xs={12}>
                    <Typography variant="body2" color="textSecondary">
                      Delivery Address
                    </Typography>
                    <Typography variant="body1">
                      {po.deliveryAddress}
                    </Typography>
                  </Grid>
                )}

                {po.notes && (
                  <Grid item xs={12}>
                    <Typography variant="body2" color="textSecondary">
                      Notes
                    </Typography>
                    <Typography variant="body1">
                      {po.notes}
                    </Typography>
                  </Grid>
                )}
              </Grid>
            </CardContent>
          </Card>
        </Grid>

        {/* Summary */}
        <Grid item xs={12} lg={4}>
          <Card>
            <CardContent>
              <Typography variant="h6" gutterBottom>
                Order Summary
              </Typography>
              
              <Box display="flex" justifyContent="space-between" mb={1}>
                <Typography variant="body2">Subtotal:</Typography>
                <Typography variant="body2" fontWeight="medium">
                  {formatCurrency(po.subTotal)}
                </Typography>
              </Box>
              
              <Box display="flex" justifyContent="space-between" mb={1}>
                <Typography variant="body2">Tax Amount:</Typography>
                <Typography variant="body2" fontWeight="medium">
                  {formatCurrency(po.taxAmount)}
                </Typography>
              </Box>
              
              <Divider sx={{ my: 2 }} />
              
              <Box display="flex" justifyContent="space-between">
                <Typography variant="h6">Total Due:</Typography>
                <Typography variant="h6" color="primary">
                  {formatCurrency(po.totalDue)}
                </Typography>
              </Box>
            </CardContent>
          </Card>
        </Grid>

        {/* Line Items */}
        <Grid item xs={12}>
          <Card>
            <CardContent>
              <Typography variant="h6" gutterBottom>
                Line Items
              </Typography>
              
              <TableContainer component={Paper} variant="outlined">
                <Table>
                  <TableHead>
                    <TableRow>
                      <TableCell>Line #</TableCell>
                      <TableCell>Item Code</TableCell>
                      <TableCell>Description</TableCell>
                      <TableCell>Quantity</TableCell>
                      <TableCell>UOM</TableCell>
                      <TableCell>Unit Price</TableCell>
                      <TableCell>Line Total</TableCell>
                      <TableCell>Tax Amount</TableCell>
                      <TableCell>Total Inc. Tax</TableCell>
                    </TableRow>
                  </TableHead>
                  <TableBody>
                    {po.poDetails.map((detail) => (
                      <TableRow key={detail.id}>
                        <TableCell>{detail.lineNumber}</TableCell>
                        <TableCell>{detail.itemCode}</TableCell>
                        <TableCell>{detail.itemDescription}</TableCell>
                        <TableCell>{detail.quantity}</TableCell>
                        <TableCell>{detail.uomCode}</TableCell>
                        <TableCell>{formatCurrency(detail.unitPrice)}</TableCell>
                        <TableCell>{formatCurrency(detail.lineTotal)}</TableCell>
                        <TableCell>{formatCurrency(detail.taxAmount)}</TableCell>
                        <TableCell>{formatCurrency(detail.lineTotalIncludingTax)}</TableCell>
                      </TableRow>
                    ))}
                  </TableBody>
                </Table>
              </TableContainer>
            </CardContent>
          </Card>
        </Grid>
      </Grid>

      {/* Approve Dialog */}
      <Dialog open={approveDialogOpen} onClose={() => setApproveDialogOpen(false)}>
        <DialogTitle>Approve Purchase Order</DialogTitle>
        <DialogContent>
          <Typography gutterBottom>
            Are you sure you want to approve PO "{po.poNumber}"?
          </Typography>
          <TextField
            fullWidth
            multiline
            rows={3}
            label="Approval Notes (Optional)"
            value={approvalNotes}
            onChange={(e) => setApprovalNotes(e.target.value)}
            sx={{ mt: 2 }}
          />
        </DialogContent>
        <DialogActions>
          <Button onClick={() => setApproveDialogOpen(false)}>Cancel</Button>
          <Button onClick={handleApprove} variant="contained" color="success">
            Approve
          </Button>
        </DialogActions>
      </Dialog>

      {/* Reject Dialog */}
      <Dialog open={rejectDialogOpen} onClose={() => setRejectDialogOpen(false)}>
        <DialogTitle>Reject Purchase Order</DialogTitle>
        <DialogContent>
          <Typography gutterBottom>
            Please provide a reason for rejecting PO "{po.poNumber}":
          </Typography>
          <TextField
            fullWidth
            multiline
            rows={3}
            label="Rejection Reason *"
            value={rejectionReason}
            onChange={(e) => setRejectionReason(e.target.value)}
            required
            sx={{ mt: 2 }}
          />
        </DialogContent>
        <DialogActions>
          <Button onClick={() => setRejectDialogOpen(false)}>Cancel</Button>
          <Button 
            onClick={handleReject} 
            variant="contained" 
            color="error"
            disabled={!rejectionReason.trim()}
          >
            Reject
          </Button>
        </DialogActions>
      </Dialog>
    </Box>
  );
};

export default ViewPurchaseOrder;