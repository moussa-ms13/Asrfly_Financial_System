import 'package:flutter/material.dart';
import 'package:supabase_flutter/supabase_flutter.dart';
import '../../widgets/customer_add_edit_dialog.dart';
import '../../models/project_model.dart';

class AddProjectDialog extends StatefulWidget {
  final ProjectModel? projectToEdit;
  final VoidCallback onSaved;

  const AddProjectDialog({
    super.key,
    this.projectToEdit,
    required this.onSaved,
  });

  @override
  State<AddProjectDialog> createState() => _AddProjectDialogState();
}

class _AddProjectDialogState extends State<AddProjectDialog> {
  final _nameController = TextEditingController();
  final _addressController = TextEditingController();
  final _companyController = TextEditingController();
  final _detailsController = TextEditingController();
  final TextEditingController _startDateController = TextEditingController();
  final TextEditingController _finishDateController = TextEditingController();
  final TextEditingController _customerController = TextEditingController();
  DateTime? _startDate;
  DateTime? _finishDate;
  bool _isLoading = false;

  @override
  void initState() {
    if (widget.projectToEdit != null) {
      _nameController.text = widget.projectToEdit!.name;
      _detailsController.text = widget.projectToEdit!.details ?? '';
      _selectedCustomer = widget.projectToEdit!.customer;
      _customerController.text = _selectedCustomer ?? '';
      _addressController.text = widget.projectToEdit!.address ?? '';
      _companyController.text = widget.projectToEdit!.company ?? '';
      _startDate = widget.projectToEdit!.startDate;
      _finishDate = widget.projectToEdit!.finishDate;
      _startDateController.text = _startDate != null
          ? _startDate!.toIso8601String().split('T')[0]
          : '';
      _finishDateController.text = _finishDate != null
          ? _finishDate!.toIso8601String().split('T')[0]
          : '';
    }
    _loadCustomers();
    super.initState();
  }

  List<String> _customers = [];
  String? _selectedCustomer;

  Future<void> _loadCustomers() async {
    try {
      final data = await Supabase.instance.client
          .from('Customers')
          .select('Name')
          .order('Name');
      final list = (data as List)
          .map((e) => (e['Name'] ?? '').toString())
          .where((s) => s.isNotEmpty)
          .toList();
      setState(() => _customers = list);
    } catch (_) {}
  }

  Future<void> _save() async {
    if (_nameController.text.isEmpty) return;
    setState(() => _isLoading = true);

    if (widget.projectToEdit == null) {
      await Supabase.instance.client.from('Projects').insert({
        'Name': _nameController.text,
        'Customer': (_selectedCustomer == null || _selectedCustomer!.isEmpty)
            ? null
            : _selectedCustomer,
        'Address': _addressController.text.isEmpty
            ? null
            : _addressController.text,
        'Company': _companyController.text.isEmpty
            ? null
            : _companyController.text,
        'StartDate': _startDate?.toIso8601String(),
        'FinishDate': _finishDate?.toIso8601String(),
        'Details': _detailsController.text.isEmpty
            ? null
            : _detailsController.text,
      });
    } else {
      await Supabase.instance.client
          .from('Projects')
          .update({
            'Name': _nameController.text,
            'Customer':
                (_selectedCustomer == null || _selectedCustomer!.isEmpty)
                ? null
                : _selectedCustomer,
            'Address': _addressController.text.isEmpty
                ? null
                : _addressController.text,
            'Company': _companyController.text.isEmpty
                ? null
                : _companyController.text,
            'StartDate': _startDate?.toIso8601String(),
            'FinishDate': _finishDate?.toIso8601String(),
            'Details': _detailsController.text.isEmpty
                ? null
                : _detailsController.text,
          })
          .eq('Id', widget.projectToEdit!.id);
    }

    widget.onSaved();
    if (mounted) Navigator.pop(context);
  }

  @override
  Widget build(BuildContext context) {
    return AlertDialog(
      title: Text(
        widget.projectToEdit == null ? "مشروع جديد" : "تعديل المشروع",
      ),
      content: SingleChildScrollView(
        child: Column(
          mainAxisSize: MainAxisSize.min,
          children: [
            TextField(
              controller: _nameController,
              decoration: const InputDecoration(
                labelText: "اسم المشروع",
                border: OutlineInputBorder(),
              ),
            ),
            const SizedBox(height: 12),
            Row(
              children: [
                Expanded(
                  child: GestureDetector(
                    onTap: () async {
                      final selected = await showModalBottomSheet<String>(
                        context: context,
                        isScrollControlled: true,
                        builder: (context) {
                          String query = '';
                          return StatefulBuilder(
                            builder: (context, setModalState) {
                              final filtered = _customers
                                  .where(
                                    (c) => c.toLowerCase().contains(
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
                                            title: Text(c),
                                            onTap: () =>
                                                Navigator.pop(context, c),
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
                      if (selected != null) {
                        setState(() {
                          _selectedCustomer = selected;
                          _customerController.text = selected;
                        });
                      }
                    },
                    child: AbsorbPointer(
                      child: TextField(
                        controller: _customerController,
                        readOnly: true,
                        decoration: const InputDecoration(
                          labelText: "العميل",
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
                          await _loadCustomers();
                          if (!mounted) return;
                          Navigator.pop(context);
                        },
                      ),
                    );
                  },
                  icon: const Icon(Icons.add, color: Colors.teal),
                ),
              ],
            ),
            const SizedBox(height: 12),
            TextField(
              controller: _addressController,
              decoration: const InputDecoration(
                labelText: "العنوان",
                border: OutlineInputBorder(),
              ),
            ),
            const SizedBox(height: 12),
            TextField(
              controller: _companyController,
              decoration: const InputDecoration(
                labelText: "الشركة",
                border: OutlineInputBorder(),
              ),
            ),
            const SizedBox(height: 12),
            Row(
              children: [
                Expanded(
                  child: TextField(
                    readOnly: true,
                    controller: _startDateController,
                    decoration: const InputDecoration(
                      labelText: "تاريخ البدء",
                      border: OutlineInputBorder(),
                    ),
                    onTap: () async {
                      final dt = await showDatePicker(
                        context: context,
                        initialDate: _startDate ?? DateTime.now(),
                        firstDate: DateTime(2000),
                        lastDate: DateTime(2100),
                      );
                      if (dt != null)
                        setState(() {
                          _startDate = dt;
                          _startDateController.text = _startDate!
                              .toIso8601String()
                              .split('T')[0];
                        });
                    },
                  ),
                ),
                const SizedBox(width: 8),
                Expanded(
                  child: TextField(
                    readOnly: true,
                    controller: _finishDateController,
                    decoration: const InputDecoration(
                      labelText: "تاريخ الانتهاء",
                      border: OutlineInputBorder(),
                    ),
                    onTap: () async {
                      final dt = await showDatePicker(
                        context: context,
                        initialDate: _finishDate ?? DateTime.now(),
                        firstDate: DateTime(2000),
                        lastDate: DateTime(2100),
                      );
                      if (dt != null)
                        setState(() {
                          _finishDate = dt;
                          _finishDateController.text = _finishDate!
                              .toIso8601String()
                              .split('T')[0];
                        });
                    },
                  ),
                ),
              ],
            ),
            const SizedBox(height: 12),
            TextField(
              controller: _detailsController,
              maxLines: 3,
              decoration: const InputDecoration(
                labelText: "التفاصيل",
                border: OutlineInputBorder(),
              ),
            ),
          ],
        ),
      ),
      actions: [
        TextButton(
          onPressed: () => Navigator.pop(context),
          child: const Text("إلغاء"),
        ),
        ElevatedButton(
          onPressed: _isLoading ? null : _save,
          style: ElevatedButton.styleFrom(
            backgroundColor: Colors.teal,
            foregroundColor: Colors.white,
          ),
          child: _isLoading
              ? const SizedBox(
                  width: 20,
                  height: 20,
                  child: CircularProgressIndicator(color: Colors.white),
                )
              : const Text("حفظ"),
        ),
      ],
    );
  }
}
