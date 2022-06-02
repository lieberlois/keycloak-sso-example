import { Component } from '@angular/core';
import { KeycloakService } from 'keycloak-angular';
import { Observable } from 'rxjs';
import { AppService } from './app.service';
import { Location, Product } from './entities';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {

  public readonly username: string = this.keycloakService.getUsername();

  public locations$: Observable<Array<Location>> | undefined;
  public products$: Observable<Array<Product>> | undefined;

  public constructor(
    private readonly keycloakService: KeycloakService,
    private readonly appService: AppService,
  ) {}

  public fetchLocations(): void {
    this.locations$ = this.appService.fetchLocations();
  }

  public fetchProducts(): void {
    this.products$ = this.appService.fetchProducts();
  }

  public handleLogout(): void {
    this.keycloakService.logout();
  }
}
