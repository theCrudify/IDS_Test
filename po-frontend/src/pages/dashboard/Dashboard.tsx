import React, { useState, useEffect } from 'react';
import {
  Box,
  Grid,
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
} from '@mui/material';
import {
  TrendingUp,
  Assignment,
  Pending,
  CheckCircle,
  Cancel,
  Visibility,
} from '@mui/icons-material';
import { useNavigate } from 'react-router-dom';
import { POStatus, POHeaderDto } from '../../types/api.types';
import { purchaseOrderService } from '../../services/purchase-order.service';
import { approvalService } from '../../services/approval.service';

interface DashboardStats {
  totalPOs: number;
  pendingPOs: number;
  approvedPOs: number;
  rejectedPOs: number;
  myPendingApprovals: number;
}

const Dashboard: React.FC = () => {
  const navigate = useNavigate();
  const [stats, setStats] = useState<DashboardStats>({
    totalPOs: 0,
    pendingPOs: 0,
    approvedPOs: 0,
    rejectedPOs: 0,
    myPendingApprovals: 0,
  });
  const [recentPOs, setRecentPOs] = useState<POHeaderDto[]>([]);
  const [pendingApprovals, setPendingApprovals] = useState<POHeaderDto[]>([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    fetchDashboardData();
  }, []);

  const fetchDashboardData = async () => {
    try {
      setLoading(true);
      
      // Get recent purchase orders
      const recentPOsResult = await purchaseOrderService.getPurchaseOrdersPaged({
        pageNumber: 1,
        pageSize: 5,
      });
      
      if (recentPOsResult.success && recentPOsResult.data) {
        setRecentPOs(recentPOsResult.data.items);
        
        // Calculate stats from the data
        const totalCount = recentPOsResult.data.totalCount;
        setStats(prev => ({ ...prev, totalPOs: totalCount }));
      }
      
      // Get pending approvals for current user (using userId = 1 as default)
      const pendingApprovalsResult = await approvalService.getPendingApprovals(1);
      if (pendingApprovalsResult.success && pendingApprovalsResult.data) {
        setPendingApprovals(pendingApprovalsResult.data);
        setStats(prev => ({ ...prev, myPendingApprovals: pendingApprovalsResult.data!.length }));
      }
      
      // Mock additional stats (in real app, these would come from API)
      setStats(prev => ({
        ...prev,
        pendingPOs: 8,
        approvedPOs: 15,
        rejectedPOs: 2,
      }));
      
    } catch (error) {
      console.error('Error fetching dashboard data:', error);
    } finally {
      setLoading(false);
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
        return 'Pending L1';
      case POStatus.PendingApprovalLevel2:
        return 'Pending L2';
      case POStatus.PendingApprovalLevel3:
        return 'Pending L3';
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

  const handleViewPO = (id: number) => {
    navigate(`/purchase-orders/${id}`);
  };

  const statsCards = [
    {
      title: 'Total Purchase Orders',
      value: stats.totalPOs,
      icon: <Assignment sx={{ fontSize: 40, color: 'primary.main' }} />,
      color: 'primary.main',
    },
    {
      title: 'Pending Approval',
      value: stats.pendingPOs,
      icon: <Pending sx={{ fontSize: 40, color: 'warning.main' }} />,
      color: 'warning.main',
    },
    {
      title: 'Approved',
      value: stats.approvedPOs,
      icon: <CheckCircle sx={{ fontSize: 40, color: 'success.main' }} />,
      color: 'success.main',
    },
    {
      title: 'My Pending Approvals',
      value: stats.myPendingApprovals,
      icon: <TrendingUp sx={{ fontSize: 40, color: 'info.main' }} />,
      color: 'info.main',
    },
  ];

  return (
    <Box>
      <Typography variant="h4" gutterBottom>
        Dashboard
      </Typography>
      
      {/* Stats Cards */}
      <Grid container spacing={3} sx={{ mb: 4 }}>
        {statsCards.map((card, index) => (
          <Grid item xs={12} sm={6} md={3} key={index}>
            <Card>
              <CardContent>
                <Box display="flex" justifyContent="space-between" alignItems="center">
                  <Box>
                    <Typography color="textSecondary" gutterBottom variant="body2">
                      {card.title}
                    </Typography>
                    <Typography variant="h4" component="div" sx={{ color: card.color }}>
                      {card.value}
                    </Typography>
                  </Box>
                  {card.icon}
                </Box>
              </CardContent>
            </Card>
          </Grid>
        ))}
      </Grid>

      <Grid container spacing={3}>
        {/* Recent Purchase Orders */}
        <Grid item xs={12} lg={8}>
          <Card>
            <CardContent>
              <Box display="flex" justifyContent="space-between" alignItems="center" mb={2}>
                <Typography variant="h6">Recent Purchase Orders</Typography>
                <Button
                  variant="outlined"
                  onClick={() => navigate('/purchase-orders')}
                >
                  View All
                </Button>
              </Box>
              
              <TableContainer component={Paper} variant="outlined">
                <Table>
                  <TableHead>
                    <TableRow>
                      <TableCell>PO Number</TableCell>
                      <TableCell>Vendor</TableCell>
                      <TableCell>Total Amount</TableCell>
                      <TableCell>Status</TableCell>
                      <TableCell>Actions</TableCell>
                    </TableRow>
                  </TableHead>
                  <TableBody>
                    {recentPOs.map((po) => (
                      <TableRow key={po.id}>
                        <TableCell>{po.poNumber}</TableCell>
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
                          <IconButton
                            size="small"
                            onClick={() => handleViewPO(po.id)}
                          >
                            <Visibility />
                          </IconButton>
                        </TableCell>
                      </TableRow>
                    ))}
                    {recentPOs.length === 0 && (
                      <TableRow>
                        <TableCell colSpan={5} align="center">
                          <Typography color="textSecondary">
                            No purchase orders found
                          </Typography>
                        </TableCell>
                      </TableRow>
                    )}
                  </TableBody>
                </Table>
              </TableContainer>
            </CardContent>
          </Card>
        </Grid>

        {/* Pending Approvals */}
        <Grid item xs={12} lg={4}>
          <Card>
            <CardContent>
              <Box display="flex" justifyContent="space-between" alignItems="center" mb={2}>
                <Typography variant="h6">Pending Approvals</Typography>
                <Button
                  variant="outlined"
                  onClick={() => navigate('/approvals')}
                  size="small"
                >
                  View All
                </Button>
              </Box>
              
              {pendingApprovals.length > 0 ? (
                <Box>
                  {pendingApprovals.slice(0, 5).map((po) => (
                    <Box
                      key={po.id}
                      sx={{
                        p: 2,
                        mb: 1,
                        border: '1px solid',
                        borderColor: 'divider',
                        borderRadius: 1,
                        cursor: 'pointer',
                        '&:hover': { bgcolor: 'action.hover' },
                      }}
                      onClick={() => navigate(`/approvals/${po.id}`)}
                    >
                      <Typography variant="subtitle2" gutterBottom>
                        {po.poNumber}
                      </Typography>
                      <Typography variant="body2" color="textSecondary">
                        {po.vendorName || po.vendorId}
                      </Typography>
                      <Typography variant="body2" fontWeight="bold">
                        {formatCurrency(po.totalDue)}
                      </Typography>
                    </Box>
                  ))}
                </Box>
              ) : (
                <Typography color="textSecondary" align="center">
                  No pending approvals
                </Typography>
              )}
            </CardContent>
          </Card>
        </Grid>
      </Grid>
    </Box>
  );
};

export default Dashboard;