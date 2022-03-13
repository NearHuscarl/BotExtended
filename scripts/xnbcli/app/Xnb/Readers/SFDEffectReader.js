const BaseReader = require("./BaseReader");
const BufferReader = require("../../BufferReader");
const BufferWriter = require("../../BufferWriter");
const BooleanReader = require("./BooleanReader");
const StringReader = require("./StringReader");
const Int32Reader = require("./Int32Reader");
const ColorReader = require("./ColorReader");
const CharReader = require("./CharReader");
const Texture2DReader = require("./Texture2DReader");
const NearUtil = require("./NearUtil");

/**
 * Boolean Reader
 * @class
 * @extends BaseReader
 */
class SFDEffectReader extends BaseReader {
  /**
   * Reads Boolean from buffer.
   * @param {BufferReader} buffer
   * @returns {{}}
   */
  read(buffer) {
    const booleanReader = new BooleanReader();
    const int32Reader = new Int32Reader();
    const colorReader = new ColorReader();

    const dynamicColorTable = [];
    const num1 = buffer.readByte();

    for (let i = 0; i < num1; i++) {
      dynamicColorTable.push(colorReader.read(buffer));
    }

    const width = int32Reader.read(buffer);
    const height = int32Reader.read(buffer);
    const size = width * height;

    const data = [];
    let color = [0, 0, 0, 0];

    for (let index = 0; index < size; index += 4) {
      if (booleanReader.read(buffer)) {
        data[index] = color[0];
        data[index + 1] = color[1];
        data[index + 2] = color[2];
        data[index + 3] = color[3];
      } else {
        const num2 = buffer.readByte();
        data[index] = color[0] = dynamicColorTable[num2][0];
        data[index + 1] = color[1] = dynamicColorTable[num2][1];
        data[index + 2] = color[2] = dynamicColorTable[num2][2];
        data[index + 3] = color[3] = dynamicColorTable[num2][3];
      }
    }

    return {
      export: {
        type: "SFDEffect",
        data,
        width,
        height,
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
    const int32Reader = new Int32Reader();
    const colorReader = new ColorReader();

    this.writeIndex(buffer, resolver);

    const dynamicColorTable = [];
    const { data, width, height } = content.export;

    for (let index = 0; index < data.length; index += 4) {
      const color = NearUtil.getColor(data, index);
      if (!dynamicColorTable.find(c => NearUtil.sameColor(c, color))) {
        dynamicColorTable.push(color);
      }
    }

    buffer.writeByte(dynamicColorTable.length);

    for (let index = 0; index < dynamicColorTable.length; index++) {
      colorReader.write(buffer, dynamicColorTable[index], null);
    }

    int32Reader.write(buffer, width, null);
    int32Reader.write(buffer, height, null);

    let num1 = data[0];
    if (num1 >= NearUtil.ByteMaxValue) {
      num1 = 0;
    }
  
    let color = [num1, 0, 0, 0];

    for (let index = 0; index < data.length; index += 4) {
      const dataColor = NearUtil.getColor(data, index);
      if (NearUtil.sameColor(dataColor, color)) {
        booleanReader.write(buffer, true, null);
      } else {
        booleanReader.write(buffer, false, null);
        const num2 = dynamicColorTable.findIndex(c => NearUtil.sameColor(c, dataColor));
        if (num2 === -1) {
          throw new Error('Texture2DBContentTypeWrite Error: Color " + value.Pixels[index].ToString() + " not inside color table')
        }
        buffer.writeByte(num2);
        color = dataColor;
      }
    }
  }
}

module.exports = SFDEffectReader;
