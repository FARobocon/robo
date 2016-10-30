#!/usr/bin/python
import pygame.mixer
import time


pygame.mixer.init()
pygame.mixer.music.load('/home/pi/git/Shovelcar/music/Chestnut_Tree_unpluged_xf.mp3')
pygame.mixer.music.play(1)

time.sleep(16)

pygame.mixer.music.stop()
