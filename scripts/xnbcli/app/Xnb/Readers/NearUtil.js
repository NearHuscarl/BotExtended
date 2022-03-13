class NearUtil {
  static ByteMaxValue = 255;

/**
 * @param {number[]} data
 * @returns {Uint32Array}
 */
  static getColor = (data, startIndex) => {
    return new Uint32Array([
      data[startIndex], // r
      data[startIndex + 1], // g
      data[startIndex + 2], // b
      data[startIndex + 3], // a
    ]);
  };

/**
 * @param {number[]} color
 * @returns {number}
 */
  static packedColor = (color) => {
      // You need to use unsigned int by using Uint32Array. Otherwise, color[3] << 24 will be overflowed
      const c = new Uint32Array(5);
      c[0] = color[0]        // r
      c[1] = color[1] <<  8; // g
      c[2] = color[2] << 16; // b
      c[3] = color[3] << 24; // a
      c[4] = c[0] | c[1] | c[2] | c[3]
      return c[4];
  }

  /**
   * @param {number[]} color1
   * @param {number[]} color2
   * @returns {boolean}
   */
  static sameColor = (color1, color2) => this.packedColor(color1) === this.packedColor(color2);
}

module.exports = NearUtil;
