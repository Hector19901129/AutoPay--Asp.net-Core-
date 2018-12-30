using System.Collections.Generic;
using System.Linq;

namespace AutoPay.Models
{
    public class JqDataTableRequestModel
    {
        public int Draw { get; set; }
        public int Start { get; set; }
        public int Length { get; set; }
        public JqDataTableSearchModel Search { get; set; }
        public List<JqDataTableSortModel> Order { get; set; }
        public List<JqDataTableColumnModel> Columns { get; set; }
        public string GetSortExpression()
        {
            var columnIndex = Order.FirstOrDefault()?.Column ?? 0;
            var sortDir = Order.FirstOrDefault()?.Dir ?? "asc";
            var columnName = Columns[columnIndex].Data;
            return $"{columnName} {sortDir}";
        }
    }
}
