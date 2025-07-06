import React, { useState, useEffect } from 'react';
import {
  Box,
  Card,
  CardContent,
  Typography,
  TextField,
  Grid,
  Button,
  Autocomplete,
  Table,
  TableBody,
  TableCell,
  TableContainer,
  TableHead,
  TableRow,
  Paper,
  IconButton,
  FormControl,
  InputLabel,
  Select,
  MenuItem,
  Divider,
  Alert,
} from '@mui/material';
import {
  Add,
  Delete,
  Save,
  Send,
  ArrowBack,
} from '@mui/icons-material';
import { useNavigate } from 'react-router-dom';
import { useForm, Controller, useFieldArray } from 'react-hook-form';
import { yupResolver } from '@hookform/resolvers/yup';
import * as yup from 'yup';
import dayjs from 'dayjs';

import {
  CreatePOHeaderDto,
  CreatePODetailDto,
  POType,
  VendorLookupDto,
  DepartmentLookupDto,
  ItemLookupDto,
  UOMLookupDto,
  TaxLookupDto,
} from '../../types/api.types';

import { purchaseOrderService } from '../../services/purchase-order.service';
import {
  vendorService,
  departmentService,
  itemService,
  uomService,
  taxService,
} from '../../services/master-data.service';

// Validation schema
const schema = yup.object().shape({
  poDate: yup.string().required('PO Date is required'),
  postingDate: yup.string().required('Posting Date is required'),
  poType: yup.number().required('PO Type is required'),
  vendorId: yup.string().required('Vendor is required'),
  deptId: yup.number().required('Department is required'),
  deliveryAddress: yup.string().required('Delivery Address is required'),
  deliveryDate: yup.string().required('Delivery Date is required'),
  currency: yup.string().required('Currency is required'),
  exchangeRate: yup.number().positive('Exchange rate must be positive').required('Exchange Rate is required'),
  poDetails: yup.array().of(
    yup.object().shape({
      itemId: yup.number().required('Item is required'),
      quantity: yup.number().positive('Quantity must be positive').required('Quantity is required'),
      unitPrice: yup.number().min(0, 'Unit price cannot be negative').required('Unit Price is required'),
      uomId: yup.number().required('UOM is required'),
      taxId: yup.number().required('Tax is required'),
    })
  ).min(1, 'At least one line item is required'),
});

interface PurchaseOrderFormProps {
  onSuccess?: () => void;
  initialData?: any;
  mode?: 'create' | 'edit';
}

const PurchaseOrderForm: React.FC<PurchaseOrderFormProps> = ({
  onSuccess,
  initialData,
  mode = 'create'
}) => {
  const navigate = useNavigate();
  const [loading, setLoading] = useState(false);
  const [submitError, setSubmitError] = useState<string | null>(null);
  
  // Lookup data
  const [vendors, setVendors] = useState<VendorLookupDto[]>([]);
  const [departments, setDepartments] = useState<DepartmentLookupDto[]>([]);
  const [items, setItems] = useState<ItemLookupDto[]>([]);
  const [uoms, setUoms] = useState<UOMLookupDto[]>([]);
  const [taxes, setTaxes] = useState<TaxLookupDto[]>([]);

  // Form setup
  const {
    control,
    handleSubmit,
    watch,
    setValue,
    formState: { errors },
  } = useForm<CreatePOHeaderDto>({
    resolver: yupResolver(schema),
    defaultValues: {
      poDate: dayjs().format('YYYY-MM-DD'),
      postingDate: dayjs().format('YYYY-MM-DD'),
      poType: POType.Local,
      vendorId: '',
      deptId: 0,
      notes: '',
      deliveryAddress: '',
      deliveryDate: dayjs().add(7, 'days').format('YYYY-MM-DD'),
      currency: 'IDR',
      exchangeRate: 1.0,
      poDetails: [{
        lineNumber: 1,
        itemId: 0,
        itemCode: '',
        itemDescription: '',
        quantity: 1,
        uomId: 0,
        unitPrice: 0,
        taxId: 0,
      }],
    },
  });

  const { fields, append, remove } = useFieldArray({
    control,
    name: 'poDetails',
  });

  const watchedDetails = watch('poDetails');

  useEffect(() => {
    fetchLookupData();
  }, []);

  const fetchLookupData = async () => {
    try {
      const [vendorsResult, deptResult, itemsResult, uomsResult, taxesResult] = await Promise.all([
        vendorService.getLookup(),
        departmentService.getLookup(),
        itemService.getLookup(),
        uomService.getLookup(),
        taxService.getLookup(),
      ]);

      if (vendorsResult.success && vendorsResult.data) {
        setVendors(vendorsResult.data);
      }
      if (deptResult.success && deptResult.data) {
        setDepartments(deptResult.data);
      }
      if (itemsResult.success && itemsResult.data) {
        setItems(itemsResult.data);
      }
      if (uomsResult.success && uomsResult.data) {
        setUoms(uomsResult.data);
      }
      if (taxesResult.success && taxesResult.data) {
        setTaxes(taxesResult.data);
      }
    } catch (error) {
      console.error('Error fetching lookup data:', error);
    }
  };

  const handleItemSelect = (index: number, item: ItemLookupDto | null) => {
    if (item) {
      setValue(`poDetails.${index}.itemId`, item.id);
      setValue(`poDetails.${index}.itemCode`, item.itemCode);
      setValue(`poDetails.${index}.itemDescription`, item.itemName);
      
      if (item.standardPrice) {
        setValue(`poDetails.${index}.unitPrice`, item.standardPrice);
      }
      if (item.defaultUOMId) {
        setValue(`poDetails.${index}.uomId`, item.defaultUOMId);
      }
      if (item.defaultTaxId) {
        setValue(`poDetails.${index}.taxId`, item.defaultTaxId);
      }
    }
  };

  const calculateLineTotal = (index: number): number => {
    const detail = watchedDetails[index];
    if (!detail) return 0;
    
    const lineTotal = detail.quantity * detail.unitPrice;
    const tax = taxes.find(t => t.id === detail.taxId);
    const taxAmount = tax ? (lineTotal * tax.taxRate / 100) : 0;
    
    return lineTotal + taxAmount;
  };

  const calculateTotals = () => {
    let subTotal = 0;
    let taxAmount = 0;

    watchedDetails.forEach((detail, index) => {
      const lineTotal = detail.quantity * detail.unitPrice;
      subTotal += lineTotal;
      
      const tax = taxes.find(t => t.id === detail.taxId);
      if (tax) {
        taxAmount += (lineTotal * tax.taxRate / 100);
      }
    });

    return {
      subTotal,
      taxAmount,
      totalDue: subTotal + taxAmount,
    };
  };

  const totals = calculateTotals();

  const addLineItem = () => {
    append({
      lineNumber: fields.length + 1,
      itemId: 0,
      itemCode: '',
      itemDescription: '',
      quantity: 1,
      uomId: 0,
      unitPrice: 0,
      taxId: 0,
    });
  };

  const removeLineItem = (index: number) => {
    if (fields.length > 1) {
      remove(index);
      // Update line numbers
      fields.forEach((_, i) => {
        if (i > index) {
          setValue(`poDetails.${i - 1}.lineNumber`, i);
        }
      });
    }
  };

  const onSubmit = async (data: CreatePOHeaderDto) => {
    try {
      setLoading(true);
      setSubmitError(null);

      const result = await purchaseOrderService.createPurchaseOrder(data);
      
      if (result.success) {
        if (onSuccess) {
          onSuccess();
        } else {
          navigate('/purchase-orders');
        }
      } else {
        setSubmitError(result.message || 'Failed to create purchase order');
      }
    } catch (error) {
      console.error('Error creating purchase order:', error);
      setSubmitError('An unexpected error occurred');
    } finally {
      setLoading(false);
    }
  };

  const formatCurrency = (amount: number): string => {
    return new Intl.NumberFormat('id-ID', {
      style: 'currency',
      currency: 'IDR',
    }).format(amount);
  };

  return (
    <Box>
      <Box display="flex" justifyContent="space-between" alignItems="center" mb={3}>
        <Typography variant="h4">
          {mode === 'create' ? 'Create Purchase Order' : 'Edit Purchase Order'}
        </Typography>
        <Button
          startIcon={<ArrowBack />}
          onClick={() => navigate('/purchase-orders')}
        >
          Back to List
        </Button>
      </Box>

      {submitError && (
        <Alert severity="error" sx={{ mb: 3 }}>
          {submitError}
        </Alert>
      )}

      <form onSubmit={handleSubmit(onSubmit)}>
        <Grid container spacing={3}>
          {/* Header Information */}
          <Grid item xs={12}>
            <Card>
              <CardContent>
                <Typography variant="h6" gutterBottom>
                  Purchase Order Information
                </Typography>
                
                <Grid container spacing={3}>
                  <Grid item xs={12} md={6}>
                    <Controller
                      name="poDate"
                      control={control}
                      render={({ field }) => (
                        <TextField
                          {...field}
                          fullWidth
                          label="PO Date"
                          type="date"
                          InputLabelProps={{ shrink: true }}
                          error={!!errors.poDate}
                          helperText={errors.poDate?.message}
                        />
                      )}
                    />
                  </Grid>

                  <Grid item xs={12} md={6}>
                    <Controller
                      name="postingDate"
                      control={control}
                      render={({ field }) => (
                        <TextField
                          {...field}
                          fullWidth
                          label="Posting Date"
                          type="date"
                          InputLabelProps={{ shrink: true }}
                          error={!!errors.postingDate}
                          helperText={errors.postingDate?.message}
                        />
                      )}
                    />
                  </Grid>

                  <Grid item xs={12} md={6}>
                    <Controller
                      name="poType"
                      control={control}
                      render={({ field }) => (
                        <FormControl fullWidth error={!!errors.poType}>
                          <InputLabel>PO Type</InputLabel>
                          <Select {...field} label="PO Type">
                            <MenuItem value={POType.Local}>Local</MenuItem>
                            <MenuItem value={POType.Import}>Import</MenuItem>
                          </Select>
                        </FormControl>
                      )}
                    />
                  </Grid>

                  <Grid item xs={12} md={6}>
                    <Controller
                      name="vendorId"
                      control={control}
                      render={({ field }) => (
                        <Autocomplete
                          options={vendors}
                          getOptionLabel={(option) => `${option.vendorId} - ${option.vendorName}`}
                          value={vendors.find(v => v.vendorId === field.value) || null}
                          onChange={(_, value) => field.onChange(value?.vendorId || '')}
                          renderInput={(params) => (
                            <TextField
                              {...params}
                              label="Vendor"
                              error={!!errors.vendorId}
                              helperText={errors.vendorId?.message}
                            />
                          )}
                        />
                      )}
                    />
                  </Grid>

                  <Grid item xs={12} md={6}>
                    <Controller
                      name="deptId"
                      control={control}
                      render={({ field }) => (
                        <Autocomplete
                          options={departments}
                          getOptionLabel={(option) => `${option.deptCode} - ${option.deptName}`}
                          value={departments.find(d => d.id === field.value) || null}
                          onChange={(_, value) => field.onChange(value?.id || 0)}
                          renderInput={(params) => (
                            <TextField
                              {...params}
                              label="Department"
                              error={!!errors.deptId}
                              helperText={errors.deptId?.message}
                            />
                          )}
                        />
                      )}
                    />
                  </Grid>

                  <Grid item xs={12} md={6}>
                    <Controller
                      name="deliveryDate"
                      control={control}
                      render={({ field }) => (
                        <TextField
                          {...field}
                          fullWidth
                          label="Delivery Date"
                          type="date"
                          InputLabelProps={{ shrink: true }}
                          error={!!errors.deliveryDate}
                          helperText={errors.deliveryDate?.message}
                        />
                      )}
                    />
                  </Grid>

                  <Grid item xs={12}>
                    <Controller
                      name="deliveryAddress"
                      control={control}
                      render={({ field }) => (
                        <TextField
                          {...field}
                          fullWidth
                          label="Delivery Address"
                          multiline
                          rows={2}
                          error={!!errors.deliveryAddress}
                          helperText={errors.deliveryAddress?.message}
                        />
                      )}
                    />
                  </Grid>

                  <Grid item xs={12} md={6}>
                    <Controller
                      name="currency"
                      control={control}
                      render={({ field }) => (
                        <TextField
                          {...field}
                          fullWidth
                          label="Currency"
                          error={!!errors.currency}
                          helperText={errors.currency?.message}
                        />
                      )}
                    />
                  </Grid>

                  <Grid item xs={12} md={6}>
                    <Controller
                      name="exchangeRate"
                      control={control}
                      render={({ field }) => (
                        <TextField
                          {...field}
                          fullWidth
                          label="Exchange Rate"
                          type="number"
                          inputProps={{ step: 0.01, min: 0 }}
                          error={!!errors.exchangeRate}
                          helperText={errors.exchangeRate?.message}
                        />
                      )}
                    />
                  </Grid>

                  <Grid item xs={12}>
                    <Controller
                      name="notes"
                      control={control}
                      render={({ field }) => (
                        <TextField
                          {...field}
                          fullWidth
                          label="Notes"
                          multiline
                          rows={3}
                        />
                      )}
                    />
                  </Grid>
                </Grid>
              </CardContent>
            </Card>
          </Grid>

          {/* Line Items */}
          <Grid item xs={12}>
            <Card>
              <CardContent>
                <Box display="flex" justifyContent="space-between" alignItems="center" mb={2}>
                  <Typography variant="h6">Line Items</Typography>
                  <Button
                    startIcon={<Add />}
                    onClick={addLineItem}
                    variant="outlined"
                  >
                    Add Item
                  </Button>
                </Box>

                <TableContainer component={Paper} variant="outlined">
                  <Table>
                    <TableHead>
                      <TableRow>
                        <TableCell>Line #</TableCell>
                        <TableCell>Item</TableCell>
                        <TableCell>Quantity</TableCell>
                        <TableCell>UOM</TableCell>
                        <TableCell>Unit Price</TableCell>
                        <TableCell>Tax</TableCell>
                        <TableCell>Total</TableCell>
                        <TableCell>Actions</TableCell>
                      </TableRow>
                    </TableHead>
                    <TableBody>
                      {fields.map((field, index) => (
                        <TableRow key={field.id}>
                          <TableCell>{index + 1}</TableCell>
                          <TableCell>
                            <Controller
                              name={`poDetails.${index}.itemId`}
                              control={control}
                              render={({ field: itemField }) => (
                                <Autocomplete
                                  options={items}
                                  getOptionLabel={(option) => `${option.itemCode} - ${option.itemName}`}
                                  value={items.find(i => i.id === itemField.value) || null}
                                  onChange={(_, value) => handleItemSelect(index, value)}
                                  sx={{ minWidth: 200 }}
                                  renderInput={(params) => (
                                    <TextField
                                      {...params}
                                      size="small"
                                      error={!!errors.poDetails?.[index]?.itemId}
                                    />
                                  )}
                                />
                              )}
                            />
                          </TableCell>
                          <TableCell>
                            <Controller
                              name={`poDetails.${index}.quantity`}
                              control={control}
                              render={({ field: qtyField }) => (
                                <TextField
                                  {...qtyField}
                                  size="small"
                                  type="number"
                                  inputProps={{ min: 1 }}
                                  sx={{ width: 80 }}
                                  error={!!errors.poDetails?.[index]?.quantity}
                                />
                              )}
                            />
                          </TableCell>
                          <TableCell>
                            <Controller
                              name={`poDetails.${index}.uomId`}
                              control={control}
                              render={({ field: uomField }) => (
                                <Autocomplete
                                  options={uoms}
                                  getOptionLabel={(option) => option.uomCode}
                                  value={uoms.find(u => u.id === uomField.value) || null}
                                  onChange={(_, value) => uomField.onChange(value?.id || 0)}
                                  sx={{ minWidth: 100 }}
                                  renderInput={(params) => (
                                    <TextField
                                      {...params}
                                      size="small"
                                      error={!!errors.poDetails?.[index]?.uomId}
                                    />
                                  )}
                                />
                              )}
                            />
                          </TableCell>
                          <TableCell>
                            <Controller
                              name={`poDetails.${index}.unitPrice`}
                              control={control}
                              render={({ field: priceField }) => (
                                <TextField
                                  {...priceField}
                                  size="small"
                                  type="number"
                                  inputProps={{ step: 0.01, min: 0 }}
                                  sx={{ width: 120 }}
                                  error={!!errors.poDetails?.[index]?.unitPrice}
                                />
                              )}
                            />
                          </TableCell>
                          <TableCell>
                            <Controller
                              name={`poDetails.${index}.taxId`}
                              control={control}
                              render={({ field: taxField }) => (
                                <Autocomplete
                                  options={taxes}
                                  getOptionLabel={(option) => `${option.taxCode} (${option.taxRate}%)`}
                                  value={taxes.find(t => t.id === taxField.value) || null}
                                  onChange={(_, value) => taxField.onChange(value?.id || 0)}
                                  sx={{ minWidth: 120 }}
                                  renderInput={(params) => (
                                    <TextField
                                      {...params}
                                      size="small"
                                      error={!!errors.poDetails?.[index]?.taxId}
                                    />
                                  )}
                                />
                              )}
                            />
                          </TableCell>
                          <TableCell>
                            <Typography variant="body2" fontWeight="medium">
                              {formatCurrency(calculateLineTotal(index))}
                            </Typography>
                          </TableCell>
                          <TableCell>
                            <IconButton
                              size="small"
                              onClick={() => removeLineItem(index)}
                              disabled={fields.length === 1}
                              color="error"
                            >
                              <Delete />
                            </IconButton>
                          </TableCell>
                        </TableRow>
                      ))}
                    </TableBody>
                  </Table>
                </TableContainer>

                {/* Totals */}
                <Box mt={3}>
                  <Divider sx={{ mb: 2 }} />
                  <Grid container spacing={2} justifyContent="flex-end">
                    <Grid item xs={12} md={4}>
                      <Box display="flex" justifyContent="space-between" mb={1}>
                        <Typography>Subtotal:</Typography>
                        <Typography fontWeight="medium">
                          {formatCurrency(totals.subTotal)}
                        </Typography>
                      </Box>
                      <Box display="flex" justifyContent="space-between" mb={1}>
                        <Typography>Tax Amount:</Typography>
                        <Typography fontWeight="medium">
                          {formatCurrency(totals.taxAmount)}
                        </Typography>
                      </Box>
                      <Divider sx={{ my: 1 }} />
                      <Box display="flex" justifyContent="space-between">
                        <Typography variant="h6">Total Due:</Typography>
                        <Typography variant="h6" color="primary">
                          {formatCurrency(totals.totalDue)}
                        </Typography>
                      </Box>
                    </Grid>
                  </Grid>
                </Box>
              </CardContent>
            </Card>
          </Grid>

          {/* Actions */}
          <Grid item xs={12}>
            <Box display="flex" gap={2} justifyContent="flex-end">
              <Button
                variant="outlined"
                onClick={() => navigate('/purchase-orders')}
              >
                Cancel
              </Button>
              <Button
                type="submit"
                variant="contained"
                startIcon={<Save />}
                disabled={loading}
              >
                {loading ? 'Saving...' : 'Save Draft'}
              </Button>
            </Box>
          </Grid>
        </Grid>
      </form>
    </Box>
  );
};

export default PurchaseOrderForm;