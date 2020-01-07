import RPi.GPIO as GPIO
import sys
from mfrc522 import SimpleMFRC522

reader = SimpleMFRC522()

if __name__ == "__main__":
    try:
        id, text = reader.read()
        print(id)
        print(text)

    finally:
        GPIO.cleanup()