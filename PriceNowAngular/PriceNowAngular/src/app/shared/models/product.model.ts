export class Product {

    productID: number=0;
    name: string='';
    description: string='';
    unit: string='';
    createdAt: Date=new Date();
    updatedAt: Date=new Date();

    prices: Price[]=[];
}
