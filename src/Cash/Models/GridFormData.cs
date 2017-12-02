namespace Cash.Web.Models
{
    /// <summary> Данные запроса на обновление данных от грида </summary>
    public class GridFormData
    {
        /// <summary> Ключ строки </summary>
        public string Key { get; set; }

        /// <summary> Данные  </summary>
        public string Values { get; set; }
    }
}