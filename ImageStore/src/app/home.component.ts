import { Component, ViewChild, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Http } from '@angular/http';

@Component({
    selector: 'home-app',
    templateUrl: './home.component.html'
})

export class HomeComponent implements OnInit {

    @ViewChild("fileInput") fileInput;
    constructor(private router: Router, private http: Http) { }

    addFile() {
        let fi = this.fileInput.nativeElement;
        if (fi.files) {
            let formData = new FormData();
            formData.append("files", fi.files[0]);

            return this.http.post('/api/home', formData)
                .subscribe(res => {
                    if (res.status == 201) {
                        let url = res.url.substr(0, res.url.indexOf("/api")) + res.text();
                        this.router.navigate(
                            ['/about'],
                            {
                                queryParams: {
                                    'url': url
                                }
                            }
                        );
                    }
                    else {
                        alert("Выберите файл!");
                    }
                });
        }
    }

    ngOnInit() {
        this.http.get('/api/home').subscribe(res => {
            console.log(res.text());
        });
    }
}