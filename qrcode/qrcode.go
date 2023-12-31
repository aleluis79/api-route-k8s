package main

import (
	"bytes"
	"fmt"
	"image"
	"log"

	// import gif, jpeg, png
	_ "image/gif"
	_ "image/jpeg"
	_ "image/png"
	"io"
	"net/http"
	"strings"

	// import bmp, tiff, webp
	_ "golang.org/x/image/bmp"
	_ "golang.org/x/image/tiff"
	_ "golang.org/x/image/webp"

	"github.com/makiuchi-d/gozxing"
	"github.com/makiuchi-d/gozxing/multi/qrcode"
)

func main() {
	addr := "0.0.0.0:8888"
	mux := http.NewServeMux()
	mux.HandleFunc("/scan", post)
	server := &http.Server{Addr: addr, Handler: mux}

	log.SetFlags(log.LstdFlags | log.Lshortfile)

	log.Println("Listening in " + addr)

	server.ListenAndServe()
}

// handle post request
func post(w http.ResponseWriter, r *http.Request) {
	if r.Method != "POST" {
		w.WriteHeader(http.StatusMethodNotAllowed)
		return
	}
	b := new(bytes.Buffer)
	if _, err := io.Copy(b, r.Body); err != nil {
		msg := fmt.Sprintf("Failed to read request body: %v", err)
		http.Error(w, msg, http.StatusInternalServerError)
		return
	}
	res, err := scan(b.Bytes())
	if err != "" {
		msg := fmt.Sprintf("Internal server error: %v", err)
		http.Error(w, msg, http.StatusInternalServerError)
		return
	}
	w.Write([]byte(res))
}

func scan(b []byte) (string, string) {
	img, _, err := image.Decode(bytes.NewReader(b))
	if err != nil {
		msg := fmt.Sprintf("failed to read image: %v", err)
		return "", msg
	}

	source := gozxing.NewLuminanceSourceFromImage(img)
	bin := gozxing.NewHybridBinarizer(source)
	bbm, err := gozxing.NewBinaryBitmap(bin)

	if err != nil {
		msg := fmt.Sprintf("error during processing: %v", err)
		return "", msg
	}

	qrReader := qrcode.NewQRCodeMultiReader()
	result, err := qrReader.DecodeMultiple(bbm, nil)
	if err != nil {
		msg := fmt.Sprintf("unable to decode QRCode: %v", err)
		return "", msg
	}
	strRes := []string{}
	for _, element := range result {
		strRes = append(strRes, element.String())
	}

	res := strings.Join(strRes, "\n")

	log.Println("Scan result OK: " + res)

	return res, ""
}
