import os
import re
from typing import List

SCRIPT_DIRECTORY = os.path.dirname(os.path.realpath(__file__))
FILE_PATH = os.path.normpath(
    os.path.join(SCRIPT_DIRECTORY, '../src/BotExtended/BotType.cs'))


def main():
    botTypeIndex = 0
    enumRe = re.compile(r"^\s*([a-zA-Z_][a-zA-Z0-9_]+).*,.*$")
    botTypes: List[str] = []

    with open(FILE_PATH, 'r') as file:
        for line in file:
            match = enumRe.match(line)
            if match:
                botGroupName = match.group(1)
                botTypes.append(str(botTypeIndex) + ': ' + botGroupName)
                botTypeIndex += 1

    for t in botTypes:
        print(t)


if __name__ == "__main__":
    main()
