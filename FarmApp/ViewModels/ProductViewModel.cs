using System.ComponentModel;
using FarmApp.Models;

namespace FarmApp.ViewModels
{
    public class ProductViewModel
    {
        [DisplayName("Наименование")]
        public string Title { get; set; }

        [DisplayName("Тип")]
        public ProductType Type { get; set; }

        [DisplayName("Описание")]
        public string Description { get; set; }
    }
}
