using Microsoft.Owin;
using Owin;
using System.Web.Http;
using System.Web.Http.OData.Extensions;
using System;
using System.Web.Http.Controllers;
using System.Web.Http.Dependencies;
using System.Collections.Generic;
using System.Web.Http.Metadata;
using System.Web.Http.Validation;
using System.Web.Http.ModelBinding;
using System.Web.Http.ValueProviders;
using System.Globalization;

[assembly: OwinStartup(typeof(GenericData.Startup))]

namespace GenericData
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var apiConfig = new HttpConfiguration();

            apiConfig.MapHttpAttributeRoutes();
            apiConfig.Routes.MapHttpRoute("default", "api/{controller}/{action}");
            apiConfig.AddODataQueryFilter();
            apiConfig.DependencyResolver = new DependencyResolver();
            //apiConfig.Services.Replace(typeof(ModelMetadataProvider), new GenericModelMetadataProvider());
            //apiConfig.Services.Clear(typeof(ModelValidatorProvider));
            //apiConfig.Services.Add(typeof(ModelValidatorProvider), new CustomModelValidatorProvider());
            //apiConfig.Services.Clear(typeof(ModelBinderProvider));
            //apiConfig.Services.Add(typeof(ModelBinderProvider), new CustomModelBinderProvider());
            //apiConfig.Services.Replace(typeof(ValueProviderFactory), new CustomValueProviderFactory());
            app.UseWebApi(apiConfig);


            app.Run(context =>
            {
                context.Response.ContentType = "text/plain";
                return context.Response.WriteAsync("OWIN here!");
            });
        }

        class DependencyResolver : IDependencyResolver
        {
            public IDependencyScope BeginScope()
            {
                return this;
            }

            public void Dispose()
            {

            }

            public object GetService(Type serviceType)
            {
                if (serviceType == typeof(ModelMetadataProvider))
                    return new 
                return null;
            }

            public IEnumerable<object> GetServices(Type serviceType)
            {
                return new object[0];
            }
        }

        class CustomModelBinderProvider : ModelBinderProvider
        {
            public override IModelBinder GetBinder(HttpConfiguration configuration, Type modelType)
            {
                return new CustomModelBinder();
            }
        }

        class CustomValueProviderFactory : ValueProviderFactory
        {
            public override IValueProvider GetValueProvider(HttpActionContext actionContext)
            {
                throw new NotImplementedException();
            }
        }

        class CutstomValueProvider : IValueProvider
        {
            public bool ContainsPrefix(string prefix)
            {
                return true;
            }

            public ValueProviderResult GetValue(string key)
            {
                return new CustomValueProviderResult("a", "a", CultureInfo.InvariantCulture);
            }
        }

        class CustomValueProviderResult : ValueProviderResult
        {
            public CustomValueProviderResult(object rawValue, string attemptedValue, CultureInfo culture) : base(rawValue, attemptedValue, culture)
            {
            }
        }

        class CustomModelBinder : IModelBinder
        {
            public bool BindModel(HttpActionContext actionContext, ModelBindingContext bindingContext)
            {
                return true;
            }
        }

        class GenericModelMetadataProvider : ModelMetadataProvider
        {
            public override IEnumerable<ModelMetadata> GetMetadataForProperties(object container, Type containerType)
            {
                throw new NotImplementedException();
            }

            public override ModelMetadata GetMetadataForProperty(Func<object> modelAccessor, Type containerType, string propertyName)
            {
                throw new NotImplementedException();
            }

            public override ModelMetadata GetMetadataForType(Func<object> modelAccessor, Type modelType)
            {
                return new ModelMetadata(this, null, modelAccessor, modelType, null);
            }
        }

        class CustomModelValidatorProvider : ModelValidatorProvider
        {
            public override IEnumerable<ModelValidator> GetValidators(ModelMetadata metadata, IEnumerable<ModelValidatorProvider> validatorProviders)
            {
                return new ModelValidator[] {
                    new CustomModelValidator(validatorProviders)
                };
            }
        }

        class CustomModelValidator : ModelValidator
        {
            public CustomModelValidator(IEnumerable<ModelValidatorProvider> validatorProviders)
                : base(validatorProviders)
            {
            }

            public override IEnumerable<ModelValidationResult> Validate(ModelMetadata metadata, object container)
            {
                return new[] {
                    new ModelValidationResult{
                        MemberName = metadata.PropertyName,
                        Message = "OK"
                    }
                };
            }
        }
    }
}
