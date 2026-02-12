class ProjectModel {
  final int id;
  final String name;
  final String? details;
  final String? customer;
  final String? address;
  final String? company;
  final DateTime? startDate;
  final DateTime? finishDate;
  final double income;
  final double outcome;
  final double balance;

  ProjectModel({
    required this.id,
    required this.name,
    this.details,
    this.customer,
    this.address,
    this.company,
    this.startDate,
    this.finishDate,
    required this.income,
    required this.outcome,
    required this.balance,
  });

  factory ProjectModel.fromMap(Map<String, dynamic> map) {
    return ProjectModel(
      id: map['Id'],
      name: map['Name'] ?? '',
      details: map['Details'],
      customer: map['Customer'],
      address: map['Address'],
      company: map['Company'],
      startDate: map['StartDate'] != null
          ? DateTime.tryParse(map['StartDate'].toString())
          : null,
      finishDate: map['FinishDate'] != null
          ? DateTime.tryParse(map['FinishDate'].toString())
          : null,
      income: (map['Income'] ?? 0).toDouble(),
      outcome: (map['Outcome'] ?? 0).toDouble(),
      balance: (map['Balance'] ?? 0).toDouble(),
    );
  }
}
