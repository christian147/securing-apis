FROM node:12.7-alpine AS base

FROM base AS install
WORKDIR /usr/src/app
COPY Client/package.json Client/package-lock.json ./
RUN npm install
COPY Client .

FROM install AS build
WORKDIR /usr/src/app
RUN npm run build:prod

FROM nginx:alpine AS final
ENV NGINX_PORT=4200
EXPOSE 4200
COPY Client/nginx.conf /etc/nginx/nginx.conf
COPY --from=build /usr/src/app/dist/client /usr/share/nginx/html