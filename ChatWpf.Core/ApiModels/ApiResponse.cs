namespace ChatWpf.Core.ApiModels
{
    public class ApiResponse
    {
        public bool Successful => ErrorMessage == null;

        public string ErrorMessage { get; set; }

        public object Response { get; set; }

        public ApiResponse()
        {

        }

    }

    public class ApiResponse<T> : ApiResponse
    {
        public new T Response { get => (T)base.Response; set => base.Response = value; }
    }
}
