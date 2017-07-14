import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import { Response, Headers } from '@angular/http';
import { Observable } from "rxjs/Observable";
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';

@Injectable()
export class ImgService {

    constructor(private http: Http) { };

    upload(files: any) {
        let formData = new FormData();
        //for (let i = 0; i < files.length; i++) {

        console.log(files[0]);
        formData.append("files", files[0]);

        console.log(formData);
        //}

        return this.http
            .post('/api/home', formData)
           /* .subscribe(
            data => console.log('success'),
            error => console.log(error))*/;

    }
}