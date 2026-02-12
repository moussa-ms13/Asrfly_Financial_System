import 'dart:io';
import 'dart:typed_data';
import 'package:flutter/material.dart';
import 'package:image_picker/image_picker.dart';
import 'package:supabase_flutter/supabase_flutter.dart';
import '../../models/category_model.dart';
import '../../models/project_model.dart';
import '../../models/supplier_model.dart';
import '../../models/outcome_model.dart';
import '../../widgets/supplier_add_edit_dialog.dart';

class AddOutcomeScreen extends StatefulWidget {
  final int? initialProjectId;
  final OutcomeModel? editOutcome;

  const AddOutcomeScreen({super.key, this.initialProjectId, this.editOutcome});

  @override
  State<AddOutcomeScreen> createState() => _AddOutcomeScreenState();
}

class _AddOutcomeScreenState extends State<AddOutcomeScreen> {
  final _formKey = GlobalKey<FormState>();

  int? _selectedCategoryId;
  int? _selectedProjectId;
  int? _selectedSupplierId;

  final TextEditingController _amountController = TextEditingController();
  final TextEditingController _reasonController = TextEditingController();
  final TextEditingController _detailsController = TextEditingController();
  final TextEditingController _supplierController = TextEditingController();
  DateTime _selectedDate = DateTime.now();

  File? _receiptImage;
  String? _existingImageName;
  final ImagePicker _picker = ImagePicker();
  bool _isLoading = false;

  List<CategoryModel> _categories = [];
  List<ProjectModel> _projects = [];
  List<SupplierModel> _suppliers = [];

  @override
  void initState() {
    super.initState();
    _selectedProjectId = widget.initialProjectId;
    _loadData();
    if (widget.editOutcome != null) {
      final e = widget.editOutcome!;
      _selectedCategoryId = e.categoryId;
      _selectedProjectId = e.projectId ?? widget.initialProjectId;
      _selectedSupplierId = e.supplierId;
      _amountController.text = e.amount.toString();
      _reasonController.text = e.recNo ?? '';
      _detailsController.text = e.details ?? '';
      _selectedDate = e.outcomeDate ?? DateTime.now();
      _existingImageName = e.image;
    }
  }

  Future<void> _loadData() async {
    final client = Supabase.instance.client;

    final catsData = await client.from('Categories').select();
    final projsData = await client.from('Projects').select();
    final suppsData = await client.from('Suppliers').select();

    if (mounted) {
      setState(() {
        _categories = (catsData as List)
            .map((e) => CategoryModel.fromMap(e))
            .toList();
        _projects = (projsData as List)
            .map((e) => ProjectModel.fromMap(e))
            .toList();
        _suppliers = (suppsData as List)
            .map((e) => SupplierModel.fromMap(e))
            .toList();
        if (_selectedSupplierId != null) {
          final found = _suppliers.firstWhere(
            (s) => s.id == _selectedSupplierId,
            orElse: () => SupplierModel(id: 0, name: '', balance: 0.0),
          );
          if (found.id != 0) _supplierController.text = found.name;
        }
      });
    }
  }

  Future<String?> _uploadImage() async {
    if (_receiptImage == null) return null;
    try {
      final fileName = 'receipt_${DateTime.now().millisecondsSinceEpoch}.jpg';
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

  Future<void> _saveOutcome() async {
    if (!_formKey.currentState!.validate()) return;

    if (_selectedCategoryId == null ||
        _selectedProjectId == null ||
        _selectedSupplierId == null) {
      ScaffoldMessenger.of(context).showSnackBar(
        const SnackBar(content: Text("الرجاء اختيار الصنف، المشروع، والمورد")),
      );
      return;
    }

    setState(() => _isLoading = true);

    try {
      String? imagePath = _existingImageName;
      if (_receiptImage != null) {
        imagePath = await _uploadImage();
      }

      if (widget.editOutcome == null) {
        await Supabase.instance.client.from('Outcome').insert({
          'CategoryId': _selectedCategoryId,
          'ProjectId': _selectedProjectId,
          'SupplierId': _selectedSupplierId,
          'Amount': double.parse(_amountController.text),
          'OutcomeDate': _selectedDate.toIso8601String(),
          'RecNo': _reasonController.text,
          'Details': _detailsController.text.isEmpty
              ? null
              : _detailsController.text,
          'Image': imagePath,
        });
      } else {
        final updateMap = <String, dynamic>{
          'CategoryId': _selectedCategoryId,
          'ProjectId': _selectedProjectId,
          'SupplierId': _selectedSupplierId,
          'Amount': double.parse(_amountController.text),
          'OutcomeDate': _selectedDate.toIso8601String(),
          'RecNo': _reasonController.text.isEmpty
              ? null
              : _reasonController.text,
          'Details': _detailsController.text.isEmpty
              ? null
              : _detailsController.text,
          'Image': imagePath,
        };

        await Supabase.instance.client
            .from('Outcome')
            .update(updateMap)
            .eq('Id', widget.editOutcome!.id);
      }

      if (!mounted) return;
      ScaffoldMessenger.of(
        context,
      ).showSnackBar(const SnackBar(content: Text("✅ تم الحفظ بنجاح")));
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

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text("إضافة مصروف"),
        backgroundColor: Colors.redAccent,
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
                                final filtered = _suppliers
                                    .where(
                                      (s) => s.name.toLowerCase().contains(
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
                                            final s = filtered[index];
                                            return ListTile(
                                              title: Text(s.name),
                                              onTap: () =>
                                                  Navigator.pop(context, s.id),
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
                          final found = _suppliers.firstWhere(
                            (s) => s.id == selectedId,
                          );
                          setState(() {
                            _selectedSupplierId = selectedId;
                            _supplierController.text = found.name;
                          });
                        }
                      },
                      child: AbsorbPointer(
                        child: TextField(
                          controller: _supplierController,
                          readOnly: true,
                          decoration: const InputDecoration(
                            labelText: "المورد *",
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
                        builder: (context) => SupplierAddEditDialog(
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
                            .firstWhere(
                              (element) => element.id == _selectedProjectId,
                            )
                            .name,
                      )
                    : null,
              ),
              const SizedBox(height: 15),

              TextFormField(
                controller: _amountController,
                keyboardType: TextInputType.number,
                decoration: const InputDecoration(
                  labelText: "المبلغ (دج) *",
                  border: OutlineInputBorder(),
                ),
                validator: (val) =>
                    (val == null || val.isEmpty) ? "مطلوب" : null,
              ),
              const SizedBox(height: 15),

              TextFormField(
                controller: _reasonController,
                decoration: const InputDecoration(
                  labelText: "رقم الوصل / البيان",
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
              const SizedBox(height: 20),

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

              const SizedBox(height: 30),
              SizedBox(
                width: double.infinity,
                height: 50,
                child: ElevatedButton(
                  onPressed: _isLoading ? null : _saveOutcome,
                  style: ElevatedButton.styleFrom(
                    backgroundColor: Colors.redAccent,
                  ),
                  child: _isLoading
                      ? const CircularProgressIndicator(color: Colors.white)
                      : const Text(
                          "حفـظ المصروف",
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
