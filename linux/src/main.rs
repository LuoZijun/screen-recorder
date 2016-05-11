#[cfg(target_os = "linux")]
extern crate x11cap;

extern crate image;

#[cfg(not(target_os = "linux"))]
extern crate screenshot;
extern crate bmp;

#[cfg(not(target_os = "linux"))]
fn capture_frame (){
    let s = screenshot::get_screenshot(0).unwrap();
    println!("{} x {} x {} = {} bytes, Row Length: {}", s.height(), s.width(), s.pixel_width(), s.raw_len(), s.row_len() );
    
    // PNG Format
    // image::save_buffer("test.png",
    //     s.as_ref(), s.width() as u32, s.height() as u32, image::RGBA(8))
    // .unwrap();

    let width  = s.width();
    let height = s.height();
    let pixel_width = s.pixel_width();
    // Image Bytes Length
    let bytes  = s.raw_len();
    let image_buff = s.as_ref();

    let mut pixels: Vec<Vec<Vec<u8>>> = Vec::with_capacity(height);
    for row in (0..height) {
        let mut lines: Vec<Vec<u8>> = Vec::with_capacity(width);
        for col in (0..width) {
            let idx = row*width*pixel_width + col*pixel_width;
            let mut pixel: Vec<u8> = Vec::with_capacity(pixel_width);
            for i in (idx..(idx+pixel_width-1)) {
                // { b ,g ,r, a }
                pixel.push(image_buff[i]);
            }
            lines.push(pixel);
        }
        pixels.push(lines);
    }
    // println!("End s: {} ns: {} ...", time::precise_time_s(), time::precise_time_ns());
    println!("Height: {} Width: {} Pixel Width: {}", pixels.len(), pixels[0].len(), pixels[0][0].len() );

    // BMP Format
    let mut im = bmp::Image::new(width as u32, height as u32);
    for row in (0..height) {
        for col in (0..width) {
            let p = s.get_pixel(row, col);
            // println!("Raw Pixel: R: {} G: {} B: {} A: {}", p.r, p.g, p.b, p.a  );
            // println!("Pixel: {:?}", pixels[row][col] );
            im.set_pixel(
                col as u32, 
                row as u32,
                // Pixel { r , g , b }
                bmp::Pixel {r: pixels[row][col][2], g: pixels[row][col][1], b: pixels[row][col][0]}
            );
        }
    }
    im.save("test.bmp").unwrap();
    println!("Done.");
}

#[cfg(target_os = "linux")]
fn capture_frame (){
    let mut capturer = x11cap::Capturer::new(Screen::Default).unwrap();
    let tmp = capturer.capture_frame().unwrap().0.iter()
              .any(|p| p.r != 0 || p.g != 0 || p.b != 0);
    println!("{:?}", tmp );
}

fn main(){
    capture_frame();
}
