FROM node:16

WORKDIR /app

COPY package.json package.json
COPY yarn.lock yarn.lock

RUN yarn 
RUN apt-get update -y && apt-get install socat -y

COPY docker-entrypoint.sh docker-entrypoint.sh 

CMD [ "./docker-entrypoint.sh" ]