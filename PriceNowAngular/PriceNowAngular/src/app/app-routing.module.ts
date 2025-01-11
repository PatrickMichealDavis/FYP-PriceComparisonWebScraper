import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './components/home/home.component';
import { CheckScraperComponent } from './components/check-scraper/check-scraper.component';

const routes: Routes = [
  { path: 'home', component: HomeComponent },
  { path: 'checkScraper', component: CheckScraperComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
