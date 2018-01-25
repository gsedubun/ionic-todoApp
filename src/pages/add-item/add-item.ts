import { Component } from '@angular/core';
import { NavController, ViewController, NavParams } from 'ionic-angular';

/**
 * Generated class for the AddItemPage page.
 *
 * See https://ionicframework.com/docs/components/#navigation for more info on
 * Ionic pages and navigation.
 */

@Component({
  selector: 'page-add-item',
  templateUrl: 'add-item.html',
})
export class AddItemPage {
  title: string;
  description: string;
  constructor(public navCtrl: NavController, public navParams: NavParams, public viewCtrl: ViewController) {
  }

  ionViewDidLoad() {
    //console.log('ionViewDidLoad AddItemPage');
  }
  saveItem() {
    let newItem = {
      title: this.title,
      description: this.description
    }
    this.viewCtrl.dismiss(newItem);
  }
  close() {
    this.viewCtrl.dismiss();
  }
}
