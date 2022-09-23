namespace FreeCourse.Services.PhotoStock.Dtos
{
    public class PhotoDto
    {
        public string Url { get; set; }
        public PhotoDto()
        {

        }
        public PhotoDto(string url)
        {
            Url = url;
        }
    }
}
