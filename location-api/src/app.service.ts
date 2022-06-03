import { Injectable } from '@nestjs/common';

@Injectable()
export class AppService {
    private cities: Array<string> = ["Augsburg", "Berlin", "München", "Leipzig", "Heidelberg"];

    public getCities(): Array<string> {
        return this.cities;
    }

    public createCity(name: string): void {
        this.cities = [...this.cities, name];
    }

    public deleteCity(name: string): void {
        this.cities = this.cities.filter((city: string): boolean => city !== name);
    }
}
