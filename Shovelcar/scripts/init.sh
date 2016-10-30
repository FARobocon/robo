#!/bin/sh

# Initialize shovel up/down GPIO pins
gpio -g mode 3 out
gpio -g mode 4 out

# Initialize left and right wheel GPIO pins
gpio -g mode 13 out
gpio -g mode 19 out
gpio -g mode 26 out

gpio -g mode 16 out
gpio -g mode 20 out
gpio -g mode 21 out
