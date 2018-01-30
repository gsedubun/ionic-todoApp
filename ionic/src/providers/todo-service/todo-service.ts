import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { resolveDefinition } from '@angular/core/src/view/util';

/*
  Generated class for the TodoServiceProvider provider.
  See https://angular.io/guide/dependency-injection for more info on providers
  and Angular DI.
*/
@Injectable()
export class TodoServiceProvider {
  apiUrl = "http://localhost:5000/api";
  constructor(public http: HttpClient) {
    console.log('Hello TodoServiceProvider Provider');
  }
  getTodo(){
    return new Promise(resolve => {
      this.http.get(this.apiUrl+'/todos')
        .subscribe(res => {
          resolve(res);
        },error => {
          console.log(error);
      })
    });
  }
  addTodo(data){
    return new Promise((resolve,reject)=>{
      this.http.post(this.apiUrl+'/todos', JSON.stringify(data),{
        headers: new HttpHeaders().set('Content-type','application/json').set("Access-Control-Allow-Origin","*")
      }).subscribe(data=>{
         resolve(data);
        },err=>{
          reject(err);
        });
    });
  }
  remove(data){
    return new Promise((resolve,reject)=>{
        this.http.delete(this.apiUrl+'/todos/'+data.id).subscribe(data=>{
          resolve(data);
        },error=>{
          console.log(error);
          reject(error);
        });
    });
  }

}
