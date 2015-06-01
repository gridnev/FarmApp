using System.ComponentModel;
using System.Web.Mvc;
using FarmApp.Models;

namespace FarmApp.ViewModels
{
    public class ProductViewModel
    {
        [HiddenInput]
        public long Id { get; set; }

        [DisplayName("Наименование")]
        public string Title { get; set; }

        [DisplayName("Тип")]
        public ProductType Type { get; set; }

        [DisplayName("Описание")]
        public string Description { get; set; }
    }
}
