import { Controller, Get } from '@nestjs/common';
import { Roles } from 'nest-keycloak-connect';

@Controller('location')
export class AppController {

  @Get()
  @Roles({ roles: ['read-location'] })
  getAll(): { cities: Array<string> } {
    return {
      cities: ["Augsburg", "Berlin", "München", "Leipzig", "Heidelberg"]
    }
  }
}
