import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { AccountService } from '../_services/account.service';
import { User } from '../_model/user';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrl: './nav.component.css'
})
export class NavComponent implements OnInit {

  model: any = {
    username: '',
    password: ''
  };
  loggedIn = false;

  constructor(private http: HttpClient, private accountService: AccountService, private router: Router,
    private toastr: ToastrService
  ) {}

  ngOnInit(): void {
    this.getCurrentUser();
  }

  getCurrentUser() {
    this.accountService.currentUser$.subscribe(user => {
      this.loggedIn = !!user
    }, error => {
      console.log(error)
    });
  }

  login(): void {
    this.accountService.login(this.model).subscribe(
      (result: any) => {
        this.router.navigateByUrl('/members');
      },
      error => {
        this.toastr.error(error.error)
      }
    );
  }
  

  logout() {
    this.loggedIn = false;
    this.model = {
      username: '',
      token: ''
    };
    this.accountService.logout();
    this.router.navigateByUrl('/');
  }
}
