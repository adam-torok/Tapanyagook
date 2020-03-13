using System;
using System.Collections.Generic;
using System.Linq;
using Tapanyagook.Models;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Tapanyagook.Repositories
{
    class TapanyagRepository : IDisposable
    {
        private tapanyagEntities db = new tapanyagEntities();
        private int _totalItems;

        public BindingList<tapanyag> getAllTapanyag(int page = 0, int itemsPerPage = 0, string search = null, string sortBy = null, bool ascending = true)
        {
            var query = db.tapanyag.OrderBy(x => x.id).AsQueryable();
            if (!string.IsNullOrWhiteSpace(search))
            {
                search = search.Replace('.', ',').ToLower();
                decimal szamertek;
                decimal.TryParse(search, out szamertek);

                if (szamertek > 0)
                {
                    query = query.Where(x => x.energia == szamertek || x.feherje == szamertek || x.szenhidrat == szamertek || x.zsir == szamertek);
                }
                else
                {
                    query = query.Where(x => x.nev.Contains(search));
                }
            }
            if (!string.IsNullOrEmpty(sortBy))
            {
                switch (sortBy)
                {
                    default:
                        query = ascending ? query.OrderBy(x => x.id) : query.OrderByDescending(x => x.id);
                        break;
                    case "nev":
                        query = ascending ? query.OrderBy(x => x.nev) : query.OrderByDescending(x => x.nev);
                        break;
                    case "energia":
                        query = ascending ? query.OrderBy(x => x.energia) : query.OrderByDescending(x => x.energia);
                        break;
                    case "feherje":
                        query = ascending ? query.OrderBy(x => x.feherje) : query.OrderByDescending(x => x.feherje);
                        break;
                    case "zsir":
                        query = ascending ? query.OrderBy(x => x.zsir) : query.OrderByDescending(x => x.zsir);
                        break;
                    case "szenhidrat":
                        query = ascending ? query.OrderBy(x => x.szenhidrat) : query.OrderByDescending(x => x.szenhidrat);
                        break;
                }
            }
            _totalItems = query.Count();

            if (page + itemsPerPage > 0)
            {
                query = query.Skip((page - 1) * itemsPerPage).Take(itemsPerPage);
            }

            return new BindingList<tapanyag>(query.ToList());
        }

        public int Count()
        {
            return _totalItems;
        }

        public void Insert(tapanyag etel)
        {
            db.tapanyag.Add(etel);
        }

        public void Delete(int id)
        {
            var etel = db.tapanyag.Find(id);
            db.tapanyag.Remove(etel);
        }

        public void Update(tapanyag param)
        {
            var jk = db.tapanyag.Find(param.id);
            if(jk!= null)
            {
                db.Entry(jk).CurrentValues.SetValues(param);
            }
        }

        public bool Exists(tapanyag tapanyag)
        {
            return db.tapanyag.Any(x => x.id == tapanyag.id);
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
        }
        public tapanyag GetTapanyag(string nev)
        {
            return db.tapanyag.SingleOrDefault(x => x.nev == nev);
        }
    }
}
