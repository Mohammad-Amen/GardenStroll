import { HttpClient } from '@angular/common/http';
import { Injectable, OnInit } from '@angular/core';
import { Member } from '../_model/member';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class MembersService implements OnInit {

  baseUrl = environment.apiUrl;
  constructor(private http: HttpClient) { }

  ngOnInit(): void {

  }

  getMembers() {
    return this.http.get<Member[]>(this.baseUrl + 'User/GetAllUsers');
  }
}
