FROM node:20-alpine AS build 

WORKDIR /app 

COPY package*.json ./

RUN npm install

COPY . .

RUN npm run build

FROM nginx:stable-alpine AS production 

# copy over tls certs
COPY ../Certificates/React/key.pem /etc/ssl/private/key.pem
COPY ../Certificates/React/cert.pem /etc/ssl/certs/cert.pem

COPY --from=build /app/build /user/share/nginx/html 

COPY frontend/nginx.conf /etc/nginx/nginx.conf

EXPOSE 443

CMD ["nginx", "-g", "daemon off;"]