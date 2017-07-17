import { Component, OnDestroy } from '@angular/core';
import { Subscription } from "rxjs/Subscription";
import { ActivatedRoute } from "@angular/router";

@Component({
    selector: 'about-app',
    template: `<img width="300" height="200" src="{{url}}"> Url:<a href="{{url}}">{{url}}</a>`
})
export class AboutComponent implements OnDestroy {
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