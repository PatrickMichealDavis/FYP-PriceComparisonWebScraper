import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Product } from '../shared/models/product.model';

@Injectable({
  providedIn: 'root'
})
export class ProductService {

  constructor(private httpClient:HttpClient) { }

  private url = 'http://localhost:5002/api/admin';
  private getProductsUrl = '/getProducts';

  getProducts():Observable<Product[]> {
    return this.httpClient.get<Product[]>(this.url + this.getProductsUrl);
  }
}
