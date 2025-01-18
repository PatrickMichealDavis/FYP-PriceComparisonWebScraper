import { Logging } from "./logging.model";
import { Price } from "./price.model";

export class Merchant {

    merchantId: number=0;
    name: string='';
    url: string='';
    contactEmail: string='';
    
    prices: Price[]=[];
    Loggings: Logging[]=[];
}
