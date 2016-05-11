#[cfg(target_os = "linux")]
extern crate x11cap;

extern crate image;

#[cfg(not(target_os = "linux"))]
extern crate screenshot;
extern crate bmp;

#[cfg(not(target_os = "linux"))]
fn capture_frame (){
    let s = screenshot::get_screenshot(0).unwrap();
    println!("{} x {} x {} = {} bytes", s.height(), s.width(), s.pixel_width(), s.raw_len());
    // println!("{:?}", s.as_ref() );
    // PNG
    image::save_buffer("test.png",
        s.as_ref(), s.width() as u32, s.height() as u32, image::RGBA(8))
    .unwrap();
    // BMP
    let mut img = bmp::Image::new(s.width() as u32, s.height() as u32);
    for row in (0..s.height()) {
        for col in (0..s.width()) {
            let p = s.get_pixel(row, col);
            // WARNING rust-bmp params are (x, y)
            img.set_pixel(col as u32, row as u32, bmp::Pixel {r: p.r, g: p.g, b: p.b});
        }
    }
    img.save("test.bmp").unwrap();
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
