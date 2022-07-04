# build stage
FROM node:lts-alpine as build-stage
WORKDIR /app
COPY package*.json ./
RUN npm install
COPY . .
# RUN echo "VITE_DOTNET_API=$VITE_DOTNET_API" > .env
# RUN echo "VITE_NODE_API=$VITE_NODE_API" >> .env
RUN npm run build

# production stage
FROM nginx:stable-alpine as production-stage
COPY --from=build-stage /app/dist /usr/share/nginx/html
EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]