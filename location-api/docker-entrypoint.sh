#!/usr/bin/env bash

# Starting proxy for keycloak
socat TCP-LISTEN:8080,fork TCP:keycloak:8080 &

# Starting main application
yarn run start:dev