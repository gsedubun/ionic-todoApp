import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { TodoItem } from '../../models/TodoItem';

/*
  Generated class for the AuthProvider provider.
  login(arg0: any, arg1: any): any {
    throw new Error("Method not implemented.");  login(arg0: any, arg1: any): any {
    throw new Error("Method not implemented.");
  }

  }

  See https://angular.io/guide/dependency-injection for more info on providers
  and Angular DI.
*/
@Injectable()
export class AuthProvider {  
  
  logout(): any {
    return new Promise((resolve, reject)=>{
      this.http.post('', JSON.stringify('')).subscribe(data=>{
        resolve(data);
      },error=>{
        reject(error);
      });
    })
  }

   apiUrl = 'http://localhost:5000/token';

  constructor(public http: HttpClient) {
    console.log('Hello AuthProvider Provider');
  }
  login(email: String, password: String) {
    return new Promise((resolve,reject)=>{
      this.http.post(this.apiUrl,JSON.stringify({email,password})).subscribe(data =>{
        resolve(data);
      }, error=>{
        reject(error);
      });
    });
  }
}
