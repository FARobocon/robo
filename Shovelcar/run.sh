#!/bin/sh
SHOVELCAR_HOME=/home/pi/git/Shovelcar
cd $SHOVELCAR_HOME
./scripts/bluetooth_serial_service &
./dance &
