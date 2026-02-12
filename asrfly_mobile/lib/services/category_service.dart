import 'package:supabase_flutter/supabase_flutter.dart';
import '../models/category_model.dart';

class CategoryService {
  final supabase = Supabase.instance.client;

  Future<List<CategoryModel>> fetchCategories() async {
    try {
      final data = await supabase
          .from('Categories')
          .select()
          .order('Id', ascending: true);
      return (data as List).map((e) => CategoryModel.fromMap(e)).toList();
    } catch (e) {
      throw Exception('فشل في تحميل الأصناف: $e');
    }
  }

  Future<List<CategoryModel>> searchCategories(String query) async {
    try {
      final data = await supabase
          .from('Categories')
          .select()
          .ilike('Name', '%$query%')
          .order('Id', ascending: true);
      return (data as List).map((e) => CategoryModel.fromMap(e)).toList();
    } catch (e) {
      throw Exception('فشل في البحث عن الأصناف: $e');
    }
  }

  Future<void> addCategory(String name, String? type, String? details) async {
    try {
      await supabase.from('Categories').insert({
        'Name': name,
        'Type': type,
        'Details': details,
        'Balance': 0,
      });
    } catch (e) {
      throw Exception('فشل في إضافة الصنف: $e');
    }
  }

  Future<void> updateCategory(
    int id,
    String name,
    String? type,
    String? details,
  ) async {
    try {
      await supabase
          .from('Categories')
          .update({'Name': name, 'Type': type, 'Details': details})
          .eq('Id', id);
    } catch (e) {
      throw Exception('فشل في تحديث الصنف: $e');
    }
  }

  Future<void> deleteCategory(int id) async {
    try {
      await supabase.from('Categories').delete().eq('Id', id);
    } catch (e) {
      throw Exception('فشل في حذف الصنف: $e');
    }
  }
}
