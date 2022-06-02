FROM node:14-alpine

ENV NODE_ENV development

WORKDIR /app
COPY package.json package.json
COPY package-lock.json package-lock.json

RUN export PATH="$HOME/.npm-global/bin:$PATH"
RUN export PATH="$HOME/.npm-packages/bin:$PATH"

RUN npm ci
RUN npm install --dev
RUN npm audit fix

EXPOSE 4200
CMD npm start