import RPi.GPIO as GPIO
import time
from servo import Servo
from datetime import datetime

lastTimeChecked = datetime.now()
shouldCloseSensor = True
shouldCloseButton = True

GPIO.setmode(GPIO.BOARD)

servo = Servo(11)
sensor = 40
button = 38
delay = 3

GPIO.setup(button, GPIO.IN, pull_up_down=GPIO.PUD_DOWN)
GPIO.setup(sensor, GPIO.IN)

def button_both(channel):
    global lastTimeChecked
    global shouldCloseButton

    if GPIO.input(button):
        servo.setAngle(90)
        shouldCloseButton = False
    else:
        shouldCloseButton = True
    lastTimeChecked = datetime.now()

def sensor_rising(channel):
    global lastTimeChecked
    global shouldCloseSensor

    if GPIO.input(sensor):
        servo.setAngle(90)
        shouldCloseSensor = False
    else:
        shouldCloseSensor = True
    lastTimeChecked = datetime.now()

GPIO.add_event_detect(button, GPIO.BOTH)
GPIO.add_event_callback(button, button_both)

GPIO.add_event_detect(sensor, GPIO.RISING)
GPIO.add_event_callback(sensor, sensor_rising)

while True:
    global lastTimeChecked
    if shouldCloseSensor == True and shouldCloseButton and (datetime.now() - lastTimeChecked).seconds > 2:
        servo.setAngle(0)

    time.sleep(0.1)

