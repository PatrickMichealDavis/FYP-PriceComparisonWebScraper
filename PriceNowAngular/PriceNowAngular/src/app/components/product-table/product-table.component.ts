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
  wishlist:Product[]=[];
  wishlistIds:number[]=[];

  constructor(public productService: ProductService) {

  }

  //change to on refresh patrick when its working
  ngOnInit():void {
    this.productService.getProducts().subscribe((data:Product[]) => {
      this.productList = data;
    });
  }

  addToWishlist(product:Product):void {
    if(!this.wishlist.includes(product)) {
         
    this.wishlist.push(product);
    console.log('Wishlist Updated:', this.wishlist);
    }
  }

  removeFromWishlist(product:Product):void {
    this.wishlist = this.wishlist.filter((item) => item !== product);
    console.log('Wishlist Updated:', this.wishlist);
  }

  compare():void {
    this.wishlistIds = this.wishlist.map((product) => product.productId);
    console.log('Wishlist Ids:', this.wishlistIds);
     this.productService.compare(this.wishlistIds);
  }

  
}
