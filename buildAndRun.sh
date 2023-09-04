#!/bin/bash

# Reset
Color_Off='\033[0m'       # Text Reset

# Regular Colors
Black='\033[0;30m'        # Black
Red='\033[0;31m'          # Red
Green='\033[0;32m'        # Green
Yellow='\033[0;33m'       # Yellow
Blue='\033[0;34m'         # Blue
Purple='\033[0;35m'       # Purple
Cyan='\033[0;36m'         # Cyan
White='\033[0;37m'        # White


# clear screen and save scrollback
function clear() {
    printf "\\033[H\\033[22J"
}

function showSpinner() {
    PID=$!
    i=1
    #sp="⣾⣽⣻⢿⡿⣟⣯⣷"
    sp="🕛🕑🕒🕓🕕🕖🕘🕙"
    echo -n ' '
    while [ -d /proc/$PID ]
    do
        sleep 0.05
        printf "\r${sp:i++%${#sp}:1}  $1..."
    done
    printf "\r                                    \r"
}

function cleanup() {
    tput cnorm
}

trap cleanup EXIT

tput reset

sleep 0.05

tput civis

echo -e "$Blue🏃  Iniciando tarea$Color_Off"

eval $(minikube docker-env)

echo -e "$Blue📌  Construyendo imágenes...$Color_Off"

docker build -t api-route ./api >/dev/null 2>&1 & showSpinner "construyendo api-route"

echo -e "$Blue✅  api-route creada$Color_Off"

docker build -t api2 ./api2 >/dev/null 2>&1 & showSpinner "construyendo api2"

echo -e "$Blue✅  api2 creada$Color_Off"

docker build -t qrcode ./qrcode >/dev/null 2>&1 & showSpinner "construyendo qrcode"

echo -e "$Blue✅  qrcode creada$Color_Off"

helm -n api-route uninstall api >/dev/null 2>&1 & showSpinner "Desinstalando chart"

echo -e "$Blue✅  Chart desinstalado$Color_Off" 

helm install -n api-route --create-namespace api ./k8s >/dev/null 2>&1 & showSpinner "Instalando chart"

echo -e "$Blue✅  Chart instalado$Color_Off"

echo -e "$Blue✅  api-route instalado$Color_Off"

echo -e "$Blue🚇  Iniciando minikube tunnel$Color_Off"

minikube tunnel

