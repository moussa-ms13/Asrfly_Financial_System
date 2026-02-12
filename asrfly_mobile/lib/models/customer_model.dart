class CustomerModel {
  final int id;
  final String name;
  final String? phone;
  final String? address;
  final String? email;
  final String? details;
  final double balance;

  CustomerModel({
    required this.id,
    required this.name,
    this.phone,
    this.address,
    this.email,
    this.details,
    required this.balance,
  });

  factory CustomerModel.fromMap(Map<String, dynamic> map) {
    return CustomerModel(
      id: map['Id'],
      name: map['Name'] ?? '',
      phone: map['PhoneNumber'],
      address: map['Address'],
      email: map['Email'],
      details: map['Details'],
      balance: (map['Balance'] ?? 0).toDouble(),
    );
  }
}
