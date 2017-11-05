using Cash.Domain.Models;
using MvcBlanket.ViewModels;

namespace Cash.Web.Models
{
    public class DisplayTreeTaskViewModel
    {
        public PagedViewModel<DisplayTaskViewModel> Tasks { get; set; }

        public Task Task { get; set; }
    }
}