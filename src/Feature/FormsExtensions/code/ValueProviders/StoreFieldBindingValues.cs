using Feature.FormsExtensions.Fields.Bindings;
using Sitecore.ExperienceForms.Mvc.Pipelines.ExecuteSubmit;
using Sitecore.Mvc.Pipelines;

namespace Feature.FormsExtensions.ValueProviders
{
    public class StoreFieldBindingValues : MvcPipelineProcessor<ExecuteSubmitActionsEventArgs>
    {
        private readonly IFieldValueBinderMapFactory fieldValueBinderMapFactory;

        public StoreFieldBindingValues(IFieldValueBinderMapFactory fieldValueBinderMapFactory)
        {
            this.fieldValueBinderMapFactory = fieldValueBinderMapFactory;
        }

        public override void Process(ExecuteSubmitActionsEventArgs args)
        {
            if (args?.FormSubmitContext?.Fields == null)
            {
                return;
            }
            var valuesUpdated = false;
            foreach (var fieldModel in args.FormSubmitContext.Fields)
            {
                if (!(fieldModel is IBindingSettings bindingSettings))
                {
                    continue;
                }
                if (!bindingSettings.StoreBindingValue) { 
                    continue;
                }
                if (string.IsNullOrEmpty(bindingSettings.ValueProviderSettings?.ValueProviderItemId))
                {
                    continue;
                }
                var bindingHandler = fieldValueBinderMapFactory.GetBindingHandler(bindingSettings.ValueProviderSettings);
                
                var property = fieldModel.GetType().GetProperty("Value");
                if (property == null)
                {
                    continue;
                }

                var value = property.GetValue(fieldModel);
                if (value == null)
                {
                    continue;
                }
                bindingHandler.StoreValue(value);
                valuesUpdated = true;
            }
        }
    }
}