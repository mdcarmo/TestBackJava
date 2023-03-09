import { Component, Input, OnInit } from '@angular/core';
import { SpentsService } from 'src/app/services/spents.service';
import { Spent } from 'src/app/shared/spent';
import { ActivatedRoute, Router } from '@angular/router';
import { CategoryService } from 'src/app/services/category.service';

@Component({
  selector: 'app-spent-detail',
  templateUrl: './spent-detail.component.html'
})
export class SpentDetailComponent implements OnInit {
  data: any;
  keyword = 'description';
  selectedCategory: any = {};
 
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
    public categoryService: CategoryService,
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
        },
        error: (e) => console.error(e)
      });
  }

  update(): void {
    this.message = '';
    this.currentSpent.category = this.selectedCategory.description;
   
    this.spentService.update(this.currentSpent.id, this.currentSpent)
      .subscribe({
        next: (res) => {
          this.message = 'Gasto alterado com sucesso!';
        },
        error: (e) => console.error(e)
      });
  }

  goBackToPrevPage(): void {
    this.router.navigate(['spents']);
  }
  
  selectEvent(item: any) {
    this.selectedCategory = item
  }

  onChangeSearch(search: string) {
    // fetch remote data from here
    // And reassign the 'data' which is binded to 'data' property.
  }

  getServerResponse(event: any) {
    this.categoryService.getByFilter(event)
      .subscribe({
        next: (data) => {
          this.data = data;
        },
        error: (e) => console.error(e)
      });
  }
  onFocused(e: any) {
    // do something
  }
}