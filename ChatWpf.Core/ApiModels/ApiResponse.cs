namespace ChatWpf.Core.ApiModels
{
    public class ApiResponse<T>
    {
        public bool Successful => ErrorMessage == null;

        public string ErrorMessage { get; set; }

        public T Response { get; set; }

        public ApiResponse()
        {

        }

    }
}
