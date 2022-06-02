import { Controller, Get } from '@nestjs/common';
import { AppService } from './app.service';

@Controller('location')
export class AppController {
  constructor(private readonly appService: AppService) {}

  @Get()
  getAll(): { cities: Array<string> } {
    return {
      cities: ["Augsburg", "Berlin", "München", "Leipzig", "Heidelberg"]
    }
  }
}
