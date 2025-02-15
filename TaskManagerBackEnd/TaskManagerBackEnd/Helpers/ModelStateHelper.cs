using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace TaskManagerBackEnd.Helpers
{
    public static class ModelStateHelper
    {
        public static Dictionary<string, string[]> ToErrorDictionary(ModelStateDictionary modelState)
        {
            return modelState
                .Where(ms => ms.Value.Errors.Count > 0)
                .ToDictionary(
                    kvp => kvp.Key,
                    kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray()
                );
        }
    }
}