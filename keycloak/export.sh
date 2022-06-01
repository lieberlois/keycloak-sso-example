docker-compose exec keycloak /opt/jboss/keycloak/bin/standalone.sh \
-Djboss.socket.binding.port-offset=100 \
-Dkeycloak.migration.provider=singleFile \
-Dkeycloak.migration.realmName=SSO \
-Dkeycloak.migration.usersExportStrategy=REALM_FILE \
-Dkeycloak.migration.action=export \
-Dkeycloak.migration.file=/opt/jboss/keycloak/imports/realm.json