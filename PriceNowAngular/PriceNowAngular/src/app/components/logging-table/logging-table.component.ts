import { Component } from '@angular/core';
import { ScraperService } from '../../service/scraper.service';
import { Logging } from '../../shared/models/logging.model';

@Component({
  selector: 'app-logging-table',
  templateUrl: './logging-table.component.html',
  styleUrl: './logging-table.component.css'
})
export class LoggingTableComponent {

  loggingList: Logging[] = [];

   constructor(public scraperService: ScraperService) { }

  ngOnInit(): void {
    this.scraperService.getAllLogs().subscribe((data: Logging[]) => {
      this.loggingList = data;
    });
  }
}
