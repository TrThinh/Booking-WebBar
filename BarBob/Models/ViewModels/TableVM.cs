using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace BarBob.Models.ViewModels
{
    public class TableVM
    {
        public Table Table { get; set; }
        public TableType TableType { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> TableTypeList { get; set; }
    }
}
