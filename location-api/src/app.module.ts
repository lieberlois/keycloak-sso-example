import { Module } from '@nestjs/common';
import { APP_GUARD } from '@nestjs/core';
import { AuthGuard, KeycloakConnectModule, PolicyEnforcementMode, ResourceGuard, RoleGuard, TokenValidation } from 'nest-keycloak-connect';
import { AppController } from './app.controller';
import { AppService } from './app.service';

@Module({
  imports: [
    KeycloakConnectModule.register({
      authServerUrl: 'http://localhost:8080/auth',
      realm: 'SSO',
      clientId: 'location-api',
      secret: 'VTW2dgMsHW6nOWvf3OQcGhXtZlXWtqpo',   
    })
  ],
  controllers: [AppController],
  providers: [
    {
      provide: APP_GUARD,     
      useClass: AuthGuard,
    },
    {
      provide: APP_GUARD,
      useClass: ResourceGuard,
    },
    {
      provide: APP_GUARD,
      useClass: RoleGuard,
    },
    AppService
  ],
})
export class AppModule {}
