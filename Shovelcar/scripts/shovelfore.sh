#!/bin/sh

# start shovel motor forward
gpio -g write 3 1
sleep $1
# stop shovel
gpio -g write 3 0
