import { Controller, Get } from '@nestjs/common';
import { AppService } from './app.service';

@Controller('location')
export class AppController {
  constructor(private readonly appService: AppService) {}

  @Get()
  getAll(): Array<string> {
    return ["Augsburg", "Berlin", "München", "Leipzig", "Heidelberg"]
  }
}
