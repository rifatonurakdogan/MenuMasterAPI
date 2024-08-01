namespace MenuMasterAPI.WebAPI.Helper
{
    public class JsonResponse
    {
        public JsonResponse(string response)
        {
            Response = response;
        }

        public string Response {  get; set; }
    }
}
