namespace Talabat2.APIs.Errors
{
    public class ApiErrorResponse
    {
        //By This Class Can Show Error Response As I Want In This Case 
        //Show StatusCode Of Error And Message
        public int StatusCode { get; set; }
        public string? Message { get; set; }
        //by Constructor Intialize StatusCode And Message If Founded if Not Can equal Message Of Below Function
        public ApiErrorResponse(int StatusCode,string? Messgae=null)
        {
            this.StatusCode = StatusCode;
            this.Message = Messgae??GetDafaultMessageForStatusCode(StatusCode);
        }
        private string? GetDafaultMessageForStatusCode(int StatusCode)
        {
            return StatusCode switch
            {
                400 => "A Bad Request , You Have Made",
                401 => "Authorized ,You Are Not",
                404 => "Resources Not Found",
                500 => "There Is Server Error",
                _ => null
            };
        }
    }
}
