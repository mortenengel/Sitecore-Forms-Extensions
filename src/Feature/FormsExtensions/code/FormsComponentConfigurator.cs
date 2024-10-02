using Feature.FormsExtensions.Fields.ReCaptcha;
using Feature.FormsExtensions.Fields.ValueResolvers;
using Feature.FormsExtensions.ValueProviders;
using Microsoft.Extensions.DependencyInjection;
using Sitecore.DependencyInjection;

namespace Feature.FormsExtensions
{
    public class FormsComponentConfigurator : IServicesConfigurator
    {
        public void Configure(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IReCaptchaService, ReCaptchaService>(provider=>new ReCaptchaService(Sitecore.Configuration.Settings.GetSetting("GoogleCaptchaPrivateKey")));
            serviceCollection.AddSingleton<IFieldValueBinderMapFactory, FieldValueBinderMapFactory>();
            serviceCollection.AddSingleton<IFormsFieldValueResolver, FormsFieldValueResolver>();
        }
    }
}