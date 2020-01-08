import RPi.GPIO as GPIO
import time

sensor = 10 # OUR PIR PIN

led = 40 # OUR LED PIN
buzzer = 16 # OUR BUZZER PIN

GPIO.setwarnings(False)
GPIO.setmode(GPIO.BOARD)
GPIO.setup(led, GPIO.OUT, initial=GPIO.LOW)
GPIO.setup(buzzer, GPIO.OUT, initial=GPIO.LOW)
# GPIO.setup(sensor, GPIO.IN, pull_up_down=GPIO.PUD_DOWN)
GPIO.setup(sensor, GPIO.IN)

if __name__ == "__main__":
    while True:
        if GPIO.input(sensor) != GPIO.HIGH:
        # if not True:
            print("Intruder oneechan ~!")
            GPIO.output(led, GPIO.HIGH)
            GPIO.output(buzzer, GPIO.HIGH)
            time.sleep(0.5)  # TO PREVENT FROM BUZZER BLOWOUT
        else:
            print("I am home alone oneechan ~!")
            GPIO.output(led, GPIO.LOW)
            GPIO.output(buzzer, GPIO.LOW)

        time.sleep(0.01)
