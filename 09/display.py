import Adafruit_SSD1306
from PIL import Image
from PIL import ImageDraw
from PIL import ImageFont

import subprocess
import time

disp = Adafruit_SSD1306.SSD1306_128_64(rst=None)
disp.begin()

disp.clear()
disp.display()

width = disp.width
height = disp.height

image = Image.new('1', (width, height))
draw = ImageDraw.Draw(image)


font = ImageFont.load_default()


def clear():
    draw.rectangle((0, 0, width, height), outline=0, fill=0)

def invalidate():
    disp.image(image)
    disp.display()


if __name__ == "__main__":
    while True:
        clear()

        # DRAWING
        draw.text((2, 2), "IOT Navrat", font=font, fill=255)

        invalidate()
        time.sleep(0.1)
