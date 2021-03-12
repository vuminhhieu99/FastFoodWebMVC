from PIL import Image
import os

for current_directory, sub_directory_names, file_names in os.walk("."):
    image_path = Image.open(r's10.png')
    image = Image.open(image_path)
    for name in file_names:
        if name.endswith(".jpg"):
            image_path = os.path.join(current_directory, name)
            image = Image.open(image_path)
            # image_out = image.transpose(Image.ROTATE_90)
            image_out = image.rotate(90)
            image_out.save(image_path)

