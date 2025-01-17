import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class ScraperService {

  private url = 'http://localhost:5002/api/admin';
  private runFullSuiteUrl = '/runFullSuite';
  private runFullSuitePartialUrl = '/runFullSuitePartial';
  

  constructor(private http:HttpClient) { }

  runFullSuite(): void {
    this.http.get(this.url + this.runFullSuite).subscribe();
  }

  runFullSuitePartial(): void {
    this.http.get(this.url + this.runFullSuitePartial).subscribe();
  }

  runScraperByMerchant(merchant: string, partial: boolean): void {
    const url = `${this.url}/runScraperByMerchant/${merchant}?partial=${partial}`;
    this.http.get(url).subscribe({
      next: (response) => console.log('Scraper executed successfully:', response),
      error: (error) => console.error('Error executing scraper:', error)
    });
  }

  testAddProduct(): void {
    this.http.get(this.url + '/testAddProduct').subscribe();
  }
  
}
