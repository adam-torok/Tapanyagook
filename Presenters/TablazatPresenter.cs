using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tapanyagook.Repositories;
using Tapanyagook.Models;
using Tapanyagook.ViewInterfaces;

namespace Tapanyagook.Presenters
{
    class TablazatPresenter
    {
        ITablazatView view;
        TapanyagRepository repo = new TapanyagRepository();

        public TablazatPresenter(ITablazatView para)
        {
            view = para;
        }

        public void LoadData()
        {
            using(var jkrepo = new TapanyagRepository())
            {
                view.bindingList = repo.getAllTapanyag(view.pageNumber, view.itemsPerPage, view.search, view.sortBy, view.ascending);
            }
        }
        public void Add(tapanyag tapanyag)
        {
            if (repo.Exists(tapanyag))
            {
                try
                {
                    repo.Update(tapanyag);
                }
                catch(Exception e)
                {
                }
            }
        }

        public void Remove(int index)
        {
            var jk = view.bindingList.ElementAt(index);
            view.bindingList.RemoveAt(index);
            if(jk.id > 0)
            {
                repo.Delete(jk.id);
            }
        }

        public void Modify(tapanyag tapanyag, int id)
        {
            view.bindingList.RemoveAt(id);
            view.bindingList.Insert(id, tapanyag);
            repo.Update(tapanyag);
        }

        public void Save() {
            repo.Save();
        }

    }
}
