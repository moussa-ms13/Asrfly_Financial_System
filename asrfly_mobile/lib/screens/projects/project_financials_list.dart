import 'package:flutter/material.dart';
import 'package:supabase_flutter/supabase_flutter.dart';
import 'dart:typed_data';
import '../../models/income_model.dart';
import '../../models/outcome_model.dart';
import '../income/add_income_screen.dart';
import '../outcome/add_outcome_screen.dart';

class ProjectFinancialsList extends StatefulWidget {
  final int projectId;
  final bool isIncome;

  const ProjectFinancialsList({
    super.key,
    required this.projectId,
    required this.isIncome,
  });

  @override
  State<ProjectFinancialsList> createState() => _ProjectFinancialsListState();
}

class _ProjectFinancialsListState extends State<ProjectFinancialsList> {
  List<dynamic> _data = [];
  bool _isLoading = true;
  late TextEditingController _amountController;
  late TextEditingController _detailsController;
  late TextEditingController _recNoController;

  @override
  void initState() {
    super.initState();
    _fetchData();
    _amountController = TextEditingController();
    _detailsController = TextEditingController();
    _recNoController = TextEditingController();
  }

  @override
  void dispose() {
    _amountController.dispose();
    _detailsController.dispose();
    _recNoController.dispose();
    super.dispose();
  }

  Future<void> _fetchData() async {
    if (mounted) setState(() => _isLoading = true);
    final client = Supabase.instance.client;
    final table = widget.isIncome ? 'Income' : 'Outcome';

    try {
      final query = client
          .from(table)
          .select(
            widget.isIncome
                // Income table in this schema uses SupplierId for the payer; join Suppliers
                ? '*, Categories(Name), Projects(Name), Suppliers(Name)'
                : '*, Categories(Name), Projects(Name), Suppliers(Name)',
          )
          .eq('ProjectId', widget.projectId)
          .order(
            widget.isIncome ? 'IncomeDate' : 'OutcomeDate',
            ascending: false,
          );

      final dynamic response = await query.timeout(const Duration(seconds: 12));

      print('DEBUG _fetchData response.runtimeType => ${response.runtimeType}');
      print('DEBUG _fetchData response => $response');

      final List parsed = [];
      if (response is List) {
        for (var e in response) {
          try {
            parsed.add(
              widget.isIncome
                  ? IncomeModel.fromMap(e as Map<String, dynamic>)
                  : OutcomeModel.fromMap(e as Map<String, dynamic>),
            );
          } catch (_) {
          }
        }
      } else if (response is Map && response['data'] is List) {
        for (var e in (response['data'] as List)) {
          try {
            parsed.add(
              widget.isIncome
                  ? IncomeModel.fromMap(e as Map<String, dynamic>)
                  : OutcomeModel.fromMap(e as Map<String, dynamic>),
            );
          } catch (_) {}
        }
      } else if (response is Map &&
          (response['error'] != null || response['message'] != null)) {
        final msg = response['error'] ?? response['message'] ?? 'خطأ في الخادم';
        if (mounted)
          ScaffoldMessenger.of(
            context,
          ).showSnackBar(SnackBar(content: Text('خطأ من الخادم: $msg')));
      } else if (response is String) {
        if (mounted)
          ScaffoldMessenger.of(context).showSnackBar(
            const SnackBar(content: Text('استجابة غير متوقعة من الخادم')),
          );
      }

      if (!mounted) return;
      setState(() {
        _data = parsed;
        _isLoading = false;
      });
    } catch (e) {
      if (mounted) setState(() => _isLoading = false);
      if (mounted) {
        ScaffoldMessenger.of(
          context,
        ).showSnackBar(SnackBar(content: Text("خطأ في تحميل البيانات: $e")));
      }
    }
  }

  Future<void> _deleteItem(int id) async {
    final table = widget.isIncome ? 'Income' : 'Outcome';
    try {
      await Supabase.instance.client.from(table).delete().eq('Id', id);
      _fetchData();
      if (mounted)
        ScaffoldMessenger.of(
          context,
        ).showSnackBar(const SnackBar(content: Text("تم الحذف بنجاح")));
    } catch (e) {
      if (mounted) {
        ScaffoldMessenger.of(
          context,
        ).showSnackBar(SnackBar(content: Text("خطأ في الحذف: $e")));
      }
    }
  }

  Future<Uint8List?> _downloadReceipt(String fileName) async {
    try {
      final res = await Supabase.instance.client.storage
          .from('receipts')
          .download(fileName);
      return res as Uint8List?;
    } catch (e) {
      return null;
    }
  }

  Future<void> _updateItem(
    int id,
    double amount,
    String? details,
    String? recNo,
  ) async {
    final table = widget.isIncome ? 'Income' : 'Outcome';
    try {
      final Map<String, dynamic> updateMap = {'Amount': amount};
      if (details != null) updateMap['Details'] = details;
      if (recNo != null) updateMap['RecNo'] = recNo;

      try {
        await Supabase.instance.client
            .from(table)
            .update(updateMap)
            .eq('Id', id);
      } catch (e) {
        final msg = e.toString();
        if (msg.contains("could not find the 'Details' column") ||
            msg.contains("Could not find the 'Details' column")) {
          updateMap.remove('Details');
          await Supabase.instance.client
              .from(table)
              .update(updateMap)
              .eq('Id', id);
        } else if (msg.contains("could not find the 'RecNo' column") ||
            msg.contains("Could not find the 'RecNo' column")) {
          updateMap.remove('RecNo');
          await Supabase.instance.client
              .from(table)
              .update(updateMap)
              .eq('Id', id);
        } else {
          rethrow;
        }
      }

      _fetchData();
      if (mounted) {
        Navigator.pop(context);
        ScaffoldMessenger.of(
          context,
        ).showSnackBar(const SnackBar(content: Text("تم التحديث بنجاح")));
      }
    } catch (e) {
      if (mounted) {
        ScaffoldMessenger.of(
          context,
        ).showSnackBar(SnackBar(content: Text("خطأ في التحديث: $e")));
      }
    }
  }

  void _showEditDialog(dynamic item) {
    _amountController.text = item.amount.toString();
    _detailsController.text = item.details ?? '';
    _recNoController.text = item.recNo ?? '';

    showDialog(
      context: context,
      builder: (context) => AlertDialog(
        title: Text(widget.isIncome ? "تعديل القبض" : "تعديل الصرف"),
        content: SingleChildScrollView(
          child: Column(
            mainAxisSize: MainAxisSize.min,
            children: [
              TextField(
                controller: _amountController,
                keyboardType: const TextInputType.numberWithOptions(
                  decimal: true,
                ),
                decoration: const InputDecoration(
                  labelText: "المبلغ",
                  border: OutlineInputBorder(),
                ),
              ),
              const SizedBox(height: 12),
              TextField(
                controller: _detailsController,
                maxLines: 2,
                decoration: const InputDecoration(
                  labelText: "التفاصيل",
                  border: OutlineInputBorder(),
                ),
              ),
              const SizedBox(height: 12),
              TextField(
                controller: _recNoController,
                decoration: const InputDecoration(
                  labelText: "رقم الوصل",
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
            onPressed: () {
              final amount = double.tryParse(_amountController.text) ?? 0;
              if (amount > 0) {
                _updateItem(
                  item.id,
                  amount,
                  _detailsController.text.isEmpty
                      ? null
                      : _detailsController.text,
                  _recNoController.text.isEmpty ? null : _recNoController.text,
                );
              } else {
                ScaffoldMessenger.of(context).showSnackBar(
                  const SnackBar(
                    content: Text("المبلغ يجب أن يكون أكبر من صفر"),
                  ),
                );
              }
            },
            child: const Text("تحديث"),
          ),
        ],
      ),
    );
  }

  void _showOptions(dynamic item) {
    showModalBottomSheet(
      context: context,
      builder: (context) => Container(
        padding: const EdgeInsets.all(20),
        child: Column(
          mainAxisSize: MainAxisSize.min,
          children: [
            Text(
              widget.isIncome ? "خيارات القبض" : "خيارات الصرف",
              style: const TextStyle(fontWeight: FontWeight.bold, fontSize: 18),
            ),
            const Divider(),
            ListTile(
              leading: const Icon(Icons.edit, color: Colors.blue),
              title: const Text("تعديل السجل"),
              onTap: () {
                Navigator.pop(context);
                // Open full form for editing and refresh on return
                if (widget.isIncome) {
                  Navigator.push(
                    context,
                    MaterialPageRoute(
                      builder: (context) => AddIncomeScreen(
                        initialProjectId: widget.projectId,
                        editIncome: item as IncomeModel,
                      ),
                    ),
                  ).then((v) {
                    if (v == true) _fetchData();
                  });
                } else {
                  Navigator.push(
                    context,
                    MaterialPageRoute(
                      builder: (context) => AddOutcomeScreen(
                        initialProjectId: widget.projectId,
                        editOutcome: item as OutcomeModel,
                      ),
                    ),
                  ).then((v) {
                    if (v == true) _fetchData();
                  });
                }
              },
            ),
            ListTile(
              leading: const Icon(Icons.delete, color: Colors.red),
              title: const Text("حذف السجل"),
              onTap: () {
                Navigator.pop(context);
                _deleteItem(item.id);
              },
            ),
          ],
        ),
      ),
    );
  }

  @override
  Widget build(BuildContext context) {
    if (_isLoading) return const Center(child: CircularProgressIndicator());
    if (_data.isEmpty)
      return const Center(
        child: Text(
          "لا توجد بيانات لهذا المشروع",
          style: TextStyle(color: Colors.grey),
        ),
      );

    return RefreshIndicator(
      onRefresh: _fetchData,
      child: ListView.builder(
        padding: const EdgeInsets.only(
          bottom: 80,
          top: 10,
          left: 10,
          right: 10,
        ),
        itemCount: _data.length,
        itemBuilder: (context, index) {
          final item = _data[index];
          final isInc = widget.isIncome;

          return Card(
            elevation: 2,
            margin: const EdgeInsets.symmetric(vertical: 6),
            child: ListTile(
              leading: CircleAvatar(
                backgroundColor: isInc
                    ? Colors.green.shade50
                    : Colors.red.shade50,
                child: Icon(
                  isInc ? Icons.arrow_downward : Icons.arrow_upward,
                  color: isInc ? Colors.green : Colors.red,
                ),
              ),
              title: Text(
                item.recNo != null && item.recNo!.isNotEmpty
                    ? "وصل رقم: ${item.recNo}"
                    : "بدون رقم وصل",
                style: const TextStyle(fontWeight: FontWeight.bold),
              ),
              subtitle: Column(
                crossAxisAlignment: CrossAxisAlignment.start,
                children: [
                  Text(
                    "التصنيف: ${item is IncomeModel ? (item.categoryName ?? 'غير معلوم') : (item is OutcomeModel ? (item.categoryName ?? 'غير معلوم') : 'غير معلوم')}",
                  ),
                  if (isInc)
                    Text(
                      "العميل: ${(item as IncomeModel).customerName ?? 'غير معلوم'}",
                    )
                  else
                    Text(
                      "المورد: ${(item as OutcomeModel).supplierName ?? 'غير معلوم'}",
                    ),
                  Text(
                    item.date.toString().split(' ')[0],
                    style: const TextStyle(fontSize: 12, color: Colors.grey),
                  ),
                  if ((item is IncomeModel &&
                          (item.image != null && item.image!.isNotEmpty)) ||
                      (item is OutcomeModel &&
                          (item.image != null && item.image!.isNotEmpty)))
                    GestureDetector(
                      onTap: () async {
                        final fileName = item.image as String?;
                        if (fileName == null) return;
                        final data = await _downloadReceipt(fileName);
                        if (data == null) {
                          if (mounted)
                            ScaffoldMessenger.of(context).showSnackBar(
                              const SnackBar(content: Text('فشل تحميل الوصل')),
                            );
                          return;
                        }
                        if (!mounted) return;
                        showDialog(
                          context: context,
                          builder: (context) => AlertDialog(
                            content: Image.memory(data),
                            actions: [
                              TextButton(
                                onPressed: () => Navigator.pop(context),
                                child: const Text('إغلاق'),
                              ),
                            ],
                          ),
                        );
                      },
                      child: Padding(
                        padding: const EdgeInsets.only(top: 8.0),
                        child: Row(
                          children: [
                            const Icon(
                              Icons.receipt_long,
                              size: 20,
                              color: Colors.grey,
                            ),
                            const SizedBox(width: 8),
                            const Text(
                              'عرض الوصل',
                              style: TextStyle(color: Colors.grey),
                            ),
                          ],
                        ),
                      ),
                    ),
                ],
              ),
              trailing: Text(
                "${item.amount} دج",
                style: TextStyle(
                  color: isInc ? Colors.green : Colors.red,
                  fontWeight: FontWeight.bold,
                  fontSize: 15,
                ),
              ),
              onLongPress: () => _showOptions(item),
            ),
          );
        },
      ),
    );
  }
}
