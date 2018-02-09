import { Component } from '@angular/core';
import { NavController, NavParams } from 'ionic-angular';
import { FormBuilder } from '@angular/forms';
import { AuthProvider } from '../../providers/auth/auth';
import { FormGroup } from '@angular/forms';
import { Validators } from '@angular/forms';
import { TabsPage } from '../tabs/tabs';

/**
 * Generated class for the LoginPage page.
 *
 * See https://ionicframework.com/docs/components/#navigation for more info on
 * Ionic pages and navigation.
 */

// @IonicPage()
@Component({
  selector: 'page-login',
  templateUrl: 'login.html',
})
export class LoginPage {
  form: FormGroup;

  constructor(public navCtrl: NavController, public navParams: NavParams, 
    private fb: FormBuilder, private authService: AuthProvider) {
      this.form = fb.group({
        username: ['', Validators.required],
        password: ['', Validators.required]
      });
  }

  login(){
    const val = this.form.value;
    if(val.username && val.password){
      this.authService.login(val.username, val.password).then(
        (data: any)=>{
          //console.log(data);
          this.navCtrl.setRoot(TabsPage);
          //alert(data);
        }
      ).catch((error: any)=>{
        console.log(error.message);
        this.navCtrl.setRoot(TabsPage);
        
      });
    }
  }

  ionViewDidLoad() {
    console.log('ionViewDidLoad LoginPage');
  }

}
