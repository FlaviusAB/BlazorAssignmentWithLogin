using System.Linq;
using LoginExample.Data.Model;
using LoginExample.Data.Persistence;
using Microsoft.AspNetCore.Components;

namespace LoginExample.Pages
{
    public partial class AdultViewPage
    {
        [Inject] private IFileContext FileContextProvider { get; set; }

        public Adult _adultModel;
        [Parameter]
        public int Id { get; set; }

        protected override void OnInitialized()
        {
            var _adultList = FileContextProvider.Adults;

            _adultModel = _adultList.FirstOrDefault(x => x.Id == Id);
   
            
        }
    }
}