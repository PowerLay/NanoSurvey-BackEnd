namespace NanoSurvey_BackEnd.Survey
{
    public class Result
    {
        public int Id { get; set; }
        public Answer Answer { get; set; }
        public Question Question { get; set; }
        public Interview Interview { get; set; }
    }
}