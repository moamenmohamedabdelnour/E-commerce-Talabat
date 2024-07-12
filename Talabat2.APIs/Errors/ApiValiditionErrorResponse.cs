namespace Talabat2.APIs.Errors
{
    public class ApiValiditionErrorResponse:ApiErrorResponse
    {
        //دا خاص بس بال validation Error 
        //عشان الشكل اللي عايزه يظهر في الريسبونس متغير بحيث ان هضيف اراي للايرور
        public IEnumerable<string> Errors { get; set; }
        public ApiValiditionErrorResponse():base(400)
        {
            Errors=new List<string>();
            //Will Assign Values To Errors Array in Program File By Configure In ApiBehaviorOptions
        }
    }
}
