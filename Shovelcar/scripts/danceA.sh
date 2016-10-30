#!/bin/sh
SCRIPTS_DIR=/home/pi/git/Shovelcar/scripts

cd $SCRIPTS_DIR

./init.sh

./play.py &

sleep 1

./forward.sh 1
./back.sh 1
./forward.sh 1
./back.sh 1
./left.sh 1
./right.sh 1
./left.sh 1
./right.sh 1
./shovelfore.sh 2
./shovelback.sh 2

sleep 2
./play.py &
sleep 1

./forward.sh 1
./back.sh 1
./forward.sh 1
./back.sh 1
./left.sh 1
./right.sh 1
./left.sh 1
./right.sh 1
./shovelfore.sh 2
./shovelback.sh 2
