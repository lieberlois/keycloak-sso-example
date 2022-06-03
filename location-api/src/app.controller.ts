import { Body, Controller, Delete, Get, Param, Post } from '@nestjs/common';
import { Scopes, Resource } from 'nest-keycloak-connect';
import { AppService } from './app.service';

@Controller('location')
@Resource('Location')
export class AppController {

  public constructor(
    private readonly appService: AppService,
  ) {}

  @Get()
  @Scopes("view")
  public getAll(): { cities: Array<string> } {
    return {
      cities: this.appService.getCities(),
    }
  }

  @Post()
  @Scopes("create")
  public createCity(@Body() data: { name: string }): void {
    return this.appService.createCity(data.name);
  }

  @Delete(":name")
  @Scopes("delete")
  public deleteCity(@Param('name') name: string): void {
    console.log("got name", name)
    return this.appService.deleteCity(name);
  }
}
