import RPi.GPIO as GPIO
import sys
from mfrc522 import SimpleMFRC522
import time

cards = [ 796380597927 ] # ARRAY WITH APPROVED IDS; ID OF OUR APPROVED CARD
reader = SimpleMFRC522()


servo = 11 # CONNECTED ON GPIO17/PIN11 (in our example)

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
        id, text = reader.read()
        print(id)
        if id in cards:
            print("Opening gate oneechan ~ !")
            setAngle(90)
            time.sleep(10)

        setAngle(0)
        time.sleep(0.05)

    GPIO.cleanup()