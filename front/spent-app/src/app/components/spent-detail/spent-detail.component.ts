import { Component, Input, OnInit } from '@angular/core';
import { SpentsService } from 'src/app/services/spents.service';
import { Spent } from 'src/app/shared/spent';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-spent-detail',
  templateUrl: './spent-detail.component.html'
})
export class SpentDetailComponent implements OnInit {

  @Input() viewMode = false;

  @Input() currentSpent: Spent = {
    codeUser: '',
    description: '',
    value: '',
    postedAt: '',
    category: '',
    id: ''
  };

  message = '';

  constructor(
    public spentService: SpentsService,
    private route: ActivatedRoute,
    private router: Router
  ) {}

  ngOnInit(): void {
    if (!this.viewMode) {
      this.message = '';
      this.getTutorial(this.route.snapshot.params["id"]);
    }
  }

  getTutorial(id: string): void {
    this.spentService.get(id)
      .subscribe({
        next: (data) => {
          this.currentSpent = data;
          console.log(data);
        },
        error: (e) => console.error(e)
      });
  }

  update(): void {
    this.message = '';

    this.spentService.update(this.currentSpent.id, this.currentSpent)
      .subscribe({
        next: (res) => {
          //console.log(res);
          this.message = 'Gasto alterado com sucesso!';
        },
        error: (e) => console.error(e)
      });
  }

  goBackToPrevPage(): void {
    this.router.navigate(['spents']);
  }
}
