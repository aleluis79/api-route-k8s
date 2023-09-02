## Lector de QR Code escrito en Go

Fuente: https://blog.devgenius.io/a-simple-qr-code-reader-service-in-golang-15483fbe55e4

### Probar usando Docker

docker build -t qrcode .

docker run -d --rm --name qrcode -p 8888:8888 qrcode

curl -X POST --data-binary @"example.png" localhost:8888/scan

### Probar localmente

RUN go mod download

go run .

curl -X POST --data-binary @"example.png" localhost:8888/scan