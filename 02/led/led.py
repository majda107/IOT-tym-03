import RPi.GPIO as GPIO
from time import sleep

GPIO.setwarnings(False)
GPIO.setmode(GPIO.BOARD)
GPIO.setup(40, GPIO.OUT, initial=GPIO.LOW)
GPIO.setup(38, GPIO.IN, pull_up_down=GPIO.PUD_DOWN)

while True:
    if GPIO.input(38) == GPIO.HIGH:
        print("I was touched onii-chan ~ ")
        GPIO.output(40, GPIO.HIGH)
        sleep(10)
        GPIO.output(40, GPIO.LOW)
        
    sleep(0.01)
