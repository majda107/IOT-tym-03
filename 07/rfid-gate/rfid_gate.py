import RPi.GPIO as GPIO
import sys
from mfrc522 import SimpleMFRC522
import time

cards = [] # ARRAY WITH APPROVED IDS
reader = SimpleMFRC522()


servo = 11

GPIO.setmode(GPIO.BOARD)
GPIO.setup(servo, GPIO.OUT)

p = GPIO.PWM(servo, 50)
p.start(0)
GPIO.output(servo, GPIO.HIGH)

def setAngle(angle):
    value = (angle / 18) + 2.5
    p.ChangeDutyCycle(value)


if __name__ == "__main__":
    while True:
        try:
            id, text = reader.read()
            print(id)
            if id in cards:
                print("Opening gate!")
                setAngle(90)
                time.sleep(10)

        finally:
            GPIO.cleanup()

        setAngle(0)
        time.sleep(0.05)