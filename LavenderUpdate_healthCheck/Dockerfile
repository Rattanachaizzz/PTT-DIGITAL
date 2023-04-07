# syntax=docker/dockerfile:1

FROM node:16.13.0

ENV NODE_ENV=production


WORKDIR /app

COPY ["package.json", "package-lock.json*", "./"]

RUN npm install --production

COPY . /app

EXPOSE 3000

CMD [ "node", "server.js" ]