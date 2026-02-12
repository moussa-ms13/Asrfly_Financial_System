import 'package:flutter/material.dart';
import 'package:supabase_flutter/supabase_flutter.dart';
import 'package:shared_preferences/shared_preferences.dart';
import 'screens/users/login_screen.dart';
import 'screens/home/home_screen.dart';

Future<void> main() async {
  WidgetsFlutterBinding.ensureInitialized();

  await Supabase.initialize(
    url: const String.fromEnvironment('SUPABASE_URL'),
    anonKey: const String.fromEnvironment('SUPABASE_ANON_KEY'),
  );

  runApp(const AsrflyMobileApp());
}

class AsrflyMobileApp extends StatelessWidget {
  const AsrflyMobileApp({super.key});

  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      title: 'Asrfly Mobile',
      debugShowCheckedModeBanner: false,
      builder: (context, child) {
        return Directionality(textDirection: TextDirection.rtl, child: child!);
      },
      theme: ThemeData(
        colorScheme: ColorScheme.fromSeed(seedColor: Colors.teal),
        useMaterial3: true,
        fontFamily: 'Cairo',
      ),
      home: const RootDecider(),
    );
  }
}

class RootDecider extends StatefulWidget {
  const RootDecider({super.key});

  @override
  State<RootDecider> createState() => _RootDeciderState();
}

class _RootDeciderState extends State<RootDecider> {
  Widget _child = const Scaffold(
    body: Center(child: CircularProgressIndicator()),
  );

  @override
  void initState() {
    super.initState();
    _decide();
  }

  Future<void> _decide() async {
    // Prefer Supabase session if present
    final session = Supabase.instance.client.auth.currentSession;
    final prefs = await SharedPreferences.getInstance();

    if (session != null) {
      // there's an active supabase session — go to home
      setState(() => _child = const HomeScreen());
      return;
    }

    // Fallback: local session stored at login (timestamp in ms)
    final lastLoginMs = prefs.getInt('lastLoginMs');
    final userName = prefs.getString('userName');
    if (lastLoginMs != null && userName != null) {
      final last = DateTime.fromMillisecondsSinceEpoch(lastLoginMs);
      final age = DateTime.now().difference(last);
      if (age.inDays < 7) {
        setState(() => _child = const HomeScreen());
        return;
      }
    }

    // otherwise show login
    setState(() => _child = const LoginScreen());
  }

  @override
  Widget build(BuildContext context) {
    return _child;
  }
}
