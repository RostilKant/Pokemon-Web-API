import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-dashboard-page',
  templateUrl: './dashboard-page.component.html',
  styleUrls: ['./dashboard-page.component.scss']
})
export class DashboardPageComponent implements OnInit {

  constructor() { }

  ngOnInit(): void {
    // if (localStorage.getItem('jwt-token') == null){
    //   this.router.navigate(['/admin', 'login']);
    // }
  }

}
