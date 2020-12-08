import { MediaMatcher } from '@angular/cdk/layout';
import {
    ChangeDetectorRef,
    Component,
    HostListener,
    OnDestroy,
    OnInit,
} from '@angular/core';
import { Unsubscribe } from 'redux';
import { store } from 'src/app/redux/store';

@Component({
    selector: 'app-dashboard',
    templateUrl: './dashboard.component.html',
    styleUrls: ['./dashboard.component.scss'],
})
export class DashboardComponent implements OnInit, OnDestroy {

    mobileQuery: MediaQueryList;
    opened = true;
    private _mobileQueryListener: () => void;

    public business = store.getState().business;
    private unsubscribe: Unsubscribe;

    constructor(changeDetectorRef: ChangeDetectorRef, media: MediaMatcher) {
        this.mobileQuery = media.matchMedia('(max-width: 768px)');
        this._mobileQueryListener = () => {
            this.opened = false;
            return changeDetectorRef.detectChanges();
        };
        this.mobileQuery.addListener(this._mobileQueryListener);
    }

    ngOnDestroy(): void {
        this.mobileQuery.removeListener(this._mobileQueryListener);
    }
    ngOnInit(): void {
        this.unsubscribe = store.subscribe(() => {
            this.business = store.getState().business;
        })

        if (window.innerWidth < 769) {
            this.opened = false;
        }
    }

    @HostListener('window:resize', ['$event'])
    onResize(event) {
        if (window.innerWidth > 768) {
            this.opened = true;
        }
    }
}
