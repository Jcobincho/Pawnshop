namespace Pawnshop.Domain.FileStorage
{
    public class S3Configuration
    {
        public string S3Url { get; set; }
        public string AccessKey { get; set; }
        public string SecretKey { get; set; }
        public string BucketName { get; set; }
    }
}
