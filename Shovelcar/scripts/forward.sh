#!/bin/sh
gpio -g write 26 1
gpio -g write 19 1
gpio -g write 13 0

gpio -g write 16 1
gpio -g write 20 1
gpio -g write 21 0

sleep $1

./stop.sh
