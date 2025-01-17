import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ScraperService } from '../../service/scraper.service';


@Component({
  selector: 'app-check-scraper',
  templateUrl: './check-scraper.component.html',
  styleUrl: './check-scraper.component.css'
})
export class CheckScraperComponent {

  selectedOption: string = '';

  constructor(public scraperService: ScraperService) { }

  ngOnInit():void {
    this.selectedOption = 'full';
  }

  runFullSuite(): void {
    console.log('Running full suite');
    this.scraperService.runFullSuite();
  }

  testAddProduct(): void {
    console.log('Testing add product');
    this.scraperService.testAddProduct();
  }
  
}
