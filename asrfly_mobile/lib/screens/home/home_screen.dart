import 'package:flutter/material.dart';
import 'package:supabase_flutter/supabase_flutter.dart';
import 'package:shared_preferences/shared_preferences.dart';
import 'package:fl_chart/fl_chart.dart';

import '../users/login_screen.dart';
import '../categories/categories_screen.dart';
import '../projects/projects_screen.dart';
import '../customers/customers_screen.dart';
import '../suppliers/suppliers_screen.dart';
import '../outcome/add_outcome_screen.dart';
import '../income/add_income_screen.dart';
import '../transactions/transactions_screen.dart';
import '../../services/financial_service.dart';

class HomeScreen extends StatefulWidget {
  const HomeScreen({super.key});

  @override
  State<HomeScreen> createState() => _HomeScreenState();
}

class _HomeScreenState extends State<HomeScreen> {
  final FinancialService financialService = FinancialService();

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text("الرئيسية - أصرفلي"),
        backgroundColor: Colors.teal,
        foregroundColor: Colors.white,
        elevation: 0,
      ),
      drawer: _buildDrawer(context),
      body: SingleChildScrollView(
        child: Column(
          children: [
            const SizedBox(height: 12),
            _buildQuickStats(),
            const SizedBox(height: 16),
            _buildIncomeOutcomeChart(),
            const SizedBox(height: 12),
            _buildRecentTransactions(),
            const SizedBox(height: 24),
          ],
        ),
      ),
      floatingActionButton: FloatingActionButton(
        onPressed: _showManagementMenu,
        backgroundColor: Colors.teal,
        foregroundColor: Colors.white,
        child: const Icon(Icons.menu),
      ),
    );
  }

  void _showQuickAddSheet() {
    showModalBottomSheet(
      context: context,
      builder: (context) => SafeArea(
        child: Wrap(
          children: [
            ListTile(
              leading: const Icon(Icons.add, color: Colors.green),
              title: const Text('إضافة قبض'),
              onTap: () {
                Navigator.pop(context);
                Navigator.push(
                  context,
                  MaterialPageRoute(
                    builder: (context) => const AddIncomeScreen(),
                  ),
                ).then((_) => setState(() {}));
              },
            ),
            ListTile(
              leading: const Icon(Icons.remove, color: Colors.red),
              title: const Text('إضافة صرف'),
              onTap: () {
                Navigator.pop(context);
                Navigator.push(
                  context,
                  MaterialPageRoute(
                    builder: (context) => const AddOutcomeScreen(),
                  ),
                ).then((_) => setState(() {}));
              },
            ),
          ],
        ),
      ),
    );
  }

  void _showManagementMenu() {
    showModalBottomSheet(
      context: context,
      builder: (context) => SafeArea(
        child: Wrap(
          children: [
            ListTile(
              leading: const Icon(Icons.work, color: Colors.teal),
              title: const Text('المشاريع'),
              onTap: () {
                Navigator.pop(context);
                Navigator.push(
                  context,
                  MaterialPageRoute(
                    builder: (context) => const ProjectsScreen(),
                  ),
                );
              },
            ),
            ListTile(
              leading: const Icon(Icons.category, color: Colors.teal),
              title: const Text('الأصناف'),
              onTap: () {
                Navigator.pop(context);
                Navigator.push(
                  context,
                  MaterialPageRoute(
                    builder: (context) => const CategoriesScreen(),
                  ),
                );
              },
            ),
            ListTile(
              leading: const Icon(Icons.people, color: Colors.teal),
              title: const Text('العملاء'),
              onTap: () {
                Navigator.pop(context);
                Navigator.push(
                  context,
                  MaterialPageRoute(
                    builder: (context) => const CustomersScreen(),
                  ),
                );
              },
            ),
            ListTile(
              leading: const Icon(Icons.local_shipping, color: Colors.teal),
              title: const Text('الموردون'),
              onTap: () {
                Navigator.pop(context);
                Navigator.push(
                  context,
                  MaterialPageRoute(
                    builder: (context) => const SuppliersScreen(),
                  ),
                );
              },
            ),
          ],
        ),
      ),
    );
  }

  Future<String?> _getDisplayName() async {
    try {
      final user = Supabase.instance.client.auth.currentUser;
      if (user != null) {
        final metaName =
            user.userMetadata?['name'] ?? user.userMetadata?['Name'];
        if (metaName != null && metaName is String && metaName.isNotEmpty)
          return metaName;
      }

      final prefs = await SharedPreferences.getInstance();
      final name = prefs.getString('userName');
      return name;
    } catch (_) {
      return null;
    }
  }

  Future<void> _handleLogout(BuildContext context) async {
    try {
      await Supabase.instance.client.auth.signOut();
    } catch (_) {}
    try {
      final prefs = await SharedPreferences.getInstance();
      await prefs.remove('userName');
      await prefs.remove('lastLoginMs');
    } catch (_) {}
    if (!mounted) return;
    Navigator.pushAndRemoveUntil(
      context,
      MaterialPageRoute(builder: (c) => const LoginScreen()),
      (route) => false,
    );
  }

  Widget _buildDrawer(BuildContext context) {
    return Drawer(
      child: ListView(
        padding: EdgeInsets.zero,
        children: [
          FutureBuilder<String?>(
            future: _getDisplayName(),
            builder: (context, snap) {
              final name = snap.data ?? 'ضيف';
              return UserAccountsDrawerHeader(
                decoration: const BoxDecoration(color: Colors.teal),
                accountName: Text(
                  name,
                  style: const TextStyle(
                    fontWeight: FontWeight.bold,
                    fontSize: 18,
                  ),
                ),
                accountEmail: const Text(''),
                currentAccountPicture: const CircleAvatar(
                  backgroundColor: Colors.white,
                  child: Icon(Icons.person, size: 40, color: Colors.teal),
                ),
              );
            },
          ),
          _buildMenuItem(context, "الرئيسية", Icons.home, () {
            Navigator.pop(context);
          }),
          _buildMenuItem(context, "المشاريع", Icons.work, () {
            Navigator.pop(context);
            Navigator.push(
              context,
              MaterialPageRoute(builder: (context) => const ProjectsScreen()),
            );
          }),
          _buildMenuItem(context, "الأصناف", Icons.category, () {
            Navigator.pop(context);
            Navigator.push(
              context,
              MaterialPageRoute(builder: (context) => const CategoriesScreen()),
            );
          }),
          _buildMenuItem(context, "العملاء", Icons.people, () {
            Navigator.pop(context);
            Navigator.push(
              context,
              MaterialPageRoute(builder: (context) => const CustomersScreen()),
            );
          }),
          _buildMenuItem(context, "الموردون", Icons.local_shipping, () {
            Navigator.pop(context);
            Navigator.push(
              context,
              MaterialPageRoute(builder: (context) => const SuppliersScreen()),
            );
          }),
          const Divider(height: 24),
          _buildMenuItem(context, "خروج", Icons.logout, () {
            showDialog(
              context: context,
              builder: (context) => AlertDialog(
                title: const Text("تأكيد الخروج"),
                content: const Text("هل تريد تسجيل الخروج؟"),
                actions: [
                  TextButton(
                    onPressed: () => Navigator.pop(context),
                    child: const Text("إلغاء"),
                  ),
                  TextButton(
                    onPressed: () {
                      Navigator.pop(context);
                      _handleLogout(context);
                    },
                    child: const Text("خروج"),
                  ),
                ],
              ),
            );
          }),
        ],
      ),
    );
  }

  Widget _buildMenuItem(
    BuildContext context,
    String title,
    IconData icon,
    VoidCallback onTap,
  ) {
    return ListTile(
      leading: Icon(icon, color: Colors.teal),
      title: Text(
        title,
        style: const TextStyle(fontSize: 16, fontWeight: FontWeight.w500),
      ),
      onTap: onTap,
    );
  }

  Widget _buildIncomeOutcomeChart() {
    return FutureBuilder<Map<String, Map<String, double>>>(
      future: financialService.getLast7DaysIncomeOutcome(),
      builder: (context, snapshot) {
        if (!snapshot.hasData) return const SizedBox.shrink();
        final data = snapshot.data!;
        final keys = data.keys.toList()..sort();
        final groups = <BarChartGroupData>[];
        for (int i = 0; i < keys.length; i++) {
          final day = data[keys[i]]!;
          final income = day['income'] ?? 0.0;
          final outcome = day['outcome'] ?? 0.0;
          groups.add(
            BarChartGroupData(
              x: i,
              barRods: [
                BarChartRodData(toY: income, color: Colors.green, width: 8),
                BarChartRodData(toY: outcome, color: Colors.red, width: 8),
              ],
              barsSpace: 4,
            ),
          );
        }

        return Padding(
          padding: const EdgeInsets.symmetric(horizontal: 16.0),
          child: SizedBox(
            height: 220,
            child: Card(
              margin: EdgeInsets.zero,
              child: Padding(
                padding: const EdgeInsets.all(12.0),
                child: BarChart(
                  BarChartData(
                    alignment: BarChartAlignment.spaceBetween,
                    barTouchData: BarTouchData(
                      enabled: true,
                      touchTooltipData: BarTouchTooltipData(
                        tooltipBgColor: Colors.grey.shade800,
                        getTooltipItem: (group, groupIndex, rod, rodIndex) {
                          final dayKey = keys[group.x.toInt()];
                          final dayMap = data[dayKey]!;
                          final incomeVal = dayMap['income'] ?? 0.0;
                          final outcomeVal = dayMap['outcome'] ?? 0.0;
                          if (rodIndex == 0) {
                            return BarTooltipItem(
                              'إيراد: ${incomeVal.toStringAsFixed(2)}',
                              const TextStyle(color: Colors.white),
                            );
                          }
                          return BarTooltipItem(
                            'صرف: ${outcomeVal.toStringAsFixed(2)}',
                            const TextStyle(color: Colors.white),
                          );
                        },
                      ),
                    ),
                    titlesData: FlTitlesData(
                      leftTitles: AxisTitles(
                        sideTitles: SideTitles(
                          showTitles: true,
                          getTitlesWidget: (value, meta) {
                            return Text(
                              _formatAmount(value),
                              style: const TextStyle(fontSize: 10),
                            );
                          },
                          interval: null,
                        ),
                      ),
                      bottomTitles: AxisTitles(
                        sideTitles: SideTitles(
                          showTitles: true,
                          getTitlesWidget: (value, meta) {
                            final idx = value.toInt();
                            if (idx < 0 || idx >= keys.length)
                              return const SizedBox.shrink();
                            final dt = DateTime.tryParse(keys[idx]);
                            final label = dt != null
                                ? _weekdayShort(dt.weekday)
                                : keys[idx];
                            return Padding(
                              padding: const EdgeInsets.only(top: 6.0),
                              child: Text(
                                label,
                                style: const TextStyle(fontSize: 12),
                              ),
                            );
                          },
                        ),
                      ),
                    ),
                    gridData: FlGridData(show: true, drawHorizontalLine: true),
                    borderData: FlBorderData(show: false),
                    barGroups: groups,
                  ),
                ),
              ),
            ),
          ),
        );
      },
    );
  }

  String _weekdayShort(int weekday) {
    const names = ['Mon', 'Tue', 'Wed', 'Thu', 'Fri', 'Sat', 'Sun'];
    return names[(weekday - 1) % 7];
  }

  String _formatAmount(double value) {
    final v = value.abs();
    if (v >= 1000) {
      final k = (v / 1000);
      return '${k.toStringAsFixed(k >= 10 ? 0 : 1)}k';
    }
    return value.toStringAsFixed(0);
  }

  Widget _buildRecentTransactions() {
    return FutureBuilder<List<Map<String, dynamic>>>(
      future: financialService.getLastTransactions(5),
      builder: (context, snap) {
        if (!snap.hasData) return const SizedBox.shrink();
        final items = snap.data!;
        return Padding(
          padding: const EdgeInsets.symmetric(horizontal: 16.0),
          child: Card(
            child: Padding(
              padding: const EdgeInsets.all(12.0),
              child: Column(
                children: [
                  Row(
                    mainAxisAlignment: MainAxisAlignment.spaceBetween,
                    children: [
                      const Text(
                        'آخر العمليات',
                        style: TextStyle(fontWeight: FontWeight.bold),
                      ),
                      TextButton(
                        onPressed: () {
                          Navigator.push(
                            context,
                            MaterialPageRoute(
                              builder: (c) => const TransactionsScreen(),
                            ),
                          );
                        },
                        child: const Text('عرض الكل'),
                      ),
                    ],
                  ),
                  const Divider(),
                  ListView.separated(
                    shrinkWrap: true,
                    physics: const NeverScrollableScrollPhysics(),
                    itemCount: items.length,
                    separatorBuilder: (_, __) => const Divider(height: 12),
                    itemBuilder: (context, index) {
                      final it = items[index];
                      final isIncome = it['type'] == 'income';
                      final name = it['name'] ?? 'غير معروف';
                      final dt = it['date'] as DateTime;
                      final amount = it['amount'] as double;
                      return ListTile(
                        leading: Icon(
                          isIncome ? Icons.arrow_upward : Icons.arrow_downward,
                          color: isIncome ? Colors.green : Colors.red,
                        ),
                        title: Text(name),
                        subtitle: Text('${dt.day}/${dt.month}/${dt.year}'),
                        trailing: Text(
                          '${amount.toStringAsFixed(2)} دج',
                          style: TextStyle(
                            color: isIncome ? Colors.green : Colors.red,
                            fontWeight: FontWeight.bold,
                          ),
                        ),
                        onTap: () {},
                      );
                    },
                  ),
                ],
              ),
            ),
          ),
        );
      },
    );
  }

  Widget _buildQuickStats() {
    return FutureBuilder<double>(
      future: financialService.getTotalIncome(),
      builder: (context, incomeSnapshot) {
        return FutureBuilder<double>(
          future: financialService.getTotalOutcome(),
          builder: (context, outcomeSnapshot) {
            final totalIncome = incomeSnapshot.data ?? 0;
            final totalOutcome = outcomeSnapshot.data ?? 0;
            final balance = totalIncome - totalOutcome;

            return Padding(
              padding: const EdgeInsets.symmetric(horizontal: 16),
              child: Column(
                children: [
                  Row(
                    children: [
                      Expanded(
                        child: _buildStatCard(
                          "الإيرادات",
                          totalIncome,
                          Colors.green,
                          Icons.trending_up,
                        ),
                      ),
                      const SizedBox(width: 12),
                      Expanded(
                        child: _buildStatCard(
                          "النفقات",
                          totalOutcome,
                          Colors.red,
                          Icons.trending_down,
                        ),
                      ),
                      const SizedBox(width: 12),
                      Expanded(
                        child: _buildStatCard(
                          "الرصيد",
                          balance,
                          balance >= 0 ? Colors.blue : Colors.orange,
                          Icons.account_balance,
                        ),
                      ),
                    ],
                  ),
                  const SizedBox(height: 12),
                  Row(
                    children: [
                      Expanded(
                        child: ElevatedButton.icon(
                          onPressed: () {
                            Navigator.push(
                              context,
                              MaterialPageRoute(
                                builder: (context) => const AddIncomeScreen(),
                              ),
                            ).then((_) => setState(() {}));
                          },
                          icon: const Icon(Icons.add, color: Colors.white),
                          label: const Text("إضافة قبض"),
                          style: ElevatedButton.styleFrom(
                            backgroundColor: Colors.green,
                            padding: const EdgeInsets.symmetric(vertical: 12),
                            shape: RoundedRectangleBorder(
                              borderRadius: BorderRadius.circular(12),
                            ),
                          ),
                        ),
                      ),
                      const SizedBox(width: 12),
                      Expanded(
                        child: ElevatedButton.icon(
                          onPressed: () {
                            Navigator.push(
                              context,
                              MaterialPageRoute(
                                builder: (context) => const AddOutcomeScreen(),
                              ),
                            ).then((_) => setState(() {}));
                          },
                          icon: const Icon(Icons.remove, color: Colors.white),
                          label: const Text("إضافة صرف"),
                          style: ElevatedButton.styleFrom(
                            backgroundColor: Colors.redAccent,
                            padding: const EdgeInsets.symmetric(vertical: 12),
                            shape: RoundedRectangleBorder(
                              borderRadius: BorderRadius.circular(12),
                            ),
                          ),
                        ),
                      ),
                    ],
                  ),
                ],
              ),
            );
          },
        );
      },
    );
  }

  Widget _buildStatCard(
    String label,
    double value,
    Color color,
    IconData icon,
  ) {
    return Container(
      padding: const EdgeInsets.all(12),
      decoration: BoxDecoration(
        color: color.withOpacity(0.1),
        borderRadius: BorderRadius.circular(12),
        border: Border.all(color: color.withOpacity(0.3)),
      ),
      child: Column(
        crossAxisAlignment: CrossAxisAlignment.start,
        children: [
          Row(
            mainAxisAlignment: MainAxisAlignment.spaceBetween,
            children: [
              Text(
                label,
                style: TextStyle(
                  color: Colors.grey.shade700,
                  fontSize: 12,
                  fontWeight: FontWeight.w600,
                ),
              ),
              Icon(icon, color: color, size: 16),
            ],
          ),
          const SizedBox(height: 8),
          Text(
            "${value.toStringAsFixed(2)} دج",
            style: TextStyle(
              color: color,
              fontSize: 14,
              fontWeight: FontWeight.bold,
            ),
          ),
        ],
      ),
    );
  }

  // analytics chart removed — dashboard simplified to quick stats
}
