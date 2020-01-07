import RPi.GPIO as GPIO
import time 

class Servo:
    
    def __init__(self, bcm_pin):
        self.pin = bcm_pin
        GPIO.setup(self.pin, GPIO.OUT)

        self.servo = GPIO.PWM(self.pin, 50)
        self.servo.start(0)

    def setAngle(self, angle):
        value = (angle / 18) + 2.5
        self.servo.ChangeDutyCycle(value)
