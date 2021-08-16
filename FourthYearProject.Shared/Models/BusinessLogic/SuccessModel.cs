namespace _4thYearProject.Shared
{
    public class SuccessModel
    {
        public string Title { get; set; }
        public int StatusCode { get; set; }
        public int OrderId { get; set; }
        public string SuccessMessage { get; set; }
        public object Data { get; set; }
    }


    public class ErrorModel
    {
        public string Title { get; set; }
        public int StatusCode { get; set; }
        public string ErrorMessage { get; set; }
    }
}