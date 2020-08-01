import 'package:flutter/material.dart';

class AppDrawer extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return Drawer(
      child: ListView(
        padding: EdgeInsets.all(0),
        children: [
          DrawerHeader(
            child: Container(),
          ),
          ListTile(
            title: Text('Inicio'),
            leading: Icon(Icons.home),
            onTap: () => Navigator.of(context).pushReplacementNamed('/'),
          ),
          Divider(),
          ListTile(
            title: Text('Ventas'),
            leading: Icon(Icons.attach_money),
            onTap: () {},
          ),
          Divider(),
          ListTile(
            title: Text('Compras'),
            leading: Icon(Icons.attach_money),
            onTap: () {},
          ),
          Divider(),
          ListTile(
            title: Text('Productos'),
            leading: Icon(Icons.shopping_cart),
            onTap: () => Navigator.of(context).pushReplacementNamed('/products'),
          ),
          Divider(),
          ListTile(
            title: Text('Clientes'),
            leading: Icon(Icons.people),
            onTap: () {},
          ),
          Divider(),
          ListTile(
            title: Text('Historial'),
            leading: Icon(Icons.list),
            onTap: () {},
          ),
          Divider(),
          ListTile(
            title: Text('Usuarios'),
            leading: Icon(Icons.supervised_user_circle),
            onTap: () {},
          ),
          Divider(),
          ListTile(
            title: Text('Ajustes'),
            leading: Icon(Icons.settings),
            onTap: () {},
          ),
          Divider(),
          ListTile(
            title: Text('Ayuda'),
            leading: Icon(Icons.help),
            onTap: () {},
          ),
          Divider(),
        ],
      ),
    );
  }
}
