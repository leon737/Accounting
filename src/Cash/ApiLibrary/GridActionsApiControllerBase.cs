using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Security.Claims;
using System.Web.Http;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Cash.Web.Binders;
using Cash.Web.Extensions;
using DevExtreme.AspNet.Data;
using Functional.Fluent;
using Functional.Fluent.MonadicTypes;
using Newtonsoft.Json;

namespace Cash.Web.ApiLibrary
{
    public abstract class GridActionsApiControllerBase<TEntity, TViewModel, TUpdateRequest, TCreateRequest> : ApiController
        where TViewModel: new()
    {
        protected IMapper Mapper;

        protected GridActionsApiControllerBase(IMapper mapper)
        {
            Mapper = mapper;
        }

        protected HttpResponseMessage Get(Func<IQueryable<TEntity>> sourceFunc, DataSourceLoadOptions loadOptions)
        {
            return Request.CreateResponse(DataSourceLoader.Load(sourceFunc().ProjectTo<TViewModel>(Mapper.ConfigurationProvider), loadOptions));
        }

        protected HttpResponseMessage Put(Func<Guid, Result<TEntity>> getEntityFunc, Func<Guid, TUpdateRequest, Guid, IResult> updateFunc, FormDataCollection form, ClaimsPrincipal principal)
        {
            var key = Guid.Parse(form.Get("key"));
            var values = form.Get("values");

            var entity = getEntityFunc(key);
            if (entity.IsFailed)
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Not found");

            var model = Mapper.Map<TViewModel>(entity.Value);
            JsonConvert.PopulateObject(values, model);

            Validate(model);
            if (!ModelState.IsValid)
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState.GetFullErrorMessage());

            var claim = principal.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
            if (claim == null)
                return Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "No valid claims found");

            var updateRequest = Mapper.Map<TUpdateRequest>(model);

            return updateFunc(key, updateRequest, Guid.Parse(claim.Value)).IsSucceed
                ? Request.CreateResponse(HttpStatusCode.OK)
                : Request.CreateErrorResponse(HttpStatusCode.NotFound, "Not found");
        }

        protected HttpResponseMessage Post(Func<TCreateRequest, Guid, IResult> insertFunc, FormDataCollection form, ClaimsPrincipal principal)
        {
            var values = form.Get("values");

            var model = new TViewModel();
            JsonConvert.PopulateObject(values, model);

            Validate(model);
            if (!ModelState.IsValid)
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState.GetFullErrorMessage());

            var claim = principal.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
            if (claim == null)
                return Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "No valid claims found");

            var createRequest = Mapper.Map<TCreateRequest>(model);

            return insertFunc(createRequest, Guid.Parse(claim.Value)).IsSucceed
                ? Request.CreateResponse(HttpStatusCode.OK)
                : Request.CreateErrorResponse(HttpStatusCode.NotFound, "Not found");
        }

        protected HttpResponseMessage Delete(Func<Guid, IResult> deleteFunc, FormDataCollection form)
        {
            var key = Guid.Parse(form.Get("key"));
            
            return deleteFunc(key).IsSucceed
                ? Request.CreateResponse(HttpStatusCode.OK)
                : Request.CreateErrorResponse(HttpStatusCode.NotFound, "Not found");
        }
    }
}