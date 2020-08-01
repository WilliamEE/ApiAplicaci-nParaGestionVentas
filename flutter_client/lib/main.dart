import 'package:flutter/material.dart';
import 'package:flutter_client/ui/app/app_home_page.dart';
import 'package:flutter_client/ui/products/products_screen.dart';

Future<void> main() async {
  WidgetsFlutterBinding.ensureInitialized();
  runApp(MyApp());
}

class MyApp extends StatelessWidget {
  // This widget is the root of your application.
  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      title: 'Flutter Demo',
      debugShowCheckedModeBanner: false,
      theme: ThemeData(
        primarySwatch: Colors.blue,
        visualDensity: VisualDensity.adaptivePlatformDensity,
      ),
      routes: {
        '/': (BuildContext context) => AppHomePage(),
        '/products': (BuildContext context) => ProductsScreen()
      },
    );
  }
}
