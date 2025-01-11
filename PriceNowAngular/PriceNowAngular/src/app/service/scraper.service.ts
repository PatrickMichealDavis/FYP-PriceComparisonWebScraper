import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class ScraperService {

  private url = 'http://localhost:8080/api/admin';

  constructor(private http:HttpClient) { }
}
