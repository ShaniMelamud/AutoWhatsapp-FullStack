import { Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-startup',
  templateUrl: './startup.component.html',
  styleUrls: ['./startup.component.scss']
})
export class StartupComponent implements OnInit {

  usman = '';
  constructor(private router: Router) { }

  ngOnInit(): void {
    setTimeout(()=>{
        this.router.navigateByUrl("/dashboard")
    }, 2000)
  }

}
