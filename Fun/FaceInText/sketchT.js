// Image to ASCII
// The Coding Train / Daniel Shiffman
// https://thecodingtrain.com/CodingChallenges/166-ascii-image.html
// https://youtu.be/55iwMYv8tGI

// ASCII video: https://editor.p5js.org/codingtrain/sketches/KTVfEcpWx
// ASCII image canvas: https://editor.p5js.org/codingtrain/sketches/r4ApYWpH_
// ASCII image DOM: https://editor.p5js.org/codingtrain/sketches/ytK7J7d5j
// ASCII image source text: https://editor.p5js.org/codingtrain/sketches/LNBpdYQHP
// ASCII image weather API: https://editor.p5js.org/codingtrain/sketches/DhdqcoWn4

//const density = "Ñ@#W$9876543210?!abc;:+=-,._                    ";
 //const density = '@#%+=|i-:.     ';
// const density = '        .:░▒▓█';
 const density = '█▓▒░:.        ';
 
let gloria;

function preload(){
  gloria = loadImage("gloria50.jpg")
}

function setup() {
  noCanvas();

  background(0);
  image(gloria, 0, 0, width, height);

  let w = width / gloria.width;
  let h = height / gloria.height;
  gloria.loadPixels();

  for (let j = 0; j < gloria.height; j++) {
    let row = '';
    for (let i = 0; i < gloria.width; i++) {
      const pixelIndex = (i + j * gloria.width) * 4;
      const r = gloria.pixels[pixelIndex + 0];
      const g = gloria.pixels[pixelIndex + 1];
      const b = gloria.pixels[pixelIndex + 2];
      const avg = (r + g + b) / 3;

      // noStock();
      // fill(r,g,b);
      // square(i*w,j*h,w);
      const len = density.length;
      const charIndex = floor(map(avg, 0, 255, len, 0));
      const c = density.charAt(charIndex);
      if (c == ' ') row += '&nbsp;'
      else row += c;
    }
    createDiv(row);
  }
}
