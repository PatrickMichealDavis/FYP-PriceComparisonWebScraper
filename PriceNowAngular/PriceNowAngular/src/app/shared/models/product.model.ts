import { Price } from "./price.model";

export class Product {

    productId: number=0;
    name: string='';
    description: string='';
    category: string='';
    unit: string='';
    createdAt: Date=new Date();
    updatedAt: Date=new Date();

    prices: Price[]=[];
}
