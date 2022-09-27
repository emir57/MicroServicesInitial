namespace FreeCourse.Shared.Messages
{
    public class CourseNameChangedEvent
    {
        public CourseNameChangedEvent()
        {

        }
        public CourseNameChangedEvent(string courseId, string upatedName) : this()
        {
            CourseId = courseId;
            UpatedName = upatedName;
        }

        public string CourseId { get; set; }
        public string UpatedName { get; set; }
    }
}
