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
  selectedMerchant?: Merchant;
  merchantList:Merchant[]=[];

  constructor(public scraperService: ScraperService) { }

  ngOnInit():void {
    this.selectedOption = 'full';
    this.getAllMerchants();
  }

  selectMerchant(merchant: Merchant) {
    this.selectedMerchant = merchant;
  }

  runFullSuite(): void {
    console.log('Running full suite');
    this.scraperService.runFullSuite();
  }

  runFullSuitePartial():void{
    console.log('Running full suite partial');
    this.scraperService.runFullSuitePartial();
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

  runScraperByMerchant():void {
    if (!this.selectedMerchant) {
      alert("Please select a merchant");
      return;
    }

    console.log("Raw selectedOption value:", this.selectedOption);

    const isPartial = this.selectedOption?.trim().toLowerCase() === "partial" ? true : false;

    console.log("After processing, isPartial:", isPartial);
    console.log("Scraping by merchant")
    this.scraperService.runScraperByMerchant(this.selectedMerchant.merchantId,isPartial);
  }

 
  
}
