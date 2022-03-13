const BaseReader = require("./BaseReader");
const BufferReader = require("../../BufferReader");
const BufferWriter = require("../../BufferWriter");
const BooleanReader = require("./BooleanReader");
const StringReader = require("./StringReader");
const Int32Reader = require("./Int32Reader");
const ColorReader = require("./ColorReader");
const CharReader = require("./CharReader");
const NearUtil = require("./NearUtil");

/**
 * Boolean Reader
 * @class
 * @extends BaseReader
 */
class SFDItemReader extends BaseReader {
  /**
   * Reads Boolean from buffer.
   * @param {BufferReader} buffer
   * @returns {{}}
   */
  read(buffer) {
    const booleanReader = new BooleanReader();
    const stringReader = new StringReader();
    const int32Reader = new Int32Reader();
    const colorReader = new ColorReader();
    const charReader = new CharReader();

    const fileName = stringReader.read(buffer);
    const gameName = stringReader.read(buffer);
    const equipmentLayer = int32Reader.read(buffer);
    const id = stringReader.read(buffer);
    const jacketUnderBelt = booleanReader.read(buffer);
    const canEquip = booleanReader.read(buffer);
    const canScript = booleanReader.read(buffer);
    const colorPalette = stringReader.read(buffer);
    const width = int32Reader.read(buffer);
    const height = int32Reader.read(buffer);

    const num1 = buffer.readByte();
    const dynamicColorTable = [];

    for (let i = 0; i < num1; i++) {
      dynamicColorTable.push(colorReader.read(buffer));
    }

    const length1 = int32Reader.read(buffer);
    const num2 = charReader.read(buffer).charCodeAt();
    const parts = [];

    for (let index1 = 0; index1 < length1; index1++) {
      const type = int32Reader.read(buffer);
      const length2 = int32Reader.read(buffer);
      const size = width * height;
      const textures = [];

      for (let index2 = 0; index2 < length2; index2++) {
        if (booleanReader.read(buffer)) {
          let color = [0, 0, 0, 0];
          const data = [];
          let emptyImage = true;

          for (let index3 = 0; index3 < size * 4; index3 += 4) {
            if (booleanReader.read(buffer)) {
              data[index3] = color[0];
              data[index3 + 1] = color[1];
              data[index3 + 2] = color[2];
              data[index3 + 3] = color[3];
            } else {
              const num3 = buffer.readByte();
              data[index3] = color[0] = dynamicColorTable[num3][0];
              data[index3 + 1] = color[1] = dynamicColorTable[num3][1];
              data[index3 + 2] = color[2] = dynamicColorTable[num3][2];
              data[index3 + 3] = color[3] = dynamicColorTable[num3][3];
              emptyImage = false;
            }
          }

          const num4 = charReader.read(buffer).charCodeAt();

          if (emptyImage) {
            textures[index2] = null;
          } else {
            textures[index2] = { data, width, height };
          }
        } else {
          textures[index2] = null;
        }
      }

      parts[index1] = { textures, type };
    }

    const data = {
      gameName,
      fileName,
      equipmentLayer,
      id,
      jacketUnderBelt,
      canEquip,
      canScript,
      colorPalette,
      width,
      height,
      parts,
    };

    return {
      export: {
        type: "SFDItem",
        data,
      },
    };
  }

  /**
   * Writes Boolean into buffer
   * @param {BufferWriter} buffer
   * @param {Mixed} data
   * @param {ReaderResolver}
   */
  write(buffer, content, resolver) {
    const booleanReader = new BooleanReader();
    const stringReader = new StringReader();
    const int32Reader = new Int32Reader();
    const colorReader = new ColorReader();
    const charReader = new CharReader();
    const { data } = content;

    this.writeIndex(buffer, resolver);

    stringReader.write(buffer, data.fileName, null); // name property
    stringReader.write(buffer, data.gameName, null);
    int32Reader.write(buffer, data.equipmentLayer, null);
    stringReader.write(buffer, data.id, null);
    booleanReader.write(buffer, data.jacketUnderBelt, null);
    booleanReader.write(buffer, data.canEquip, null);
    booleanReader.write(buffer, data.canScript, null);
    stringReader.write(buffer, data.colorPalette, null);
    int32Reader.write(buffer, data.width, null); // tileWidth
    int32Reader.write(buffer, data.height, null); // tileHeight

    const { parts } = data;
    const dynamicColorTable = [];

    for (let index1 = 0; index1 < parts.length; index1++) {
      for (let index2 = 0; index2 < parts[index1].textures.length; index2++) {
        if (parts[index1].textures[index2] !== null) {
          for (let index3 = 0; index3 < parts[index1].textures[index2].data.length; index3 += 4) {
            const color = NearUtil.getColor(parts[index1].textures[index2].data, index3);
            if (!dynamicColorTable.find(c => NearUtil.sameColor(c, color))) {
              dynamicColorTable.push(color);
            }
          }
        }
      }
    }

    buffer.writeByte(dynamicColorTable.length);

    for (let index = 0; index < dynamicColorTable.length; index++) {
      colorReader.write(buffer, dynamicColorTable[index], null);
    }

    int32Reader.write(buffer, parts.length, null);
    charReader.write(buffer, '\n', null);

    for (let index1 = 0; index1 < parts.length; index1++) {
      int32Reader.write(buffer, parts[index1].type, null);
      int32Reader.write(buffer, parts[index1].textures.length, null);

      for (let index2 = 0; index2 < parts[index1].textures.length; index2++) {
        if (parts[index1].textures[index2] !== null) {
          booleanReader.write(buffer, true, null);
          let num1 = parts[index1].textures[index2].data[0];
          if (num1 >= NearUtil.ByteMaxValue)
            num1 = 0;
          let color = [num1, 0, 0, 0];

          for (let index3 = 0; index3 < parts[index1].textures[index2].data.length; index3 += 4) {
            const color3 = NearUtil.getColor(parts[index1].textures[index2].data, index3);
            if (NearUtil.sameColor(color3, color)) {
              booleanReader.write(buffer, true, null);
            } else {
              booleanReader.write(buffer, false, null);
              const num2 = dynamicColorTable.findIndex(c => NearUtil.sameColor(c, color3));
              if (num2 === -1) {
                throw new Error('ItemsContentTypeWrite Error: Color " + value.parts[index1].Pixels[index2][index3].ToString() + " not inside color table')
              }
              buffer.writeByte(num2);
              color = color3;
            }
          }
          charReader.write(buffer, '\n', null);
        } else {
          booleanReader.write(buffer, false, null);
        }
      }
    }
  }
}

module.exports = SFDItemReader;

// EquipmentLayer
// Equipment.GetText()
// public static string GetText(int layer)
// {
//   switch (layer)
//   {
//     case 0:
//       return LanguageHelper.GetText("equipment.layer.skin");
//     case 1:
//       return LanguageHelper.GetText("equipment.layer.chestUnder");
//     case 2:
//       return LanguageHelper.GetText("equipment.layer.legs");
//     case 3:
//       return LanguageHelper.GetText("equipment.layer.waist");
//     case 4:
//       return LanguageHelper.GetText("equipment.layer.feet");
//     case 5:
//       return LanguageHelper.GetText("equipment.layer.chestOver");
//     case 6:
//       return LanguageHelper.GetText("equipment.layer.accessory");
//     case 7:
//       return LanguageHelper.GetText("equipment.layer.hands");
//     case 8:
//       return LanguageHelper.GetText("equipment.layer.head");
//     case 9:
//       return "X";
//     default:
//       return LanguageHelper.GetText("genera.none");
//   }
// }
// }

// ItemPart.Type
// 0: head
// 1: body
// 2: arm
// 3: fist
// 4: leg

// animations[0].frames[0].parts[0].id
// id: AnimationPartData.GlobalId or globalId in short
// localId = abs(globalId % 50)
// ItemPart.Type = globalId / 50
// Texture = ItemPart.Textures[localId]

// Textures.RecolorTexture() applies the color to the texture whose pixels have the following colors
// rgb(x, 0, 0): primary color
// rgb(0, x, 0): secondary color
// rgb(0, 0, x): tertiary color
//
// Where x is the shade of the color. It has following values:
// 255: lightest
// 192: lighter
// 128: darkest
// 64: never used
// 32: never used
