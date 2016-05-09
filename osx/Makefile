OUT := record-screen

$(OUT): record-screen.m
	cc -o $@ $< -fobjc-arc -Wall -framework Foundation -framework AVFoundation -framework ApplicationServices -framework CoreVideo -framework CoreMedia -framework AppKit

.PHONY: clean
clean:
	rm $(OUT)
