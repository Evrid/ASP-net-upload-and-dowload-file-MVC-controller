using System.ComponentModel.DataAnnotations;

namespace FileUploadDownloadTest.Models
{
    public class FileModel
    {
        [Key]
        [Required]
        public int ID { get; set; }
        [Required]
        public string FileName { get; set; }
        [Required]
        public string FilePath { get; set; }
        [Required]
        public string OriginalFileName { get; set; }


    }
}
