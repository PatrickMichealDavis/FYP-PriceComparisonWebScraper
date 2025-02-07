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

  private comparedProducts: Product[] = [];

  getProducts():Observable<Product[]> {
    return this.httpClient.get<Product[]>(this.url + this.getProductsUrl);
  }

  compare(productListIds: number[]): void {
    console.log('Product List Ids:', productListIds); 

    this.httpClient.post<Product[]>(this.url + this.compareUrl, productListIds, {
        headers: { 'Content-Type': 'application/json' }

    }).subscribe({
        next: (response) => {
          this.comparedProducts = response;
            console.log("response:", response);
        },
        error: (error) => {
            console.error("error", error);
        }
    });
}

  
}
