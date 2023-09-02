# Construir imagenes con Docker

eval $(minikube docker-env)

docker build -t api-route ./api

docker build -t api2 ./api2

# Subir imagenes a Minikube

~~minikube image load api-route~~

~~minikube image load api2~~

# Instalar con helm

helm install api .

# Desinstalar con helm

helm uninstall api