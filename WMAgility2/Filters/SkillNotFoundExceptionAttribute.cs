using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using WMAgility2.Utilities;

namespace WMAgility2.Filters
{
    public class SkillNotFoundExceptionAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {

            if (context.Exception is SkillNotFoundException)
            {
                context.Result = new ViewResult
                {
                    ViewName = "SkillNotFound",
                    ViewData = new ViewDataDictionary(new EmptyModelMetadataProvider(), new ModelStateDictionary())
                    {
                        Model = "An error occured while searching the requested skill"
                    }
                };
            }
        }
    }
}