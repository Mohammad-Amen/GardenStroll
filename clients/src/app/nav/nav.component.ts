import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrl: './nav.component.css'
})
export class NavComponent implements OnInit {

  model: any = {};

  constructor(private http: HttpClient) {}

  ngOnInit(): void {
    
  }

  login(): void {
    this.http.post("http://localhost:5222/api/account/login", this.model).subscribe((result) => {
      console.log(result);
    })
  }
}
