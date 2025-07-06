import React, { useState, useEffect } from 'react';
import {
  Box,
  Card,
  CardContent,
  Typography,
  Table,
  TableBody,
  TableCell,
  TableContainer,
  TableHead,
  TableRow,
  Paper,
  Chip,
  Button,
  IconButton,
  Dialog,
  DialogTitle,
  DialogContent,
  DialogActions,
  TextField,
  Alert,
  Grid,
  Tabs,
  Tab,
} from '@mui/material';
import {
  CheckCircle,
  Cancel,
  Visibility,
  Timeline,
  Pending,
  CheckCircleOutline,
  CancelOutlined,
} from '@mui/icons-material';
import { useNavigate } from 'react-router-dom';
import { POStatus, POHeaderDto } from '../../types/api.types';
import { approvalService } from '../../services/approval.service';
import { purchaseOrderService } from '../../services/purchase-order.service';

interface TabPanelProps {
  children?: React.ReactNode;
  index: number;
  value: number;
}

function TabPanel(props: TabPanelProps) {
  const { children, value, index, ...other } = props;

  return (
    <div
      role="tabpanel"
      hidden={value !== index}
      id={`approval-tabpanel-${index}`}
      aria-labelledby={`approval-tab-${index}`}
      {...other}
    >
      {value === index && <Box sx={{ pt: 3 }}>{children}</Box>}
    </div>
  );
}

const ApprovalsPage: React.FC = () => {
  const navigate = useNavigate();
  const [tabValue, setTabValue] = useState(0);
  const [pendingApprovals, setPendingApprovals] = useState<POHeaderDto[]>([]);
  const [allPOs, setAllPOs] = useState<POHeaderDto[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);
  
  // Dialog states
  const [approveDialogOpen, setApproveDialogOpen] = useState(false);
  const [rejectDialogOpen, setRejectDialogOpen] = useState(false);
  const [selectedPO, setSelectedPO] = useState<POHeaderDto | null>(null);
  const [approvalNotes, setApprovalNotes] = useState('');
  const [rejectionReason, setRejectionReason] = useState('');
  
  const currentUserId = 1; // Mock current user ID

  useEffect(() => {
    fetchApprovalData();
  }, []);

  const fetchApprovalData = async () => {
    try {
      setLoading(true);
      setError(null);
      
      // Fetch pending approvals for current user
      const pendingResult = await approvalService.getPendingApprovals(currentUserId);
      if (pendingResult.success && pendingResult.data) {
        setPendingApprovals(pendingResult.data);
      }
      
      // Fetch all POs for approval history
      const allPOsResult = await purchaseOrderService.getPurchaseOrdersPaged({
        pageNumber: 1,
        pageSize: 50,
        sortField: 'CreatedAt',
        sortDirection: 'desc',
      });
      
      if (allPOsResult.success && allPOsResult.data) {
        // Filter only POs that are in approval process or completed
        const approvalPOs = allPOsResult.data.items.filter(po => 
          po.status !== POStatus.Draft && po.status !== POStatus.ReopenedForCorrection
        );
        setAllPOs(approvalPOs);
      }
      
    } catch (error) {
      setError('Failed to fetch approval data');
      console.error('Error fetching approval data:', error);
    } finally {
      setLoading(false);
    }
  };

  const handleTabChange = (event: React.SyntheticEvent, newValue: number) => {
    setTabValue(newValue);
  };

  const handleApprove = (po: POHeaderDto) => {
    setSelectedPO(po);
    setApprovalNotes('');
    setApproveDialogOpen(true);
  };

  const handleReject = (po: POHeaderDto) => {
    setSelectedPO(po);
    setRejectionReason('');
    setRejectDialogOpen(true);
  };

  const confirmApprove = async () => {
    if (!selectedPO) return;
    
    try {
      const result = await approvalService.approvePurchaseOrder(
        selectedPO.id,
        currentUserId,
        approvalNotes
      );
      
      if (result.success) {
        setApproveDialogOpen(false);
        setSelectedPO(null);
        setApprovalNotes('');
        fetchApprovalData(); // Refresh data
      } else {
        setError(result.message || 'Failed to approve purchase order');
      }
    } catch (error) {
      setError('An unexpected error occurred');
      console.error('Error approving purchase order:', error);
    }
  };

  const confirmReject = async () => {
    if (!selectedPO || !rejectionReason.trim()) return;
    
    try {
      const result = await approvalService.rejectPurchaseOrder(
        selectedPO.id,
        currentUserId,
        rejectionReason
      );
      
      if (result.success) {
        setRejectDialogOpen(false);
        setSelectedPO(null);
        setRejectionReason('');
        fetchApprovalData(); // Refresh data
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
      case POStatus.PendingApprovalLevel1:
      case POStatus.PendingApprovalLevel2:
      case POStatus.PendingApprovalLevel3:
        return 'warning';
      case POStatus.Approved:
        return 'success';
      case POStatus.Rejected:
        return 'error';
      default:
        return 'default';
    }
  };

  const getStatusText = (status: POStatus): string => {
    switch (status) {
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
      default:
        return 'Unknown';
    }
  };

  const getApprovalLevel = (status: POStatus): string => {
    switch (status) {
      case POStatus.PendingApprovalLevel1:
        return 'Level 1 (Checker)';
      case POStatus.PendingApprovalLevel2:
        return 'Level 2 (Acknowledge)';
      case POStatus.PendingApprovalLevel3:
        return 'Level 3 (Approver)';
      default:
        return '-';
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

  const getPendingCount = () => pendingApprovals.length;
  const getApprovedCount = () => allPOs.filter(po => po.status === POStatus.Approved).length;
  const getRejectedCount = () => allPOs.filter(po => po.status === POStatus.Rejected).length;

  return (
    <Box>
      <Typography variant="h4" gutterBottom>
        Purchase Order Approvals
      </Typography>

      {error && (
        <Alert severity="error" sx={{ mb: 3 }}>
          {error}
        </Alert>
      )}

      {/* Summary Cards */}
      <Grid container spacing={3} sx={{ mb: 4 }}>
        <Grid item xs={12} md={4}>
          <Card>
            <CardContent>
              <Box display="flex" justifyContent="space-between" alignItems="center">
                <Box>
                  <Typography color="textSecondary" gutterBottom variant="body2">
                    Pending Approvals
                  </Typography>
                  <Typography variant="h4" component="div" sx={{ color: 'warning.main' }}>
                    {getPendingCount()}
                  </Typography>
                </Box>
                <Pending sx={{ fontSize: 40, color: 'warning.main' }} />
              </Box>
            </CardContent>
          </Card>
        </Grid>
        
        <Grid item xs={12} md={4}>
          <Card>
            <CardContent>
              <Box display="flex" justifyContent="space-between" alignItems="center">
                <Box>
                  <Typography color="textSecondary" gutterBottom variant="body2">
                    Approved This Month
                  </Typography>
                  <Typography variant="h4" component="div" sx={{ color: 'success.main' }}>
                    {getApprovedCount()}
                  </Typography>
                </Box>
                <CheckCircleOutline sx={{ fontSize: 40, color: 'success.main' }} />
              </Box>
            </CardContent>
          </Card>
        </Grid>
        
        <Grid item xs={12} md={4}>
          <Card>
            <CardContent>
              <Box display="flex" justifyContent="space-between" alignItems="center">
                <Box>
                  <Typography color="textSecondary" gutterBottom variant="body2">
                    Rejected This Month
                  </Typography>
                  <Typography variant="h4" component="div" sx={{ color: 'error.main' }}>
                    {getRejectedCount()}
                  </Typography>
                </Box>
                <CancelOutlined sx={{ fontSize: 40, color: 'error.main' }} />
              </Box>
            </CardContent>
          </Card>
        </Grid>
      </Grid>

      <Card>
        <CardContent>
          <Box sx={{ borderBottom: 1, borderColor: 'divider' }}>
            <Tabs value={tabValue} onChange={handleTabChange}>
              <Tab label={`Pending Approvals (${getPendingCount()})`} />
              <Tab label="Approval History" />
            </Tabs>
          </Box>

          {/* Pending Approvals Tab */}
          <TabPanel value={tabValue} index={0}>
            <TableContainer component={Paper} variant="outlined">
              <Table>
                <TableHead>
                  <TableRow>
                    <TableCell>PO Number</TableCell>
                    <TableCell>Date</TableCell>
                    <TableCell>Vendor</TableCell>
                    <TableCell>Total Amount</TableCell>
                    <TableCell>Current Level</TableCell>
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
                  ) : pendingApprovals.length === 0 ? (
                    <TableRow>
                      <TableCell colSpan={7} align="center">
                        <Typography color="textSecondary">
                          No pending approvals
                        </Typography>
                      </TableCell>
                    </TableRow>
                  ) : (
                    pendingApprovals.map((po) => (
                      <TableRow key={po.id} hover>
                        <TableCell>
                          <Typography variant="body2" fontWeight="medium">
                            {po.poNumber}
                          </Typography>
                        </TableCell>
                        <TableCell>{formatDate(po.poDate)}</TableCell>
                        <TableCell>{po.vendorName || po.vendorId}</TableCell>
                        <TableCell>{formatCurrency(po.totalDue)}</TableCell>
                        <TableCell>{getApprovalLevel(po.status)}</TableCell>
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
                              onClick={() => navigate(`/purchase-orders/${po.id}`)}
                              title="View Details"
                            >
                              <Visibility />
                            </IconButton>
                            <IconButton
                              size="small"
                              onClick={() => handleApprove(po)}
                              title="Approve"
                              color="success"
                            >
                              <CheckCircle />
                            </IconButton>
                            <IconButton
                              size="small"
                              onClick={() => handleReject(po)}
                              title="Reject"
                              color="error"
                            >
                              <Cancel />
                            </IconButton>
                          </Box>
                        </TableCell>
                      </TableRow>
                    ))
                  )}
                </TableBody>
              </Table>
            </TableContainer>
          </TabPanel>

          {/* Approval History Tab */}
          <TabPanel value={tabValue} index={1}>
            <TableContainer component={Paper} variant="outlined">
              <Table>
                <TableHead>
                  <TableRow>
                    <TableCell>PO Number</TableCell>
                    <TableCell>Date</TableCell>
                    <TableCell>Vendor</TableCell>
                    <TableCell>Total Amount</TableCell>
                    <TableCell>Final Status</TableCell>
                    <TableCell>Actions</TableCell>
                  </TableRow>
                </TableHead>
                <TableBody>
                  {loading ? (
                    <TableRow>
                      <TableCell colSpan={6} align="center">
                        <Typography>Loading...</Typography>
                      </TableCell>
                    </TableRow>
                  ) : allPOs.length === 0 ? (
                    <TableRow>
                      <TableCell colSpan={6} align="center">
                        <Typography color="textSecondary">
                          No approval history found
                        </Typography>
                      </TableCell>
                    </TableRow>
                  ) : (
                    allPOs.map((po) => (
                      <TableRow key={po.id} hover>
                        <TableCell>
                          <Typography variant="body2" fontWeight="medium">
                            {po.poNumber}
                          </Typography>
                        </TableCell>
                        <TableCell>{formatDate(po.poDate)}</TableCell>
                        <TableCell>{po.vendorName || po.vendorId}</TableCell>
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
                              onClick={() => navigate(`/purchase-orders/${po.id}`)}
                              title="View Details"
                            >
                              <Visibility />
                            </IconButton>
                            <IconButton
                              size="small"
                              onClick={() => navigate(`/approvals/${po.id}/history`)}
                              title="View Approval History"
                            >
                              <Timeline />
                            </IconButton>
                          </Box>
                        </TableCell>
                      </TableRow>
                    ))
                  )}
                </TableBody>
              </Table>
            </TableContainer>
          </TabPanel>
        </CardContent>
      </Card>

      {/* Approve Dialog */}
      <Dialog open={approveDialogOpen} onClose={() => setApproveDialogOpen(false)}>
        <DialogTitle>Approve Purchase Order</DialogTitle>
        <DialogContent>
          <Typography gutterBottom>
            Are you sure you want to approve PO "{selectedPO?.poNumber}"?
          </Typography>
          <Typography variant="body2" color="textSecondary" gutterBottom>
            Total Amount: {selectedPO ? formatCurrency(selectedPO.totalDue) : ''}
          </Typography>
          <TextField
            fullWidth
            multiline
            rows={3}
            label="Approval Notes (Optional)"
            value={approvalNotes}
            onChange={(e) => setApprovalNotes(e.target.value)}
            sx={{ mt: 2 }}
            placeholder="Add any notes regarding this approval..."
          />
        </DialogContent>
        <DialogActions>
          <Button onClick={() => setApproveDialogOpen(false)}>Cancel</Button>
          <Button onClick={confirmApprove} variant="contained" color="success">
            Approve
          </Button>
        </DialogActions>
      </Dialog>

      {/* Reject Dialog */}
      <Dialog open={rejectDialogOpen} onClose={() => setRejectDialogOpen(false)}>
        <DialogTitle>Reject Purchase Order</DialogTitle>
        <DialogContent>
          <Typography gutterBottom>
            Please provide a reason for rejecting PO "{selectedPO?.poNumber}":
          </Typography>
          <Typography variant="body2" color="textSecondary" gutterBottom>
            Total Amount: {selectedPO ? formatCurrency(selectedPO.totalDue) : ''}
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
            placeholder="Please specify why this PO is being rejected..."
          />
        </DialogContent>
        <DialogActions>
          <Button onClick={() => setRejectDialogOpen(false)}>Cancel</Button>
          <Button 
            onClick={confirmReject} 
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

export default ApprovalsPage;