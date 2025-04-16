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
  isScraping: boolean = false;

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
    this.isScraping = true;  
    this.scraperService.runFullSuite().subscribe({
      next: (data) => {
        console.log('Full suite success:', data);
        this.isScraping = false; 
      },
      error: (err) => {
        console.error('Full suite error:', err);
        this.isScraping = false; 
      }
    });
  }

  runFullSuitePartial():void{
    console.log('Running full suite partial');
    this.isScraping = true; 
    this.scraperService.runFullSuitePartial().subscribe({
      next: (data) => {
        console.log('Full suite partial success:', data);
        this.isScraping = false; 
      },
      error: (err) => {
        console.error('Full suite partial error:', err);
        this.isScraping = false; 
      }
    });
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

    this.isScraping = true;

    this.scraperService.runScraperByMerchant(this.selectedMerchant.merchantId, isPartial)
      .subscribe({
        next: (response) => {
          console.log("Scraper executed successfully:", response);
          this.isScraping = false;
        },
        error: (error) => {
          console.error("Error executing scraper:", error);
          this.isScraping = false;
        }
      });
  }

 
  
}
