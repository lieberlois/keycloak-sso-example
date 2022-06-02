import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { map, Observable } from "rxjs";
import { LocationListDto, ProductListDto } from "./dtos";
import { Location, Product } from "./entities";

@Injectable({
    providedIn: 'root',
})
export class AppService {
    public constructor (
      private httpClient: HttpClient
    ) { }
  
    public fetchLocations(): Observable<Array<Location>> {
        return this.httpClient
            .get<LocationListDto>('http://localhost:3000/api/location')
            .pipe(
                map((locListDto: LocationListDto) => locListDto.cities)
            );
    }
  
    public fetchProducts(): Observable<Array<Product>> {
        return this.httpClient
            .get<ProductListDto>('http://localhost:5000/api/product')
            .pipe(
                map((productListDto: ProductListDto) => productListDto.products)
            );
    }
  }