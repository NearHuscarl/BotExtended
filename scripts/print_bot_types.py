import os
from util.print_enum import print_enum

SCRIPT_DIRECTORY = os.path.dirname(os.path.realpath(__file__))
FILE_PATH = os.path.normpath(os.path.join(SCRIPT_DIRECTORY, '../src/BotExtended/BotType.cs'))

def main():
    print_enum(FILE_PATH)

if __name__ == "__main__":
    main()
