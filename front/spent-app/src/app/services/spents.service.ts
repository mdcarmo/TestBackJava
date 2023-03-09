import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { Spent } from '../shared/spent';

const baseUrl = 'http://localhost:5226/api/expense-management';

@Injectable({
  providedIn: 'root'
})

export class SpentsService {
  spents: Array<Spent> = [];

  constructor(private http: HttpClient, public router: Router) { }

  getAll(id: any): Observable<Spent[]> {
    return this.http.get<Spent[]>(`${baseUrl}/${id}`);
  }

  get(id: any): Observable<Spent> {
    return this.http.get<Spent>(`${baseUrl}/getById/${id}`);
  }

  update(id: any, data: any): Observable<any> {
    return this.http.put(`${baseUrl}/${id}`, data);
  }

  findByDate(id: any,date: any): Observable<Spent[]> {
    return this.http.get<Spent[]>(`${baseUrl}/filterByDate/${id}?dateFind=${date}`);
  }
}
