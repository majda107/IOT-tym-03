import RPi.GPIO as GPIO
import time

sensor = 38

led = 40
buzzer = 41

GPIO.setwarnings(False)
GPIO.setmode(GPIO.BOARD)
GPIO.setup(led, GPIO.OUT, initial=GPIO.LOW)
GPIO.setup(buzzer, GPIO.OUT, initial=GPIO.LOW)
GPIO.setup(sensor, GPIO.IN, pull_up_down=GPIO.PUD_DOWN)

if __name__ == "__main__":
    while True:
        if GPIO.input(sensor) == GPIO.HIGH:
            print("Intruder oneechan ~!")
            GPIO.output(led, GPIO.HIGH)
            GPIO.output(buzzer, GPIO.HIGH)
            time.sleep(0.5) # TO PREVENT FROM BUZZER BLOWOUT
        else:
            GPIO.output(led, GPIO.LOW)
            GPIO.output(buzzer, GPIO.LOW)

        time.sleep(0.01)
