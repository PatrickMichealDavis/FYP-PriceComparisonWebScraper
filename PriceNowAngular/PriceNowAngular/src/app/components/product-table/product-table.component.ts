import { Component } from '@angular/core';
import { ProductService } from '../../service/product.service';
import { Product } from '../../shared/models/product.model';
declare var $: any;


@Component({
  selector: 'app-product-table',
  templateUrl: './product-table.component.html',
  styleUrl: './product-table.component.css'
})
export class ProductTableComponent {

  productList:Product[]=[];
  wishlist:Product[]=[];
  wishlistIds:number[]=[];
  categories: string[] = [];
  selectedCategory: string = '';
  dataTable: any;
  filteredProducts: Product[] = [];

  constructor(public productService: ProductService) {

  }

  //change to on refresh patrick when its working
  ngOnInit():void {
    this.productService.getProducts().subscribe((data:Product[]) => {
      this.productList = data;
      this.filteredProducts = data;
      this.categories = [...new Set(data.map(product => product.category))];
      this.initializeDataTable();
    });
  }

  filterProducts(): void {
    if (this.dataTable) {
      this.dataTable.destroy(); 
    }

    if (this.selectedCategory) {
      this.filteredProducts = this.productList.filter(product => product.category === this.selectedCategory);
    } else {
      this.filteredProducts = [...this.productList]; 
    }

    setTimeout(() => {
      this.initializeDataTable(); 
    }, 100);
  }

  ngOnDestroy(): void {
    if (this.dataTable) {
      this.dataTable.destroy(); 
    }
  }

  private initializeDataTable(): void {
    setTimeout(() => {
      this.dataTable = ($('#prodTable') as any).DataTable();
    }, 0);
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
