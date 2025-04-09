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
  comparedList: Product[] = [];

  

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
    alert("Item added to wishlist");
    }
  }

  removeFromWishlist(product:Product):void {
    this.wishlist = this.wishlist.filter((item) => item !== product);
    console.log('Wishlist Updated:', this.wishlist);
  }

  compare():void {
    this.wishlistIds = this.wishlist.map((product) => product.productId);
    console.log('Wishlist Ids:', this.wishlistIds);
     this.productService.compare(this.wishlistIds).subscribe({
      next: (data) => {
        this.comparedList = data;
        this.comparedList = this.comparedList.map(product => ({
          ...product,
          prices: product.prices.sort((a, b) => a.priceValue - b.priceValue)
      }));
      },
      error: (err) => console.error('Error fetching products', err)
    });


  }

  getDays(date: Date): string {

    const now = new Date();
    const scrapedDate = new Date(date);
  
    const diffInMs = now.getTime() - scrapedDate.getTime();
    const diffInHours = Math.floor(diffInMs / (1000 * 60 * 60));
    const diffInDays = Math.floor(diffInHours / 24);
  
    if (diffInDays === 0)
    {
      if (diffInHours === 0)
        {
          return 'Just now'
        };
      if (diffInHours === 1)
        {
          return '1 hour ago'
        };

      return `${diffInHours} hours ago`;
    }
  
    if (diffInDays === 1)
      {
        return '1 day ago'
      };

    return `${diffInDays} days ago`;
  }
  
  
}
