const BaseReader = require('./BaseReader');
const NearUtil = require('./NearUtil');

/**
 * Rectangle Reader
 * @class
 * @extends BaseReader
 */
class ColorReader extends BaseReader {
    /**
     * Reads Rectangle from buffer.
     * @param {BufferReader} buffer
     * @returns {Uint8Array}
     */
    read(buffer) {
        return new Uint8Array([
             buffer.readByte(),
             buffer.readByte(),
             buffer.readByte(),
             buffer.readByte(),
        ])
    }

    /**
     * Writes Effects into the buffer
     * @param {BufferWriter} buffer
     * @param {{a: Number, r: Number, b: Number, g: Number}} content The data
     * @param {ReaderResolver} resolver
     */
    write(buffer, content, resolver) {
        this.writeIndex(buffer, resolver);
        buffer.writeUInt32(NearUtil.packedColor(content))
    }
}

module.exports = ColorReader;
