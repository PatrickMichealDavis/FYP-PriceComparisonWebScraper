import { Merchant } from "./merchant.model";

export class Logging {

    scrapeId: number=0;
    merchantId: number=0;
    scrapedAt: Date=new Date();
    status: string='';
    errorMessage: string='';

    merchant: Merchant=new Merchant();
}
