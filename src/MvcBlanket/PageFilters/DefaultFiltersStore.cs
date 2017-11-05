/*
MVC Blanket Library Copyright (C) 2009-2012 Leonid Gordo

This library is free software; you can redistribute it and/or modify it under the terms of the GNU Lesser General Public License as published by the Free Software Foundation; 
either version 3.0 of the License, or (at your option) any later version.

This library is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. 
See the GNU Lesser General Public License for more details.

You should have received a copy of the GNU Lesser General Public License along with this library; 
if not, write to the Free Software Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA 02111-1307 USA 
*/

using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Web.Mvc;

namespace MvcBlanket.PageFilters
{
    public class DefaultFiltersStore : IFiltersStore
    {
        public ControllerContext Context { get; set; }

        public void SaveFilters(IDictionary<string, string> filtersToSave)
        {
            var stream = SerializeFilters(filtersToSave);
            byte[] buffer = new byte[stream.Length];
            stream.Position = 0;
            stream.Read(buffer, 0, (int)stream.Length);
            SaveToPermanentStore(buffer);
        }

        protected virtual Stream SerializeFilters(IDictionary<string, string> filtersToSave)
        {
            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream ms = new MemoryStream();
            bf.Serialize(ms, filtersToSave);
            return ms;
        }

        protected virtual void SaveToPermanentStore(byte[] buffer)
        {
            Context.RequestContext.HttpContext.Session[GetSessionFiltersKey()] = buffer;
        }

        public IDictionary<string, string> LoadFilters(IDictionary<string, string> dynamicFilters)
        {
            var buffer = LoadFromPermanentStore();
            var storedFilters = buffer != null ? DeserializeFilters(new MemoryStream(buffer)) : new Dictionary<string, string>();

            Dictionary<string, string> result = new Dictionary<string, string>();
            var keys = dynamicFilters.Keys.Concat(storedFilters.Keys).Distinct();
            foreach (var key in keys)
            {
                if (dynamicFilters.ContainsKey(key) && storedFilters.ContainsKey(key))
                {
                    if (!result.ContainsKey(key))
                        result.Add(key, dynamicFilters[key]);
                }
                else if (dynamicFilters.ContainsKey(key))
                {
                    if (!result.ContainsKey(key))
                        result.Add(key, dynamicFilters[key]);
                }
                else if (storedFilters.ContainsKey(key))
                    if (!result.ContainsKey(key))
                        result.Add(key, storedFilters[key]);
            }
            return result;
        }

        protected virtual byte[] LoadFromPermanentStore()
        {
            return (byte[])Context.RequestContext.HttpContext.Session[GetSessionFiltersKey()];
        }

        protected virtual IDictionary<string, string> DeserializeFilters(Stream stream)
        {
            BinaryFormatter bf = new BinaryFormatter();
            object result = bf.Deserialize(stream);
            return result as Dictionary<string, string>;
        }

        protected virtual string GetSessionFiltersKey()
        {
            string controllerName = Context.RouteData.Values["Controller"].ToString();
            string actionName = Context.RouteData.Values["Action"].ToString();
            return "Filters_" + controllerName + "_" + actionName;
        }
    }
}
