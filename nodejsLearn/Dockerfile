# build stage
FROM node:lts-alpine as build-stage
WORKDIR /app
COPY package*.json ./
RUN npm ci
COPY . .
# RUN echo "VITE_DOTNET_API=$VITE_DOTNET_API" > .env
# RUN echo "VITE_NODE_API=$VITE_NODE_API" >> .env
EXPOSE ${PORT:-3030}
ENTRYPOINT [ "npm", "run", "start" ]
# runs on port 3030