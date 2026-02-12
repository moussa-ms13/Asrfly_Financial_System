class IncomeModel {
  final int id;
  final int? projectId;
  final int? categoryId;
  final int? supplierId; // This stores Customer ID (not Supplier)
  final double amount;
  final String? details;
  final String? recNo;
  final String? image;
  final DateTime? incomeDate;
  final String? categoryName;
  final String? customerName;

  IncomeModel({
    required this.id,
    this.projectId,
    this.categoryId,
    this.supplierId,
    required this.amount,
    this.details,
    this.recNo,
    this.image,
    this.incomeDate,
    this.categoryName,
    this.customerName,
  });

  // Getters for compatibility
  String get categoryNameSafe => categoryName ?? 'غير معلوم';
  String get customerNameSafe => customerName ?? 'غير معلوم';
  DateTime get date => incomeDate ?? DateTime.now();

  factory IncomeModel.fromMap(Map<String, dynamic> map) {
    String? catName;
    String? custName;
    if (map['Categories'] != null && map['Categories'] is Map) {
      catName = (map['Categories'] as Map)['Name']?.toString();
    }
    // Some schemas use a foreign key to Customers, others reuse Suppliers for income.
    if (map['Customers'] != null && map['Customers'] is Map) {
      custName = (map['Customers'] as Map)['Name']?.toString();
    } else if (map['Suppliers'] != null && map['Suppliers'] is Map) {
      custName = (map['Suppliers'] as Map)['Name']?.toString();
    }

    return IncomeModel(
      id: map['Id'] as int? ?? 0,
      projectId: map['ProjectId'] as int?,
      categoryId: map['CategoryId'] as int?,
      supplierId: map['SupplierId'] as int?, // Customer ID
      amount: (map['Amount'] as num?)?.toDouble() ?? 0.0,
      details: map['Details'] as String?,
      recNo: map['RecNo'] as String?,
      image: map['Image'] as String?,
      incomeDate: map['IncomeDate'] != null
          ? DateTime.tryParse(map['IncomeDate'].toString())
          : null,
      categoryName: catName,
      customerName: custName,
    );
  }

  Map<String, dynamic> toMap() {
    return {
      'Id': id,
      'ProjectId': projectId,
      'CategoryId': categoryId,
      'SupplierId': supplierId, // Customer ID
      'Amount': amount,
      'Details': details,
      'RecNo': recNo,
      'Image': image,
      'IncomeDate': incomeDate?.toIso8601String(),
    };
  }
}
