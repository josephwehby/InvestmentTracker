FROM node:20-alpine AS build 

WORKDIR /app 

COPY package*.json ./

RUN npm install

COPY . .

RUN npm run build

FROM nginx:stable-alpine AS production

RUN apk add --no-cache ca-certificates

# copy react app to nginx
COPY --from=build /app/dist /usr/share/nginx/html 

# copy over tls certs
COPY Certs/key.pem /etc/nginx/ssl/key.pem
COPY Certs/cert.pem /etc/nginx/ssl/cert.pem
COPY Certs/certificate.crt /usr/local/share/ca-certificates/certificate.crt

RUN update-ca-certificates

COPY nginx.conf /etc/nginx/nginx.conf

EXPOSE 443

CMD ["nginx", "-g", "daemon off;"]
