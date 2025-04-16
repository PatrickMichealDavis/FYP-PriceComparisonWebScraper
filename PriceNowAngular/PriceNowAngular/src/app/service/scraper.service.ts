import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Merchant } from '../shared/models/merchant.model';
import { Logging } from '../shared/models/logging.model';

@Injectable({
  providedIn: 'root'
})
export class ScraperService {

  private url = 'http://localhost:5002/api/admin';
  private runFullSuiteUrl = '/runFullSuite';
  private runFullSuitePartialUrl = '/runFullSuitePartial';
  private runScraperByMerchantUrl = 'runScraperByMerchant';
  private getLogsUrl = '/getAllLogs';
  

  constructor(private http:HttpClient) { }

  runFullSuite(): Observable<any> {
    return this.http.get(this.url + this.runFullSuiteUrl);
  }

  runFullSuitePartial(): Observable<any> {
    return this.http.get(this.url + this.runFullSuitePartialUrl);
  }

  runScraperByMerchant(merchantId: number, partial: boolean): void {
    const url = `${this.url}/${this.runScraperByMerchantUrl}?merchantId=${merchantId}&isPartial=${partial}`;
    this.http.get(url).subscribe({
      next: (response) => console.log('Scraper executed successfully:', response),
      error: (error) => console.error('Error executing scraper:', error)
    });
  }

  testAddProduct(): void {
    this.http.get(this.url + '/testAddProduct').subscribe();
  }

  getAllMerchants(): Observable<Merchant[]> {
      return this.http.get<Merchant[]>(this.url + '/getAllMerchants');
  }

  getAllLogs(): Observable<Logging[]> {
    return this.http.get<Logging[]>(this.url + this.getLogsUrl);
  }

  
  
}
