import os
import re
from typing import List
import sys
import glob
from pathlib import Path
import subprocess
import shutil

# run this script to add custom textures which are required in BotExtended script

path = os.path

SCRIPT_DIRECTORY = path.dirname(path.realpath(__file__))
XNB_EXTRACT_PATH = path.normpath(path.join(SCRIPT_DIRECTORY, 'xnbcli'))
TEXTURE_PATH = path.join(XNB_EXTRACT_PATH, 'PACKED')
SFD_PATH = sys.argv[1]

def pack_textures():
    os.chdir(XNB_EXTRACT_PATH)
    p = subprocess.Popen('npm run pack', shell=True, stdout = subprocess.PIPE)
    stdout, stderr = p.communicate()

def update_map_object_files(source, dest):
    print('update ' + source)
    dest_path = path.join(SFD_PATH, dest)

    with open(path.join(SCRIPT_DIRECTORY, source), 'r') as objects_file:
        object_declarations = objects_file.read()

    with open(dest_path, "r") as f:
        lines = f.readlines()

    is_my_section = False
    # delete my section
    with open(dest_path, "w") as f:
        for line in lines:
            if line.startswith('// *** BOT EXTENDED ***'):
                if not is_my_section: is_my_section = True

            if is_my_section: continue
            f.write(line)
                
            if line.startswith('// *** BOT EXTENDED ***'):
                if is_my_section: is_my_section = False

    # re-add my section
    with open(dest_path, "a") as file:
        file.writelines(object_declarations) # append missing data

def main():
    objects_path = path.join(SFD_PATH, 'Content\Data\Images\Objects\BotExtended')
    tiles_path = path.join(SFD_PATH, 'Content\Data\Images\Tiles\BotExtended')
    items_path = path.join(SFD_PATH, 'Content\Data\Items')

    Path(objects_path).mkdir(parents=True, exist_ok=True)
    Path(tiles_path).mkdir(parents=True, exist_ok=True)

    print('copy custom items')
    shutil.copytree(path.join(SCRIPT_DIRECTORY, 'items'), items_path, dirs_exist_ok=True)

    print('packing textures')
    pack_textures()

    print('copy texture files')
    shutil.copytree(path.join(TEXTURE_PATH, 'objects'), objects_path, dirs_exist_ok=True)
    shutil.copytree(path.join(TEXTURE_PATH, 'tiles'), tiles_path, dirs_exist_ok=True)

    update_map_object_files('objects.sfdx', 'Content\Data\Tiles\objects.sfdx')
    update_map_object_files('tiles.sfdx', 'Content\Data\Tiles\\tiles.sfdx')


if __name__ == "__main__":
    main()
