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
  private compareUrl = '/compare';
  private priceNowUrl = '/priceNow';

  private comparedProducts: Product[] = [];

  getProducts():Observable<Product[]> {
    return this.httpClient.get<Product[]>(this.url + this.getProductsUrl);
  }

  compare(productListIds: number[]): Observable<Product[]> {
    console.log('Product List Ids:', productListIds); 

    return this.httpClient.post<Product[]>(this.url + this.compareUrl, productListIds, {
        headers: { 'Content-Type': 'application/json' }
    });
}

priceNow(Product : Product) : Observable<Product> {
    console.log('Product:', Product); 

    return this.httpClient.post<Product>(this.url + this.priceNowUrl, Product, {
      headers: { 'Content-Type': 'application/json' }
  });
  
}
}
