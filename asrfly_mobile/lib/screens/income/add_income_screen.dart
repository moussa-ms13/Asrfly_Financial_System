import 'package:flutter/material.dart';
import 'dart:io';
import 'dart:typed_data';
import 'package:image_picker/image_picker.dart';
import 'package:supabase_flutter/supabase_flutter.dart';
import '../../models/category_model.dart';
import '../../models/project_model.dart';
import '../../models/customer_model.dart';
import '../../models/income_model.dart';
import '../../widgets/customer_add_edit_dialog.dart';

class AddIncomeScreen extends StatefulWidget {
  final int? initialProjectId;
  final IncomeModel? editIncome;

  const AddIncomeScreen({super.key, this.initialProjectId, this.editIncome});

  @override
  State<AddIncomeScreen> createState() => _AddIncomeScreenState();
}

class _AddIncomeScreenState extends State<AddIncomeScreen> {
  final _formKey = GlobalKey<FormState>();

  int? _selectedCategoryId;
  int? _selectedProjectId;
  int? _selectedCustomerId;

  final TextEditingController _customerController = TextEditingController();

  final TextEditingController _amountController = TextEditingController();
  final TextEditingController _recNoController = TextEditingController();
  final TextEditingController _detailsController = TextEditingController();
  DateTime _selectedDate = DateTime.now();
  bool _isLoading = false;
  File? _receiptImage;
  String? _existingImageName;
  final ImagePicker _picker = ImagePicker();

  List<CategoryModel> _categories = [];
  List<ProjectModel> _projects = [];
  List<CustomerModel> _customers = [];

  @override
  void initState() {
    super.initState();
    _selectedProjectId = widget.initialProjectId;
    _loadData();
    // Prefill when editing
    if (widget.editIncome != null) {
      final e = widget.editIncome!;
      _selectedCategoryId = e.categoryId;
      _selectedProjectId = e.projectId ?? widget.initialProjectId;
      _selectedCustomerId = e.supplierId;
      _amountController.text = e.amount.toString();
      _recNoController.text = e.recNo ?? '';
      _detailsController.text = e.details ?? '';
      _selectedDate = e.incomeDate ?? DateTime.now();
      _existingImageName = e.image;
    }
  }

  Future<void> _loadData() async {
    final client = Supabase.instance.client;
    final catsData = await client.from('Categories').select();
    final projsData = await client.from('Projects').select();
    final custsData = await client.from('Customers').select();

    if (mounted) {
      setState(() {
        _categories = (catsData as List)
            .map((e) => CategoryModel.fromMap(e))
            .toList();
        _projects = (projsData as List)
            .map((e) => ProjectModel.fromMap(e))
            .toList();
        _customers = (custsData as List)
            .map((e) => CustomerModel.fromMap(e))
            .toList();
        if (_selectedCustomerId != null) {
          final found = _customers.firstWhere(
            (c) => c.id == _selectedCustomerId,
            orElse: () => CustomerModel(id: 0, name: '', balance: 0.0),
          );
          if (found.id != 0) _customerController.text = found.name;
        }
      });
    }
  }

  Future<void> _saveIncome() async {
    if (!_formKey.currentState!.validate()) return;
    if (_selectedCategoryId == null ||
        _selectedProjectId == null ||
        _selectedCustomerId == null) {
      ScaffoldMessenger.of(
        context,
      ).showSnackBar(const SnackBar(content: Text("أكمل الحقول المطلوبة")));
      return;
    }

    setState(() => _isLoading = true);
    try {
      String? imagePath = _existingImageName;
      if (_receiptImage != null) {
        imagePath = await _uploadImage();
      }

      if (widget.editIncome == null) {
        await Supabase.instance.client.from('Income').insert({
          'CategoryId': _selectedCategoryId,
          'ProjectId': _selectedProjectId,
          'SupplierId': _selectedCustomerId,
          'Amount': double.parse(_amountController.text),
          'IncomeDate': _selectedDate.toIso8601String(),
          'RecNo': _recNoController.text,
          'Details': _detailsController.text.isEmpty
              ? null
              : _detailsController.text,
          'Image': imagePath,
        });
      } else {
        final updateMap = <String, dynamic>{
          'CategoryId': _selectedCategoryId,
          'ProjectId': _selectedProjectId,
          'SupplierId': _selectedCustomerId,
          'Amount': double.parse(_amountController.text),
          'IncomeDate': _selectedDate.toIso8601String(),
          'RecNo': _recNoController.text.isEmpty ? null : _recNoController.text,
          'Details': _detailsController.text.isEmpty
              ? null
              : _detailsController.text,
          'Image': imagePath,
        };

        await Supabase.instance.client
            .from('Income')
            .update(updateMap)
            .eq('Id', widget.editIncome!.id);
      }

      if (!mounted) return;
      Navigator.pop(context, true);
    } catch (e) {
      if (!mounted) return;
      ScaffoldMessenger.of(
        context,
      ).showSnackBar(SnackBar(content: Text("خطأ: $e")));
    } finally {
      if (mounted) setState(() => _isLoading = false);
    }
  }

  Future<String?> _uploadImage() async {
    if (_receiptImage == null) return null;
    try {
      final fileName =
          'receipt_income_${DateTime.now().millisecondsSinceEpoch}.jpg';
      await Supabase.instance.client.storage
          .from('receipts')
          .upload(
            fileName,
            _receiptImage!,
            fileOptions: const FileOptions(cacheControl: '3600', upsert: false),
          );
      return fileName;
    } catch (e) {
      throw "فشل رفع الصورة: $e";
    }
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text("إضافة مقبوضات"),
        backgroundColor: Colors.green,
      ),
      body: SingleChildScrollView(
        padding: const EdgeInsets.all(20),
        child: Form(
          key: _formKey,
          child: Column(
            children: [
              DropdownButtonFormField<int>(
                value: _selectedCategoryId,
                decoration: const InputDecoration(
                  labelText: "الصنف *",
                  border: OutlineInputBorder(),
                ),
                items: _categories
                    .map(
                      (c) => DropdownMenuItem(value: c.id, child: Text(c.name)),
                    )
                    .toList(),
                onChanged: (val) => setState(() => _selectedCategoryId = val),
              ),
              const SizedBox(height: 15),

              Row(
                children: [
                  Expanded(
                    child: GestureDetector(
                      onTap: () async {
                        final selectedId = await showModalBottomSheet<int>(
                          context: context,
                          isScrollControlled: true,
                          builder: (context) {
                            String query = '';
                            return StatefulBuilder(
                              builder: (context, setModalState) {
                                final filtered = _customers
                                    .where(
                                      (c) => c.name.toLowerCase().contains(
                                        query.toLowerCase(),
                                      ),
                                    )
                                    .toList();
                                return Padding(
                                  padding: EdgeInsets.only(
                                    bottom: MediaQuery.of(
                                      context,
                                    ).viewInsets.bottom,
                                  ),
                                  child: Column(
                                    mainAxisSize: MainAxisSize.min,
                                    children: [
                                      Padding(
                                        padding: const EdgeInsets.all(8.0),
                                        child: TextField(
                                          decoration: const InputDecoration(
                                            prefixIcon: Icon(Icons.search),
                                            hintText: 'ابحث',
                                            border: OutlineInputBorder(),
                                          ),
                                          onChanged: (v) =>
                                              setModalState(() => query = v),
                                        ),
                                      ),
                                      SizedBox(
                                        height: 300,
                                        child: ListView.builder(
                                          itemCount: filtered.length,
                                          itemBuilder: (context, index) {
                                            final c = filtered[index];
                                            return ListTile(
                                              title: Text(c.name),
                                              onTap: () =>
                                                  Navigator.pop(context, c.id),
                                            );
                                          },
                                        ),
                                      ),
                                    ],
                                  ),
                                );
                              },
                            );
                          },
                        );
                        if (selectedId != null) {
                          if (!mounted) return;
                          final found = _customers.firstWhere(
                            (c) => c.id == selectedId,
                          );
                          setState(() {
                            _selectedCustomerId = selectedId;
                            _customerController.text = found.name;
                          });
                        }
                      },
                      child: AbsorbPointer(
                        child: TextField(
                          controller: _customerController,
                          readOnly: true,
                          decoration: const InputDecoration(
                            labelText: "العميل *",
                            border: OutlineInputBorder(),
                          ),
                        ),
                      ),
                    ),
                  ),
                  const SizedBox(width: 8),
                  IconButton(
                    onPressed: () {
                      showDialog(
                        context: context,
                        builder: (context) => CustomerAddEditDialog(
                          onSaved: () async {
                            await _loadData();
                            if (!mounted) return;
                            Navigator.of(this.context).pop();
                          },
                        ),
                      );
                    },
                    icon: const Icon(Icons.add, color: Colors.teal),
                  ),
                ],
              ),
              const SizedBox(height: 15),

              DropdownButtonFormField<int>(
                value: _selectedProjectId,
                decoration: const InputDecoration(
                  labelText: "المشروع *",
                  border: OutlineInputBorder(),
                ),
                items: _projects
                    .map(
                      (p) => DropdownMenuItem(value: p.id, child: Text(p.name)),
                    )
                    .toList(),
                onChanged: widget.initialProjectId != null
                    ? null
                    : (val) => setState(() => _selectedProjectId = val),
                disabledHint: _projects.isNotEmpty && _selectedProjectId != null
                    ? Text(
                        _projects
                            .firstWhere((e) => e.id == _selectedProjectId)
                            .name,
                      )
                    : null,
              ),
              const SizedBox(height: 15),

              TextFormField(
                controller: _amountController,
                keyboardType: TextInputType.number,
                decoration: const InputDecoration(
                  labelText: "المبلغ القبوض *",
                  border: OutlineInputBorder(),
                ),
                validator: (val) =>
                    (val == null || val.isEmpty) ? "مطلوب" : null,
              ),
              const SizedBox(height: 15),

              TextFormField(
                controller: _recNoController,
                decoration: const InputDecoration(
                  labelText: "رقم الوصل / الملاحظة",
                  border: OutlineInputBorder(),
                ),
              ),
              const SizedBox(height: 15),

              TextFormField(
                controller: _detailsController,
                maxLines: 3,
                decoration: const InputDecoration(
                  labelText: "التفاصيل (اختياري)",
                  border: OutlineInputBorder(),
                ),
              ),
              const SizedBox(height: 15),

              const Text(
                "إرفاق وصل",
                style: TextStyle(fontWeight: FontWeight.bold),
              ),
              Row(
                mainAxisAlignment: MainAxisAlignment.spaceEvenly,
                children: [
                  TextButton.icon(
                    onPressed: () async {
                      final XFile? picked = await _picker.pickImage(
                        source: ImageSource.camera,
                      );
                      if (picked != null)
                        setState(() => _receiptImage = File(picked.path));
                    },
                    icon: const Icon(Icons.camera_alt),
                    label: const Text("كاميرا"),
                  ),
                  TextButton.icon(
                    onPressed: () async {
                      final XFile? picked = await _picker.pickImage(
                        source: ImageSource.gallery,
                      );
                      if (picked != null)
                        setState(() => _receiptImage = File(picked.path));
                    },
                    icon: const Icon(Icons.image),
                    label: const Text("المعرض"),
                  ),
                ],
              ),
              if (_receiptImage != null)
                Image.file(_receiptImage!, height: 100)
              else if (_existingImageName != null)
                TextButton.icon(
                  onPressed: () async {
                    final name = _existingImageName;
                    if (name == null) return;
                    final ctx = context;
                    final messenger = ScaffoldMessenger.of(ctx);
                    try {
                      final Uint8List res = await Supabase
                          .instance
                          .client
                          .storage
                          .from('receipts')
                          .download(name);
                      if (!mounted) return;
                      showDialog(
                        context: ctx,
                        builder: (context) => AlertDialog(
                          content: Image.memory(res),
                          actions: [
                            TextButton(
                              onPressed: () => Navigator.pop(context),
                              child: const Text('إغلاق'),
                            ),
                          ],
                        ),
                      );
                    } catch (e) {
                      if (!mounted) return;
                      messenger.showSnackBar(
                        SnackBar(content: Text('خطأ: $e')),
                      );
                    }
                  },
                  icon: const Icon(Icons.receipt_long),
                  label: const Text('عرض الوصل الحالي'),
                ),

              const SizedBox(height: 20),
              SizedBox(
                width: double.infinity,
                height: 50,
                child: ElevatedButton(
                  onPressed: _isLoading ? null : _saveIncome,
                  style: ElevatedButton.styleFrom(
                    backgroundColor: Colors.green,
                  ),
                  child: _isLoading
                      ? const CircularProgressIndicator(color: Colors.white)
                      : const Text(
                          "حفظ العملية",
                          style: TextStyle(color: Colors.white),
                        ),
                ),
              ),
            ],
          ),
        ),
      ),
    );
  }
}
