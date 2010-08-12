
namespace HelpRequest.Controllers.ViewModels
{
    public class GenericViewModel
    {
        public string AppName;
        public string CallingPage;
        public static GenericViewModel Create(string appName)
        {
            return new GenericViewModel{AppName = appName};
        }
    }
}
