import 'package:supabase_flutter/supabase_flutter.dart';
import '../models/customer_model.dart';

class CustomerService {
  final supabase = Supabase.instance.client;

  Future<List<CustomerModel>> fetchCustomers() async {
    try {
      final data = await supabase
          .from('Customers')
          .select()
          .order('Id', ascending: true);
      return (data as List).map((e) => CustomerModel.fromMap(e)).toList();
    } catch (e) {
      throw Exception('فشل في تحميل العملاء: $e');
    }
  }

  Future<List<CustomerModel>> searchCustomers(String query) async {
    try {
      final data = await supabase
          .from('Customers')
          .select()
          .ilike('Name', '%$query%')
          .order('Id', ascending: true);
      return (data as List).map((e) => CustomerModel.fromMap(e)).toList();
    } catch (e) {
      throw Exception('فشل في البحث عن العملاء: $e');
    }
  }

  Future<void> addCustomer(
    String name,
    String? phone,
    String? address,
    String? email,
    String? details,
  ) async {
    try {
      await supabase.from('Customers').insert({
        'Name': name,
        'PhoneNumber': phone,
        'Address': address,
        'Email': email,
        'Details': details,
        'Balance': 0,
      });
    } catch (e) {
      throw Exception('فشل في إضافة العميل: $e');
    }
  }

  Future<void> updateCustomer(
    int id,
    String name,
    String? phone,
    String? address,
    String? email,
    String? details,
  ) async {
    try {
      await supabase
          .from('Customers')
          .update({
            'Name': name,
            'PhoneNumber': phone,
            'Address': address,
            'Email': email,
            'Details': details,
          })
          .eq('Id', id);
    } catch (e) {
      throw Exception('فشل في تحديث العميل: $e');
    }
  }

  Future<void> deleteCustomer(int id) async {
    try {
      await supabase.from('Customers').delete().eq('Id', id);
    } catch (e) {
      throw Exception('فشل في حذف العميل: $e');
    }
  }
}
