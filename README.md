# Construir imagenes con Docker

eval $(minikube docker-env)

docker build -t api-route ./api

docker build -t api2 ./api2

docker build -t qrcode ./qrcode

# Subir imagenes a Minikube

~~minikube image load api-route~~

~~minikube image load api2~~

~~minikube image load qrcode~~

# Instalar con helm

helm install -n api-route --create-namespace api ./k8s

# Desinstalar con helm

helm -n api-route uninstall api

kubectl delete namespace api-route