using System;
using AutoMapper;
using Functional.Fluent.Helpers;
using Functional.Fluent.MonadicTypes;
using Newtonsoft.Json;

namespace Cash.Web.Models
{
    /// <summary> Запрос на обновление данных от грида </summary>
    public class GridUpdateRequest
    {
        private readonly string _values;

        /// <summary> Запрос на обновление данных от грида </summary>
        public GridUpdateRequest(Guid key, string values)
        {
            Key = key;
            _values = values;
        }

        /// <summary> Ключ записи </summary>
        public Guid Key { get; }

        /// <summary> Получение данных view-модели от клиента </summary>
        public TViewModel GetViewModel<TViewModel>()
        {
            return JsonConvert.DeserializeObject<TViewModel>(_values);
        }

        /// <summary> Наполнение view-модели слиянием данных из хранилища и данными от клиента </summary>
        public Result<TViewModel> GetViewModel<TViewModel, TEntity>(Func<Guid, Result<TEntity>> getEntityFunc)
        {
            var entity = getEntityFunc(Key);
            if (entity.IsFailed)
                return Result.Fail<TViewModel>();

            var model = Mapper.Map<TViewModel>(entity.Value);
            JsonConvert.PopulateObject(_values, model);

            return Result.Success(model);
        }
    }
}