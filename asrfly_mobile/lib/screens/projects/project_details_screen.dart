import 'package:flutter/material.dart';
import '../../models/project_model.dart';
import '../outcome/add_outcome_screen.dart';
import '../income/add_income_screen.dart';
import 'project_financials_list.dart';

class ProjectDetailsScreen extends StatefulWidget {
  final ProjectModel project;
  final int initialTab;
  const ProjectDetailsScreen({
    super.key,
    required this.project,
    this.initialTab = 0,
  });

  @override
  State<ProjectDetailsScreen> createState() => _ProjectDetailsScreenState();
}

class _ProjectDetailsScreenState extends State<ProjectDetailsScreen>
    with SingleTickerProviderStateMixin {
  late TabController _tabController;

  @override
  void initState() {
    super.initState();
    _tabController = TabController(
      length: 2,
      vsync: this,
      initialIndex: widget.initialTab,
    );
    _tabController.addListener(() {
      setState(() {});
    });
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: Text(widget.project.name),
        backgroundColor: Colors.teal,
        foregroundColor: Colors.white,
        bottom: TabBar(
          controller: _tabController,
          indicatorColor: Colors.white,
          labelColor: Colors.white,
          unselectedLabelColor: Colors.white70,
          tabs: const [
            Tab(text: "المصروفات", icon: Icon(Icons.arrow_upward)),
            Tab(text: "المقبوضات", icon: Icon(Icons.arrow_downward)),
          ],
        ),
      ),
      body: TabBarView(
        controller: _tabController,
        children: [
          ProjectFinancialsList(projectId: widget.project.id, isIncome: false),

          ProjectFinancialsList(projectId: widget.project.id, isIncome: true),
        ],
      ),
      floatingActionButton: FloatingActionButton.extended(
        backgroundColor: _tabController.index == 0 ? Colors.red : Colors.green,
        icon: const Icon(Icons.add, color: Colors.white),
        label: Text(
          _tabController.index == 0 ? "مصروف للمشروع" : "قبض للمشروع",
          style: const TextStyle(color: Colors.white),
        ),
        onPressed: () {
          if (_tabController.index == 0) {
            Navigator.push(
              context,
              MaterialPageRoute(
                builder: (context) =>
                    AddOutcomeScreen(initialProjectId: widget.project.id),
              ),
            ).then((_) => setState(() {}));
          } else {
            Navigator.push(
              context,
              MaterialPageRoute(
                builder: (context) =>
                    AddIncomeScreen(initialProjectId: widget.project.id),
              ),
            ).then((_) => setState(() {}));
          }
        },
      ),
    );
  }
}
