using System.Collections;
using System.Text.RegularExpressions;
using Windows.Graphics.Imaging;
using Windows.Media.Ocr;
using Windows.Storage.Streams;

namespace Vision
{
    public class Ocr
    {
        public const string KoKR = "ko-KR";
        public const string EnUS = "en-US";

        readonly OcrEngine _ocrEngine;
        readonly DataWriter _writer;
        readonly InMemoryRandomAccessStream _stream;
        readonly Regex _allowedRegex = new(@"[^a-zA-Z0-9가-힣ㄱ-ㅎㅏ-ㅣ]");

        public Ocr(string code)
        {
            _ocrEngine = OcrEngine.TryCreateFromLanguage(new(code))
                      ?? OcrEngine.TryCreateFromUserProfileLanguages()
                      ?? throw new InvalidOperationException("OCR initialize error. confirm language package.");

            _stream = new InMemoryRandomAccessStream();
            _writer = new DataWriter(_stream);
        }

        public async Task<OcrResult> GetOcrAsync(byte[] imageData)
        {
            if (imageData == null || imageData.Length == 0)
                throw new Exception("image is empty");

            _stream.Size = 0;
            _writer.WriteBytes(imageData);
            await _writer.StoreAsync();
            _stream.Seek(0);

            var decoder = await BitmapDecoder.CreateAsync(_stream);
            using var softwareBitmap = await decoder.GetSoftwareBitmapAsync(
                BitmapPixelFormat.Bgra8,
                BitmapAlphaMode.Premultiplied);

            var result = await _ocrEngine.RecognizeAsync(softwareBitmap);
            return result;
        }
    }
}
