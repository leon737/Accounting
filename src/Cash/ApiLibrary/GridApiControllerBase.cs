using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Security.Claims;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using AutoMapper;
using Cash.Web.Binders;
using Cash.Web.ModelBinders;
using Functional.Fluent;
using Functional.Fluent.MonadicTypes;

namespace Cash.Web.ApiLibrary
{
    public abstract class GridApiControllerBase<TEntity, TViewModel, TUpdateRequest, TCreateRequest> : GridActionsApiControllerBase<TEntity, TViewModel, TUpdateRequest, TCreateRequest>
        where TViewModel:new()
    {

        private readonly Func<IQueryable<TEntity>> _sourceFunc;
        private readonly Func<Guid, Result<TEntity>> _getEntityFunc;
        private readonly Func<Guid, TUpdateRequest, Guid, IResult> _updateFunc;
        private readonly Func<TCreateRequest, Guid, IResult> _insertFunc;
        private readonly Func<Guid, IResult> _deleteFunc;

        protected GridApiControllerBase(IMapper mapper,
            Func<IQueryable<TEntity>> sourceFunc,
            Func<Guid, Result<TEntity>> getEntityFunc,
            Func<Guid, TUpdateRequest, Guid, IResult> updateFunc,
            Func<TCreateRequest, Guid, IResult> insertFunc,
            Func<Guid, IResult> deleteFunc) : base(mapper)
        {
            _sourceFunc = sourceFunc;
            _getEntityFunc = getEntityFunc;
            _updateFunc = updateFunc;
            _insertFunc = insertFunc;
            _deleteFunc = deleteFunc;
        }

        [HttpGet]
        public HttpResponseMessage Get(DataSourceLoadOptions loadOptions)
        {
            return Get(_sourceFunc, loadOptions);
        }

        [HttpPut]
        public HttpResponseMessage Put(FormDataCollection form, [ModelBinder(typeof(PrincipalModelBinder))] ClaimsPrincipal principal)
        {
            return Put(_getEntityFunc, _updateFunc, form, principal);
        }

        [HttpPost]
        public HttpResponseMessage Post(FormDataCollection form, [ModelBinder(typeof(PrincipalModelBinder))] ClaimsPrincipal principal)
        {
            return Post(_insertFunc, form, principal);
        }

        [HttpDelete]
        public HttpResponseMessage Delete(FormDataCollection form)
        {
            return Delete(_deleteFunc, form);
        }

    }
}