import { Component, OnDestroy } from '@angular/core';
import { Subscription } from "rxjs/Subscription";
import { ActivatedRoute } from "@angular/router";

@Component({
    selector: 'about-app',
    template: `<h3>Здесь будет загруженное фото  Url: {{url}}</h3>`
})
export class AboutComponent implements OnDestroy{
    private url: string;
    
    private querySubscription: Subscription;
    constructor(private route: ActivatedRoute) {
        
        this.querySubscription = route.queryParams.subscribe(
            (queryParam: any) => {
                this.url = queryParam['url'];
            }
        );
    }
    ngOnDestroy() {
        this.querySubscription.unsubscribe();
    }
}