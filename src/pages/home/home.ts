import { Component } from '@angular/core';
import { NavController,  ModalController } from 'ionic-angular';
import { AddItemPage } from '../add-item/add-item';

@Component({
  selector: 'page-home',
  templateUrl: 'home.html'
})
export class HomePage {
  saveItem(item) {
    this.items.push(item);
  }
  public items=[];
  constructor(public navCtrl: NavController, public modalCtrl: ModalController) {

  }
  ionViewDidLoad(){
    // this.items=[
    //   {title:"hi1", description:"test1"},
    //   {title:"hi2", description:"test2"},
    // ];
  }
  addItem(){
    let addModal = this.modalCtrl.create(AddItemPage);
      addModal.onDidDismiss((item)=>{
        if(item)
          this.saveItem(item);
      });
    addModal.present();
  }
  
  viewItem(){

  }

}
