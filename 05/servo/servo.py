import RPi.GPIO as GPIO
import time

pin = 11
button = 38

GPIO.setmode(GPIO.BOARD)
GPIO.setup(pin, GPIO.OUT)
GPIO.setup(button, GPIO.IN, pull_up_down=GPIO.PUD_DOWN)

p = GPIO.PWM(pin, 50)
p.start(0)

GPIO.output(pin, GPIO.HIGH)


def setAngle(angle):
    value = (angle / 18) + 2.5
    p.ChangeDutyCycle(value)

while True:
    if GPIO.input(button) == GPIO.HIGH:
        print("I was touched onee-chan ~")
        setAngle(90)
        time.sleep(10)
    setAngle(0)
    time.sleep(0.05)
