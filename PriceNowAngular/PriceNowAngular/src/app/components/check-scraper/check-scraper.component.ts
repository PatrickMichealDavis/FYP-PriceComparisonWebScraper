import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ScraperService } from '../../service/scraper.service';
import { Merchant } from '../../shared/models/merchant.model';


@Component({
  selector: 'app-check-scraper',
  templateUrl: './check-scraper.component.html',
  styleUrl: './check-scraper.component.css'
})
export class CheckScraperComponent {

  selectedOption: string = '';
  merchantList:Merchant[]=[];

  constructor(public scraperService: ScraperService) { }

  ngOnInit():void {
    this.selectedOption = 'full';
    this.getAllMerchants();
  }

  runFullSuite(): void {
    console.log('Running full suite');
    this.scraperService.runFullSuite();
  }

  testAddProduct(): void {
    console.log('Testing add product');
    this.scraperService.testAddProduct();
  }

  getAllMerchants():void {
    this.scraperService.getAllMerchants().subscribe({
      next:(data)=>{
        this.merchantList = data;
      },error:(err)=>console.error('Error fetching merchants',err)});
  }

 
  
}
