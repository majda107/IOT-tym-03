import RPi.GPIO as GPIO
import sys
from mfrc522 import SimpleMFRC522
import time

cards = [ 796380597927 ] # ARRAY WITH APPROVED IDS; ID OF OUR APPROVED CARD
reader = SimpleMFRC522()


servo = 12 # CONNECTED ON PIN12 (in our example)
led = 40 # CONNECTED LED ON PIN40 (in our example)

GPIO.setmode(GPIO.BOARD)
GPIO.setup(servo, GPIO.OUT)
GPIO.setup(led, GPIO.OUT)

p = GPIO.PWM(servo, 50)
p.start(0)
GPIO.output(servo, GPIO.HIGH)

def setAngle(angle):
    value = (angle / 18) + 2.5
    p.ChangeDutyCycle(value)


if __name__ == "__main__":
    setAngle(0)
    GPIO.output(led, GPIO.LOW)

    try:
        while True:
            id, text = reader.read()
            print(id)
            if id in cards:
                print("Opening gate oneechan ~ !")
                setAngle(90)
                time.sleep(10)
            else:
                GPIO.output(led, GPIO.HIGH)
                time.sleep(2)
                GPIO.output(led, GPIO.LOW)

            setAngle(0)
            time.sleep(0.05)
    finally:
        GPIO.cleanup()