import os
import re
from typing import List
import sys
import shutil
import glob
from pathlib import Path
import subprocess

# run this script to add custom textures which are required in BotExtended script

path = os.path

SCRIPT_DIRECTORY = path.dirname(path.realpath(__file__))
XNB_EXTRACT_PATH = path.normpath(path.join(SCRIPT_DIRECTORY, 'XNBExtract'))
TEXTURE_PATH = path.join(XNB_EXTRACT_PATH, 'PACKED')
SFD_PATH = sys.argv[1]

def pack_textures():
    print('packing textures')
    os.chdir(XNB_EXTRACT_PATH)
    p = subprocess.Popen(path.join(XNB_EXTRACT_PATH, 'PackFiles.bat'), shell=True, stdout = subprocess.PIPE)
    stdout, stderr = p.communicate()

def update_object_declarations():
    print('update object declarations')
    objects_path = path.join(SFD_PATH, 'Content\Data\Tiles\objects.sfdx')

    with open(path.join(SCRIPT_DIRECTORY, 'objects.sfdx'), 'r') as objects_file:
        object_declarations = objects_file.read()

    with open(objects_path, "r") as f:
        lines = f.readlines()

    is_my_section = False
    # delete my section
    with open(objects_path, "w") as f:
        for line in lines:
            if line.startswith('// *** BOT EXTENDED ***'):
                if not is_my_section: is_my_section = True

            if is_my_section: continue
            f.write(line)
                
            if line.startswith('// *** BOT EXTENDED ***'):
                if is_my_section: is_my_section = False

    # re-add my section
    with open(objects_path, "a") as file:
        file.writelines(object_declarations) # append missing data

def main():
    texture_path = path.join(SFD_PATH, 'Content\Data\Images\Objects\BotExtended')
    Path(texture_path).mkdir(parents=True, exist_ok=True)

    pack_textures()

    print('copy texture files')
    texture_files = glob.iglob(path.join(TEXTURE_PATH, "*.xnb"))

    for file in texture_files:
        if path.isfile(file):
            shutil.copy2(file, texture_path)

    update_object_declarations()


if __name__ == "__main__":
    main()
