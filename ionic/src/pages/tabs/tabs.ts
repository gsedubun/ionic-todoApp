import { Component } from '@angular/core';

import { AboutPage } from '../about/about';
import { ContactPage } from '../contact/contact';
import { HomePage } from '../home/home';
import { AuthProvider } from '../../providers/auth/auth';
import { NavController } from 'ionic-angular/navigation/nav-controller';
import { LoginPage } from '../login/login';

@Component({
  templateUrl: 'tabs.html'
})
export class TabsPage {

  tab1Root = HomePage;
  tab2Root = AboutPage;
  tab3Root = ContactPage;

  constructor(private auth: AuthProvider, private navCtrl: NavController) {

  }

  logOut(): void{
    this.auth.logout().then((data: any)=>{
      this.navCtrl.setRoot(LoginPage);
    }).catch((error: any)=>{
      console.dir(error);
      this.navCtrl.setRoot(LoginPage);
      
    });
  }
}
