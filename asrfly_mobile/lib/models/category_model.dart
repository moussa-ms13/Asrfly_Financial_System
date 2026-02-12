class CategoryModel {
  final int id;
  final String name;
  final String? type;
  final String? details;
  final double balance;

  CategoryModel({
    required this.id,
    required this.name,
    this.type,
    this.details,
    required this.balance,
  });

  factory CategoryModel.fromMap(Map<String, dynamic> map) {
    return CategoryModel(
      id: map['Id'],
      name: map['Name'] ?? '',
      type: map['Type'],
      details: map['Details'],
      balance: (map['Balance'] ?? 0).toDouble(),
    );
  }
}
