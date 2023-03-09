import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Category } from '../shared/category';

const baseUrl = 'http://localhost:5226/api/category';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {
  spents: Array<Category> = [];
 
  constructor(private http: HttpClient) { }

  getByFilter(search: any): Observable<Category[]> {
    return this.http.get<Category[]>(`${baseUrl}/getbyfilter/${search}`);
  }
}
