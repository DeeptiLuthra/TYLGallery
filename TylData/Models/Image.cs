using System;

namespace TylData.Models
{
    public class Image
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public byte[] ImageBytes { get; set; }
        public string Encoding { get; set; }
        public string ImageData => $"data:{Encoding};base64,{Convert.ToBase64String(ImageBytes)}";
    }
}