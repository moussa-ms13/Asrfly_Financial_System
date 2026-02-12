import 'package:supabase_flutter/supabase_flutter.dart';

class FinancialService {
  final supabase = Supabase.instance.client;

  Future<Map<String, double>> getLast7DaysSpending() async {
    try {
      final now = DateTime.now();
      final sevenDaysAgo = now.subtract(const Duration(days: 7));

      final data = await supabase
          .from('Outcome')
          .select('OutcomeDate, Amount')
          .gte('OutcomeDate', sevenDaysAgo.toIso8601String())
          .lte('OutcomeDate', now.toIso8601String())
          .order('OutcomeDate', ascending: true);

      // Group by day and sum amounts
      final Map<String, double> dailySpending = {};

      for (var record in data) {
        final outcomeDate = DateTime.tryParse(record['OutcomeDate'].toString());
        if (outcomeDate != null) {
          final dateKey =
              '${outcomeDate.year}-${outcomeDate.month.toString().padLeft(2, '0')}-${outcomeDate.day.toString().padLeft(2, '0')}';

          if (dailySpending.containsKey(dateKey)) {
            dailySpending[dateKey] =
                dailySpending[dateKey]! + (record['Amount'] as num).toDouble();
          } else {
            dailySpending[dateKey] = (record['Amount'] as num).toDouble();
          }
        }
      }

      // Fill missing days with 0
      for (int i = 0; i < 7; i++) {
        final date = sevenDaysAgo.add(Duration(days: i));
        final dateKey =
            '${date.year}-${date.month.toString().padLeft(2, '0')}-${date.day.toString().padLeft(2, '0')}';
        dailySpending.putIfAbsent(dateKey, () => 0);
      }

      return dailySpending;
    } catch (e) {
      throw Exception('فشل في تحميل بيانات النفقات: $e');
    }
  }

  Future<double> getTotalIncome() async {
    try {
      final data = await supabase.from('Income').select('Amount');
      double total = 0;
      for (var record in data) {
        total += (record['Amount'] as num).toDouble();
      }
      return total;
    } catch (e) {
      throw Exception('فشل في تحميل إجمالي الإيرادات: $e');
    }
  }

  Future<double> getTotalOutcome() async {
    try {
      final data = await supabase.from('Outcome').select('Amount');
      double total = 0;
      for (var record in data) {
        total += (record['Amount'] as num).toDouble();
      }
      return total;
    } catch (e) {
      throw Exception('فشل في تحميل إجمالي النفقات: $e');
    }
  }

  /// Returns a map where the key is date string `yyyy-MM-dd` and the value
  /// contains aggregated income and outcome for that day.
  Future<Map<String, Map<String, double>>> getLast7DaysIncomeOutcome() async {
    try {
      final now = DateTime.now();
      final sevenDaysAgo = now.subtract(
        const Duration(days: 6),
      ); // include today

      // Fetch incomes and outcomes in the range
      final incomes = await supabase
          .from('Income')
          .select('IncomeDate, Amount')
          .gte('IncomeDate', sevenDaysAgo.toIso8601String())
          .lte('IncomeDate', now.toIso8601String())
          .order('IncomeDate', ascending: true);

      final outcomes = await supabase
          .from('Outcome')
          .select('OutcomeDate, Amount')
          .gte('OutcomeDate', sevenDaysAgo.toIso8601String())
          .lte('OutcomeDate', now.toIso8601String())
          .order('OutcomeDate', ascending: true);

      final Map<String, Map<String, double>> result = {};

      // Initialize dates
      for (int i = 0; i < 7; i++) {
        final date = sevenDaysAgo.add(Duration(days: i));
        final key =
            '${date.year}-${date.month.toString().padLeft(2, '0')}-${date.day.toString().padLeft(2, '0')}';
        result[key] = {'income': 0.0, 'outcome': 0.0, 'profit': 0.0};
      }

      for (var rec in incomes) {
        final dt = DateTime.tryParse(rec['IncomeDate'].toString());
        if (dt == null) continue;
        final key =
            '${dt.year}-${dt.month.toString().padLeft(2, '0')}-${dt.day.toString().padLeft(2, '0')}';
        final amt = (rec['Amount'] as num).toDouble();
        result.putIfAbsent(
          key,
          () => {'income': 0.0, 'outcome': 0.0, 'profit': 0.0},
        );
        result[key]!['income'] = (result[key]!['income'] ?? 0) + amt;
      }

      for (var rec in outcomes) {
        final dt = DateTime.tryParse(rec['OutcomeDate'].toString());
        if (dt == null) continue;
        final key =
            '${dt.year}-${dt.month.toString().padLeft(2, '0')}-${dt.day.toString().padLeft(2, '0')}';
        final amt = (rec['Amount'] as num).toDouble();
        result.putIfAbsent(
          key,
          () => {'income': 0.0, 'outcome': 0.0, 'profit': 0.0},
        );
        result[key]!['outcome'] = (result[key]!['outcome'] ?? 0) + amt;
      }

      // compute profit
      result.forEach((k, v) {
        v['profit'] = (v['income'] ?? 0) - (v['outcome'] ?? 0);
      });

      return result;
    } catch (e) {
      throw Exception('فشل في تحميل بيانات التحليلات: $e');
    }
  }

  /// Returns the latest combined transactions (income + outcome) sorted by date desc
  /// Each item: { 'type': 'income'|'outcome', 'name': String?, 'date': DateTime, 'amount': double }
  Future<List<Map<String, dynamic>>> getLastTransactions(int n) async {
    try {
      final client = supabase;

      final incomes = await client
          .from('Income')
          .select('Id,Amount,IncomeDate,SupplierId,RecNo')
          .order('IncomeDate', ascending: false)
          .limit(n);

      final outcomes = await client
          .from('Outcome')
          .select('Id,Amount,OutcomeDate,SupplierId,RecNo')
          .order('OutcomeDate', ascending: false)
          .limit(n);

      // collect supplier/customer ids to resolve names
      final Set<int> supplierIds = {};
      for (var r in incomes) {
        if (r['SupplierId'] != null) supplierIds.add(r['SupplierId'] as int);
      }
      for (var r in outcomes) {
        if (r['SupplierId'] != null) supplierIds.add(r['SupplierId'] as int);
      }

      final Map<int, String> idToName = {};
      if (supplierIds.isNotEmpty) {
        // Fallback: fetch all suppliers/customers and map by id (compatible API)
        final supData = await client.from('Suppliers').select('Id,name');
        for (var s in supData) {
          if (s['Id'] != null)
            idToName[s['Id'] as int] = (s['name'] ?? '') as String;
        }

        final custData = await client.from('Customers').select('Id,name');
        for (var c in custData) {
          if (c['Id'] != null)
            idToName[c['Id'] as int] = (c['name'] ?? '') as String;
        }
      }

      final List<Map<String, dynamic>> combined = [];

      for (var r in incomes) {
        final dt = DateTime.tryParse(r['IncomeDate'].toString());
        if (dt == null) continue;
        combined.add({
          'type': 'income',
          'name': idToName[r['SupplierId']] ?? null,
          'date': dt,
          'amount': (r['Amount'] as num).toDouble(),
          'raw': r,
        });
      }

      for (var r in outcomes) {
        final dt = DateTime.tryParse(r['OutcomeDate'].toString());
        if (dt == null) continue;
        combined.add({
          'type': 'outcome',
          'name': idToName[r['SupplierId']] ?? null,
          'date': dt,
          'amount': (r['Amount'] as num).toDouble(),
          'raw': r,
        });
      }

      combined.sort(
        (a, b) => (b['date'] as DateTime).compareTo(a['date'] as DateTime),
      );

      return combined.take(n).toList();
    } catch (e) {
      throw Exception('فشل في جلب العمليات: $e');
    }
  }
}
