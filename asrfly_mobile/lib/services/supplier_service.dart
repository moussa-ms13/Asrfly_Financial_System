import 'package:supabase_flutter/supabase_flutter.dart';
import '../models/supplier_model.dart';

class SupplierService {
  final supabase = Supabase.instance.client;

  Future<List<SupplierModel>> fetchSuppliers() async {
    try {
      final data = await supabase
          .from('Suppliers')
          .select()
          .order('Id', ascending: true);
      return (data as List).map((e) => SupplierModel.fromMap(e)).toList();
    } catch (e) {
      throw Exception('فشل في تحميل الموردين: $e');
    }
  }

  Future<List<SupplierModel>> searchSuppliers(String query) async {
    try {
      final data = await supabase
          .from('Suppliers')
          .select()
          .ilike('Name', '%$query%')
          .order('Id', ascending: true);
      return (data as List).map((e) => SupplierModel.fromMap(e)).toList();
    } catch (e) {
      throw Exception('فشل في البحث عن الموردين: $e');
    }
  }

  Future<void> addSupplier(
    String name,
    String? phone,
    String? address,
    String? email,
    String? details,
  ) async {
    try {
      await supabase.from('Suppliers').insert({
        'Name': name,
        'PhoneNumber': phone,
        'Address': address,
        'Email': email,
        'Details': details,
        'Balance': 0,
      });
    } catch (e) {
      throw Exception('فشل في إضافة المورد: $e');
    }
  }

  Future<void> updateSupplier(
    int id,
    String name,
    String? phone,
    String? address,
    String? email,
    String? details,
  ) async {
    try {
      await supabase
          .from('Suppliers')
          .update({
            'Name': name,
            'PhoneNumber': phone,
            'Address': address,
            'Email': email,
            'Details': details,
          })
          .eq('Id', id);
    } catch (e) {
      throw Exception('فشل في تحديث المورد: $e');
    }
  }

  Future<void> deleteSupplier(int id) async {
    try {
      await supabase.from('Suppliers').delete().eq('Id', id);
    } catch (e) {
      throw Exception('فشل في حذف المورد: $e');
    }
  }
}
