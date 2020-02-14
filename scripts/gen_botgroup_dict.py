import os
import re
from typing import List

SCRIPT_DIRECTORY = os.path.dirname(os.path.realpath(__file__))
FILE_PATH = os.path.normpath(
    os.path.join(SCRIPT_DIRECTORY, '../src/BotExtended/Group/BotGroup.cs'))


def main():
    botGroupIndex = 0
    bossGroupStartIndex = 200  # unlikely to change so meh
    botGroupRe = re.compile(r"^\s*([a-zA-Z_][a-zA-Z0-9_]+).*,.*$")
    botGroups: List[str] = []

    with open(FILE_PATH, 'r') as file:
        for line in file:
            match = botGroupRe.match(line)
            if match:
                botGroupName = match.group(1)

                if botGroupName.startswith('Boss_') and botGroupIndex < bossGroupStartIndex:
                    botGroupIndex = bossGroupStartIndex
                botGroups.append(str(botGroupIndex) + ': ' + botGroupName)
                botGroupIndex += 1

    for g in botGroups:
        print(g)


if __name__ == "__main__":
    main()
