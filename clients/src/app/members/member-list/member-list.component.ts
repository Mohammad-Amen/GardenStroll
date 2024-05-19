import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { MembersService } from '../../_services/members.service';
import { Member } from '../../_model/member';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-member-list',
  templateUrl: './member-list.component.html',
  styleUrl: './member-list.component.css'
})
export class MemberListComponent implements OnInit{
  members: Member[] | undefined = [];

  constructor(private membersServices: MembersService, private toastr: ToastrService) {}

  ngOnInit(): void {
    this.getMembers();
  }

  getMembers() {
    this.membersServices.getMembers().subscribe(
      result => {
        this.members = result;
      },
      error => {
        this.toastr.error(error.error);
      }
    );
  }
}