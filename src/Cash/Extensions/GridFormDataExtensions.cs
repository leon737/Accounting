using System;
using Cash.Web.Models;

namespace Cash.Web.Extensions
{
    public static class GridFormDataExtensions
    {
        /// <summary> Конвертация в <see cref="GridUpdateRequest"/> </summary>
        public static GridUpdateRequest ToUpdateRequest(this GridFormData formData)
        {
            return new GridUpdateRequest(Guid.Parse(formData.Key), formData.Values);
        }
    }
}