namespace Model
{
    public class EsImage
    {
        public long ESImageId { get; set; }

        public string ImageMimeType { get; set; }

        public byte[] Thumbnail { get; set; }
        public byte[] ImageData { get; set; }
    }
}