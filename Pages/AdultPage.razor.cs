using System;
using System.Collections.Generic;
using System.Linq;
using LoginExample.Data.Model;
using LoginExample.Data.Persistence;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;

namespace LoginExample.Pages
{
public partial class AdultPage
    {
        [Parameter]
        public int? Id { get; set; }
        private IList<Adult> _adultList;
        private Adult _adultModel;

        [Inject] private NavigationManager _navigationManager { get; set; }
        [Inject] private IFileContext FileContextProvider { get; set; }

        public void HandleValidSubmit()
        {
            Console.WriteLine(_adultModel.FirstName);
        }

        protected override void OnInitialized()
        {
            _adultList = FileContextProvider.Adults;
            if (Id.HasValue)
            {
                _adultModel = DeepCopyAdult(_adultList.FirstOrDefault(adult => adult.Id == Id));

            }
            else
            {
                _adultModel = new Adult();
                //var lastEntry =_adultList.LastOrDefault();
                Adult targetAdult = null;
                foreach(var adult in _adultList)
                {
                    if(targetAdult == null)
                    {
                        targetAdult = adult;
                    }
                    else
                    {
                        if (adult.Id > targetAdult.Id)
                            targetAdult = adult;
                    }
                } 
                _adultModel.Id = targetAdult != null ? targetAdult.Id + 1 : 0;
            }
        }

        private static Adult DeepCopyAdult(Adult adult)
        {
            return JsonConvert.DeserializeObject<Adult>(JsonConvert.SerializeObject(adult));
        }

        private void Save()
        {
            if (!Id.HasValue)
            {
                _adultList.Add(_adultModel);
            }
            else
            {
                var existingAdult = _adultList.FirstOrDefault(x => x.Id == Id.Value);
                var index = _adultList.IndexOf(existingAdult);
                _adultList[index] = _adultModel;
            }
            FileContextProvider.SaveChanges();
            _navigationManager.NavigateTo($"/adults");
        }

        private void Cancel()
        {
            _navigationManager.NavigateTo($"/adults");
        }

    }


}