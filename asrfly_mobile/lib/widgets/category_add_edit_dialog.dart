import 'package:flutter/material.dart';
import '../services/category_service.dart';

class CategoryAddEditDialog extends StatefulWidget {
  final String? categoryId;
  final String? initialName;
  final String? initialType;
  final String? initialDetails;
  final VoidCallback onSaved;

  const CategoryAddEditDialog({
    super.key,
    this.categoryId,
    this.initialName,
    this.initialType,
    this.initialDetails,
    required this.onSaved,
  });

  @override
  State<CategoryAddEditDialog> createState() => _CategoryAddEditDialogState();
}

class _CategoryAddEditDialogState extends State<CategoryAddEditDialog> {
  late TextEditingController nameController;
  late TextEditingController detailsController;
  String? _selectedType;
  final CategoryService categoryService = CategoryService();
  bool isLoading = false;

  @override
  void initState() {
    super.initState();
    nameController = TextEditingController(text: widget.initialName ?? '');
    _selectedType = widget.initialType;
    detailsController = TextEditingController(
      text: widget.initialDetails ?? '',
    );
  }

  @override
  void dispose() {
    nameController.dispose();
    detailsController.dispose();
    super.dispose();
  }

  Future<void> _saveCategory() async {
    if (nameController.text.isEmpty) {
      ScaffoldMessenger.of(
        context,
      ).showSnackBar(const SnackBar(content: Text('يرجى إدخال اسم الصنف')));
      return;
    }

    setState(() => isLoading = true);

    try {
      if (widget.categoryId == null) {
        await categoryService.addCategory(
          nameController.text,
          _selectedType == null || _selectedType!.isEmpty
              ? null
              : _selectedType,
          detailsController.text.isEmpty ? null : detailsController.text,
        );
        if (!mounted) return;
        ScaffoldMessenger.of(
          context,
        ).showSnackBar(const SnackBar(content: Text('تم إضافة الصنف بنجاح')));
      } else {
        await categoryService.updateCategory(
          int.parse(widget.categoryId!),
          nameController.text,
          _selectedType == null || _selectedType!.isEmpty
              ? null
              : _selectedType,
          detailsController.text.isEmpty ? null : detailsController.text,
        );
        if (!mounted) return;
        ScaffoldMessenger.of(
          context,
        ).showSnackBar(const SnackBar(content: Text('تم تحديث الصنف بنجاح')));
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
      title: Text(widget.categoryId == null ? 'إضافة صنف جديد' : 'تعديل الصنف'),
      content: SingleChildScrollView(
        child: Column(
          mainAxisSize: MainAxisSize.min,
          children: [
            TextField(
              controller: nameController,
              decoration: InputDecoration(
                labelText: 'اسم الصنف',
                border: OutlineInputBorder(
                  borderRadius: BorderRadius.circular(8),
                ),
              ),
              enabled: !isLoading,
            ),
            const SizedBox(height: 16),
            DropdownButtonFormField<String>(
              value: _selectedType,
              items: const [
                DropdownMenuItem(value: 'قبض', child: Text('قبض')),
                DropdownMenuItem(value: 'صرف', child: Text('صرف')),
              ],
              onChanged: (v) => setState(() => _selectedType = v),
              decoration: InputDecoration(
                labelText: 'النوع',
                border: OutlineInputBorder(
                  borderRadius: BorderRadius.circular(8),
                ),
              ),
            ),
            const SizedBox(height: 16),
            TextField(
              controller: detailsController,
              maxLines: 3,
              decoration: InputDecoration(
                labelText: 'التفاصيل (اختياري)',
                border: OutlineInputBorder(
                  borderRadius: BorderRadius.circular(8),
                ),
              ),
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
          onPressed: isLoading ? null : _saveCategory,
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
