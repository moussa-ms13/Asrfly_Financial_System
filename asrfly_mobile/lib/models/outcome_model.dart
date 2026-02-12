class OutcomeModel {
  final int id;
  final int? projectId;
  final int? categoryId;
  final int? supplierId; // This stores Supplier ID (not Customer)
  final double amount;
  final String? details;
  final String? recNo;
  final String? image;
  final DateTime? outcomeDate;
  final String? categoryName;
  final String? supplierName;

  OutcomeModel({
    required this.id,
    this.projectId,
    this.categoryId,
    this.supplierId,
    required this.amount,
    this.details,
    this.recNo,
    this.image,
    this.outcomeDate,
    this.categoryName,
    this.supplierName,
  });

  // Getters for compatibility
  String get categoryNameSafe => categoryName ?? 'غير معلوم';
  String get supplierNameSafe => supplierName ?? 'غير معلوم';
  DateTime get date => outcomeDate ?? DateTime.now();

  factory OutcomeModel.fromMap(Map<String, dynamic> map) {
    String? catName;
    String? suppName;
    if (map['Categories'] != null && map['Categories'] is Map) {
      catName = (map['Categories'] as Map)['Name']?.toString();
    }
    if (map['Suppliers'] != null && map['Suppliers'] is Map) {
      suppName = (map['Suppliers'] as Map)['Name']?.toString();
    }

    return OutcomeModel(
      id: map['Id'] as int? ?? 0,
      projectId: map['ProjectId'] as int?,
      categoryId: map['CategoryId'] as int?,
      supplierId: map['SupplierId'] as int?, // Supplier ID
      amount: (map['Amount'] as num?)?.toDouble() ?? 0.0,
      details: map['Details'] as String?,
      recNo: map['RecNo'] as String?,
      image: map['Image'] as String?,
      outcomeDate: map['OutcomeDate'] != null
          ? DateTime.tryParse(map['OutcomeDate'].toString())
          : null,
      categoryName: catName,
      supplierName: suppName,
    );
  }

  Map<String, dynamic> toMap() {
    return {
      'Id': id,
      'ProjectId': projectId,
      'CategoryId': categoryId,
      'SupplierId': supplierId, // Supplier ID
      'Amount': amount,
      'Details': details,
      'RecNo': recNo,
      'Image': image,
      'OutcomeDate': outcomeDate?.toIso8601String(),
    };
  }
}
