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


# font = ImageFont.load_default()
font = ImageFont.truetype('ARIALBD.TTF', 14)
font_small = ImageFont.truetype('ARIALBD.TTF', 9)


def clear():
    draw.rectangle((0, 0, width, height), outline=0, fill=0)

def invalidate():
    disp.image(image)
    disp.display()


if __name__ == "__main__":
    while True:
        clear()

        CPU = subprocess.check_output("top -bn1 | grep load | awk '{printf \"CPU Load: %.2f\", $(NF-2)}'", shell = True )
        MEM = subprocess.check_output("free -m | awk 'NR==2{printf \"Mem: %s/%sMB %.2f%%\", $3,$2,$3*100/$2 }'", shell = True )

        # DRAWING
        draw.text((2, 4), "IOT NAVRAT", font=font, fill=255)
        draw.text((2, 28), CPU, font=font, fill=255)
        draw.text((2, 44), MEM, font=font_small, fill=255)

        invalidate()
        time.sleep(0.1)
