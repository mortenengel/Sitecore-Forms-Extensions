using System;
using Sitecore.ExperienceForms.Models;
using Sitecore.ExperienceForms.Processing;
using Sitecore.ExperienceForms.Processing.Actions;

namespace Feature.FormsExtensions.SubmitActions.ShowFormPage
{
    public class ShowFormPageAction : SubmitActionBase<ShowFormPageData>
    {
        public ShowFormPageAction(ISubmitActionData submitActionData) : base(submitActionData)
        {
        }


        protected override bool Execute(ShowFormPageData data, FormSubmitContext formSubmitContext)
        {
            if (data.FormPageId == null || data.FormPageId == Guid.Empty)
            {
                return true; //we will not crash on this
            }
            ShowFormPageContext.FormPage = data.FormPageId;
            return true;
        }

    }
}