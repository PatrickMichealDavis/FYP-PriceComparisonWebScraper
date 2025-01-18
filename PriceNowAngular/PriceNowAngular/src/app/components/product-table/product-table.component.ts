import { Component } from '@angular/core';
import { ProductService } from '../../service/product.service';
import { Product } from '../../shared/models/product.model';

@Component({
  selector: 'app-product-table',
  templateUrl: './product-table.component.html',
  styleUrl: './product-table.component.css'
})
export class ProductTableComponent {

  productList:Product[]=[];

  constructor(public productService: ProductService) {

  }

  //change to on refresh patrick when its working
  ngOnInit():void {
    this.productService.getProducts().subscribe((data:Product[]) => {
      this.productList = data;
    });
  }

}
