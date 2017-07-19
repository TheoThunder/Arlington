using DataAnnotationsExtensions.ClientValidation;

[assembly: WebActivator.PreApplicationStartMethod(typeof(Data.AppStart_RegisterClientValidationExtensions), "Start", callAfterGlobalAppStart: true)]
 
namespace Data {
    public static class AppStart_RegisterClientValidationExtensions {
        public static void Start() {
            DataAnnotationsModelValidatorProviderExtensions.RegisterValidationExtensions();            
        }
    }
}