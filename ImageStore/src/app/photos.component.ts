import { Component, OnInit } from '@angular/core';
import { Http } from "@angular/http";

@Component({
    selector: 'photos-app',
    template: `<br/>
<div *ngFor="let u of urls">
<img src="{{u.path}}" width="300" height="200"> Url:<a href="{{u.path}}">{{u.path}}</a>
    </div>
`
})
export class PhotosComponent implements OnInit {
    private urls: inform[];
    constructor(private http: Http) { }

    ngOnInit() {
        this.http.get('/api/home/photos').subscribe(res => {
            this.urls = res.json();
            for (var i in this.urls) {
                this.urls[i].path = res.url.substr(0, res.url.indexOf("/api")) + this.urls[i].path;
            }
        })
    }
}

class inform {
    id: any;
    name: string;
    path: string;
    user: string;
}