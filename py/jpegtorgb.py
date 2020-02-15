import sys
from pathlib import Path
from PIL import Image

if len(sys.argv) != 2:
    print("ERROR! Add image path as (only) parameter.")
    sys.exit(0)

js = True
requiredSize = (16,16)

elementStart = "{" if not js else "["
elementEnd = "}" if not js else "]"
elementSeperator = ","

imagePath = Path(sys.argv[1])
if not imagePath.is_file():
    print("ERROR! path provided is not a file.")
    sys.exit(0)


im = Image.open(imagePath)
if requiredSize != im.size:
    im = im.resize(requiredSize, Image.ANTIALIAS)
pix = im.load()

rowPixels = []
for x in range(im.size[0]):
    columnPixels = []
    for y in range(im.size[1]):
        pixel = pix[x, y]
        pixelString = elementStart + elementSeperator.join(map(lambda p: "{0:#0{1}x}".format(p,4), pixel)) + elementEnd
        columnPixels.append(pixelString)
    rowPixels.append(elementStart + elementSeperator.join(columnPixels) + elementEnd)

print( elementStart + "\n  " + (elementSeperator + "\n  ").join(rowPixels) + "\n" + elementEnd)

