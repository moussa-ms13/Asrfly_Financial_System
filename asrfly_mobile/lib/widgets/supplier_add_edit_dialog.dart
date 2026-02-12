import 'package:flutter/material.dart';
import '../services/supplier_service.dart';

class SupplierAddEditDialog extends StatefulWidget {
  final String? supplierId;
  final String? initialName;
  final String? initialPhone;
  final String? initialAddress;
  final String? initialEmail;
  final String? initialDetails;
  final VoidCallback onSaved;

  const SupplierAddEditDialog({
    super.key,
    this.supplierId,
    this.initialName,
    this.initialPhone,
    this.initialAddress,
    this.initialEmail,
    this.initialDetails,
    required this.onSaved,
  });

  @override
  State<SupplierAddEditDialog> createState() => _SupplierAddEditDialogState();
}

class _SupplierAddEditDialogState extends State<SupplierAddEditDialog> {
  late TextEditingController nameController;
  late TextEditingController phoneController;
  late TextEditingController addressController;
  late TextEditingController emailController;
  late TextEditingController detailsController;
  final SupplierService supplierService = SupplierService();
  bool isLoading = false;

  @override
  void initState() {
    super.initState();
    nameController = TextEditingController(text: widget.initialName ?? '');
    phoneController = TextEditingController(text: widget.initialPhone ?? '');
    addressController = TextEditingController(
      text: widget.initialAddress ?? '',
    );
    emailController = TextEditingController(text: widget.initialEmail ?? '');
    detailsController = TextEditingController(
      text: widget.initialDetails ?? '',
    );
  }

  @override
  void dispose() {
    nameController.dispose();
    phoneController.dispose();
    addressController.dispose();
    emailController.dispose();
    detailsController.dispose();
    super.dispose();
  }

  Future<void> _saveSupplier() async {
    if (nameController.text.isEmpty) {
      ScaffoldMessenger.of(
        context,
      ).showSnackBar(const SnackBar(content: Text('يرجى إدخال اسم المورد')));
      return;
    }

    setState(() => isLoading = true);

    try {
      if (widget.supplierId == null) {
        await supplierService.addSupplier(
          nameController.text,
          phoneController.text.isEmpty ? null : phoneController.text,
          addressController.text.isEmpty ? null : addressController.text,
          emailController.text.isEmpty ? null : emailController.text,
          detailsController.text.isEmpty ? null : detailsController.text,
        );
        if (!mounted) return;
        ScaffoldMessenger.of(
          context,
        ).showSnackBar(const SnackBar(content: Text('تم إضافة المورد بنجاح')));
      } else {
        await supplierService.updateSupplier(
          int.parse(widget.supplierId!),
          nameController.text,
          phoneController.text.isEmpty ? null : phoneController.text,
          addressController.text.isEmpty ? null : addressController.text,
          emailController.text.isEmpty ? null : emailController.text,
          detailsController.text.isEmpty ? null : detailsController.text,
        );
        if (!mounted) return;
        ScaffoldMessenger.of(
          context,
        ).showSnackBar(const SnackBar(content: Text('تم تحديث المورد بنجاح')));
      }
      widget.onSaved();
      if (!mounted) return;
      Navigator.pop(context);
    } catch (e) {
      if (!mounted) return;
      ScaffoldMessenger.of(
        context,
      ).showSnackBar(SnackBar(content: Text('خطأ: $e')));
    } finally {
      if (mounted) setState(() => isLoading = false);
    }
  }

  @override
  Widget build(BuildContext context) {
    return AlertDialog(
      title: Text(
        widget.supplierId == null ? 'إضافة مورد جديد' : 'تعديل المورد',
      ),
      content: SingleChildScrollView(
        child: Column(
          mainAxisSize: MainAxisSize.min,
          children: [
            TextField(
              controller: nameController,
              decoration: InputDecoration(
                labelText: 'اسم المورد',
                border: OutlineInputBorder(
                  borderRadius: BorderRadius.circular(8),
                ),
              ),
              enabled: !isLoading,
            ),
            const SizedBox(height: 12),
            TextField(
              controller: phoneController,
              decoration: InputDecoration(
                labelText: 'رقم الهاتف (اختياري)',
                border: OutlineInputBorder(
                  borderRadius: BorderRadius.circular(8),
                ),
              ),
              keyboardType: TextInputType.phone,
              enabled: !isLoading,
            ),
            const SizedBox(height: 12),
            TextField(
              controller: addressController,
              decoration: InputDecoration(
                labelText: 'العنوان (اختياري)',
                border: OutlineInputBorder(
                  borderRadius: BorderRadius.circular(8),
                ),
              ),
              enabled: !isLoading,
            ),
            const SizedBox(height: 12),
            TextField(
              controller: emailController,
              decoration: InputDecoration(
                labelText: 'البريد الالكتروني (اختياري)',
                border: OutlineInputBorder(
                  borderRadius: BorderRadius.circular(8),
                ),
              ),
              keyboardType: TextInputType.emailAddress,
              enabled: !isLoading,
            ),
            const SizedBox(height: 12),
            TextField(
              controller: detailsController,
              decoration: InputDecoration(
                labelText: 'التفاصيل (اختياري)',
                border: OutlineInputBorder(
                  borderRadius: BorderRadius.circular(8),
                ),
              ),
              maxLines: 3,
              enabled: !isLoading,
            ),
          ],
        ),
      ),
      actions: [
        TextButton(
          onPressed: isLoading ? null : () => Navigator.pop(context),
          child: const Text('إلغاء'),
        ),
        ElevatedButton(
          onPressed: isLoading ? null : _saveSupplier,
          style: ElevatedButton.styleFrom(
            backgroundColor: Colors.teal,
            foregroundColor: Colors.white,
          ),
          child: isLoading
              ? const SizedBox(
                  width: 20,
                  height: 20,
                  child: CircularProgressIndicator(strokeWidth: 2),
                )
              : const Text('حفظ'),
        ),
      ],
    );
  }
}
