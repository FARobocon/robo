#!/bin/sh

# start shovel motor backward
gpio -g write 4 1
sleep $1
# stop shovel
gpio -g write 4 0
