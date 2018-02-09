import { Component } from '@angular/core';
import { NavController,  ModalController } from 'ionic-angular';
import { AddItemPage } from '../add-item/add-item';
import { TodoServiceProvider } from '../../providers/todo-service/todo-service';
//import { getSymbolIterator } from '@angular/core/src/util';
import { TodoItem } from '../../models/TodoItem';


@Component({
  selector: 'page-home',
  templateUrl: 'home.html'
})
export class HomePage {
  public items: TodoItem[]=[];

  saveItem(item) {
    this.todoService.addTodo(item).then((data: TodoItem)=>{
      this.items.push(data);
      
    });
  }
  getItem(){
    this.todoService.getTodo().then((data: TodoItem[])=>{
        this.items=data;
    });
  }
  constructor(public navCtrl: NavController, public modalCtrl: ModalController, public todoService: TodoServiceProvider) {
    this.getItem();
  }
  
  //called pada saat view download.
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
  
  viewItem(item: any){
    alert(item.title+'-'+item.id);
  }
  removeItem(item: any){
    var idx = this.items.findIndex(i=> i=== item);
    this.todoService.remove(item);
    this.items.splice(idx,1);
  }
}
