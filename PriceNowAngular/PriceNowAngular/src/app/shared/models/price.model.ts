import { Merchant } from "./merchant.model";
import { Product } from "./product.model";

export class Price {

    priceId: number=0;
    productID: number=0;
    merchantId: number=0;
    priceValue: number=0;
    scrapedAt: Date=new Date();

    product: Product=new Product();
    merchant: Merchant=new Merchant();
}
