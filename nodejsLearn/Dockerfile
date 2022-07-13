# build stage
FROM node:lts-alpine as build-stage
WORKDIR /app
COPY package*.json ./
RUN npm ci
COPY . .
EXPOSE ${PORT:-3030}
ENTRYPOINT [ "npm", "run", "start" ]
# runs on port 3030