using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LoginExample.Data.Model;
using LoginExample.Data.Persistence;
using Microsoft.AspNetCore.Components;

namespace LoginExample.Pages
{
    public partial class SearchAdults
    {

        [Inject] private IFileContext FileContextProvider { get; set; }
        
        [Inject] private NavigationManager _navigationManager { get; set; }

        private string _searchInputVal = "";
        private bool _val = true;
        private IList<Adult> _adultList;
        private IList<Adult> _adultListOriginal;

        protected override void OnInitialized()
        {
            _adultList = FileContextProvider.Adults;
            _adultListOriginal = _adultList;
        }

        public void Edit(int id)
        {
            _navigationManager.NavigateTo($"adult/{id}");
        }
        public void View(int id)
        {
            _navigationManager.NavigateTo($"adultView/{id}");
        }

        public void Add() {
            _navigationManager.NavigateTo($"adult");
        }
        public void Delete(int id)
        {
            _adultList.Remove(_adultList.FirstOrDefault(x => x.Id == id));
            FileContextProvider.SaveChanges();

        }
        private async Task Search()
        {
            var lowerCaseFieldValue = _searchInputVal.ToLower();
            _adultList = _adultListOriginal.Where(adult => adult.FirstName.ToLower().Contains(lowerCaseFieldValue)
                                                           || adult.LastName.ToLower().Contains(lowerCaseFieldValue)).ToList();
            await InvokeAsync(() => StateHasChanged())
                .ConfigureAwait(false);
        }
        

    }
}