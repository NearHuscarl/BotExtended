import os
import re
from typing import List
import sys
import shutil
from pathlib import Path

# run this script to add custom textures which are required in BotExtended script

path = os.path

SCRIPT_DIRECTORY = path.dirname(path.realpath(__file__))
TEXTURE_PATH = path.normpath(path.join(SCRIPT_DIRECTORY, '../textures'))

def main():
    sfd_path = sys.argv[1]
    texture_path = path.join(sfd_path, 'Content\Data\Images\Objects\BotExtended')
    objects_path = path.join(sfd_path, 'Content\Data\Tiles\objects.sfdx')
    Path(texture_path).mkdir(parents=True, exist_ok=True)

    shutil.copy2(path.join(TEXTURE_PATH, 'Target00.xnb'), texture_path)

    target_declaration = 'tile(Target00) { type=Static; mainLayer=1; drawCategory=OBJ; colorPalette=Neon; fixture(){ collisionGroup=none; } }'
    with open(objects_path, "r+") as file:
        for line in file:
            if target_declaration in line:
                break
        else: # not found, we are at the eof
            file.writelines('\n' + target_declaration) # append missing data


if __name__ == "__main__":
    main()
