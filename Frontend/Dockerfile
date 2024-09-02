FROM node:20-alpine AS build 

WORKDIR /app 

COPY package*.json ./

RUN npm install

COPY . .

RUN npm run build

FROM nginx:stable-alpine AS production 

# copy over tls certs
COPY Certs/key.pem /etc/ssl/private/key.pem
COPY Certs/cert.pem /etc/ssl/certs/cert.pem

COPY --from=build /app/dist /usr/share/nginx/html 

COPY nginx.conf /etc/nginx/nginx.conf

EXPOSE 443

CMD ["nginx", "-g", "daemon off;"]
