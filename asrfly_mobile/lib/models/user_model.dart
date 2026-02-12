class UserModel {
  final int id;
  final String name;
  final String userName;
  final String? password;
  final String? phone;
  final String? email;
  final int roleId;

  UserModel({
    required this.id,
    required this.name,
    required this.userName,
    this.password,
    this.phone,
    this.email,
    required this.roleId,
  });

  factory UserModel.fromMap(Map<String, dynamic> map) {
    return UserModel(
      id: map['Id'],
      name: map['Name'] ?? '',
      userName: map['UserName'] ?? '',
      password: map['Password'],
      phone: map['Phone'],
      email: map['Email'],
      roleId: map['RoleId'] ?? 0,
    );
  }
}
