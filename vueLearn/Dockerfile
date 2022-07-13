# build stage
FROM node:lts-alpine as build-stage
WORKDIR /app
COPY package*.json ./
RUN npm install
COPY . .
ARG VITE=DOTNET_API=https://dotnet-api-a2aofhar4a-ew.a.run.app/,NODE_API=https://nodejs-api-a2aofhar4a-ew.a.run.app/
RUN VITE_ARGS=$VITE npm run build-only

# production stage
FROM nginx:stable-alpine as production-stage
COPY --from=build-stage /app/dist /usr/share/nginx/html
EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]