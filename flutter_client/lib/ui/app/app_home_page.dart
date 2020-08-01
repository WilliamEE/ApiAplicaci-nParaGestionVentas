import 'package:flutter/material.dart';
import 'package:flutter_client/ui/app/app_drawer.dart';

class AppHomePage extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: Text('Inicio'),
      ),
      drawer: AppDrawer(),
    );
  }
}
