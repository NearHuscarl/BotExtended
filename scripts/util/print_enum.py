import re
from typing import List, AnyStr

def print_enum(file_path: AnyStr):
    botTypeIndex = 0
    enumRe = re.compile(r"^\s*([a-zA-Z_][a-zA-Z0-9_]+).*,.*$")
    botTypes: List[str] = []

    with open(file_path, 'r') as file:
        for line in file:
            match = enumRe.match(line)
            if match:
                botGroupName = match.group(1)
                botTypes.append(str(botTypeIndex) + ': ' + botGroupName)
                botTypeIndex += 1

    for t in botTypes:
        print(t)
