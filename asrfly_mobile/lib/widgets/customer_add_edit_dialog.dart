import 'package:flutter/material.dart';
import '../services/customer_service.dart';

class CustomerAddEditDialog extends StatefulWidget {
  final String? customerId;
  final String? initialName;
  final String? initialPhone;
  final String? initialAddress;
  final String? initialEmail;
  final String? initialDetails;
  final VoidCallback onSaved;

  const CustomerAddEditDialog({
    super.key,
    this.customerId,
    this.initialName,
    this.initialPhone,
    this.initialAddress,
    this.initialEmail,
    this.initialDetails,
    required this.onSaved,
  });

  @override
  State<CustomerAddEditDialog> createState() => _CustomerAddEditDialogState();
}

class _CustomerAddEditDialogState extends State<CustomerAddEditDialog> {
  late TextEditingController nameController;
  late TextEditingController phoneController;
  late TextEditingController addressController;
  late TextEditingController emailController;
  late TextEditingController detailsController;
  final CustomerService customerService = CustomerService();
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

  Future<void> _saveCustomer() async {
    if (nameController.text.isEmpty) {
      ScaffoldMessenger.of(
        context,
      ).showSnackBar(const SnackBar(content: Text('يرجى إدخال اسم العميل')));
      return;
    }

    setState(() => isLoading = true);

    try {
      if (widget.customerId == null) {
        await customerService.addCustomer(
          nameController.text,
          phoneController.text.isEmpty ? null : phoneController.text,
          addressController.text.isEmpty ? null : addressController.text,
          emailController.text.isEmpty ? null : emailController.text,
          detailsController.text.isEmpty ? null : detailsController.text,
        );
        if (!mounted) return;
        ScaffoldMessenger.of(
          context,
        ).showSnackBar(const SnackBar(content: Text('تم إضافة العميل بنجاح')));
      } else {
        await customerService.updateCustomer(
          int.parse(widget.customerId!),
          nameController.text,
          phoneController.text.isEmpty ? null : phoneController.text,
          addressController.text.isEmpty ? null : addressController.text,
          emailController.text.isEmpty ? null : emailController.text,
          detailsController.text.isEmpty ? null : detailsController.text,
        );
        if (!mounted) return;
        ScaffoldMessenger.of(
          context,
        ).showSnackBar(const SnackBar(content: Text('تم تحديث العميل بنجاح')));
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
        widget.customerId == null ? 'إضافة عميل جديد' : 'تعديل العميل',
      ),
      content: SingleChildScrollView(
        child: Column(
          mainAxisSize: MainAxisSize.min,
          children: [
            TextField(
              controller: nameController,
              decoration: InputDecoration(
                labelText: 'اسم العميل',
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
          onPressed: isLoading ? null : _saveCustomer,
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
