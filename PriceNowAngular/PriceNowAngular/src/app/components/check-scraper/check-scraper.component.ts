import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';


@Component({
  selector: 'app-check-scraper',
  templateUrl: './check-scraper.component.html',
  styleUrl: './check-scraper.component.css'
})
export class CheckScraperComponent {

  selectedOption: string = '';

  constructor() { }

  ngOnInit():void {
    this.selectedOption = 'full';
  }
}
