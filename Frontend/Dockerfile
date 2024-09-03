FROM node:20-alpine AS build 

WORKDIR /app 

COPY package*.json ./

RUN npm install

COPY . .

RUN npm run build

FROM nginx:stable-alpine AS production 

# copy react app to nginx
COPY --from=build /app/dist /usr/share/nginx/html 

# copy over tls certs
COPY Certs/key.pem /etc/nginx/ssl/key.pem
COPY Certs/cert.pem /etc/nginx/ssl/cert.pem


COPY nginx.conf /etc/nginx/nginx.conf

EXPOSE 443

CMD ["nginx", "-g", "daemon off;"]
