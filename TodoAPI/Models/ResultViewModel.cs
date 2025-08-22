using NPOI.SS.Formula.Functions;

namespace TodoAPI.Models
{
    public class ResultViewModel<T>
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; } = string.Empty;
        public T? Data { get; set; }
    }
}
