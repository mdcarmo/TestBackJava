import { DatePipe } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { SpentsService } from 'src/app/services/spents.service';
import { AuthService } from 'src/app/shared/auth.service';
import { Spent } from 'src/app/shared/spent';

@Component({
  selector: 'app-spents',
  templateUrl: './spents.component.html'
})
export class SpentsComponent implements OnInit {
  currentUser: any = {};
  spents?: Spent[];
  currentSpent: any = {};
  currentIndex = -1;
  dateSearch = '';
  
  constructor(
    public authService: AuthService,
    public spentService: SpentsService,
    private datePipe: DatePipe
  ) {}

  ngOnInit() {
    this.currentUser = this.authService.getUser();
    this.retrieveSpents();    
  }
  
  retrieveSpents(): void {
    this.spentService.getAll(this.currentUser.id)
      .subscribe({
        next: (data) => {
          this.spents = data;
        },
        error: (e) => console.error(e)
      });
  }

  setActivSpent(spent: Spent, index: number): void {
    this.currentSpent = spent;
    this.currentIndex = index;
  }

  searchDate(): void {
    this.currentSpent = {};
    this.currentIndex = -1;

    //new Date(this.dateSearch).toISOString();
    var ddMMyyyy = this.datePipe.transform(this.dateSearch,"dd-MM-yyyy");

    this.spentService.findByDate(this.currentUser.id,ddMMyyyy)
      .subscribe({
        next: (data) => {
          this.spents = data;
        },
        error: (e) => console.error(e)
      });
  }
}
