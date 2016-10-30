#!/bin/sh

# left wheel -> go forward
gpio -g write 26 1 
gpio -g write 19 1
gpio -g write 13 0

# right wheel -> go back
gpio -g write 16 0
gpio -g write 20 1
gpio -g write 21 1

sleep $1

./stop.sh
