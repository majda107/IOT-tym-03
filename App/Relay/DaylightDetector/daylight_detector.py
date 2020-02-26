import sys
sys.path.insert(1, '../NavratClient')

import smbus
import RPi.GPIO as GPIO
from time import sleep
from NavratClient.navrat_client import NavratClient

DEVICE = 0x23
LUX_CAP = 60

GPIO.setwarnings(False)
GPIO.setmode(GPIO.BOARD)
GPIO.setup(36, GPIO.OUT, initial=GPIO.LOW)

client = NavratClient()

bus = smbus.SMBus(1)
pwm = GPIO.PWM(36, 100)
pwm.start(0)

def readLight(addr=DEVICE):
    data = bus.read_i2c_block_data(addr, 0x20) # one time high res mode
    return ((data[1] + (256 * data[0])) / 1.2)

def capValue(value):
    return value / LUX_CAP * 100

if __name__ == "__main__":
    client.start()
    while True:
        value = readLight()
        if(value <= LUX_CAP):
            pwm.ChangeDutyCycle(100 - capValue(value))
            #GPIO.output(36, GPIO.HIGH)
        else:
            pwm.ChangeDutyCycle(0)
            #GPIO.output(36, GPIO.LOW)
            #GPIO.PWM(36, 255)
        # if NAGApi.set_luminosity_value(value):
        #     print('value sent to server!')
        # else:
        #     print('couldn\'t contact server')

        client.send_data("relay-luminosity", value)
        print(str(value))
        sleep(0.2)

    pwm.stop()
