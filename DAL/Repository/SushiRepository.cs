using DAL.Context;
using DAL.Interfaces;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class SushiRepository: IRepository<Sushi>
    {
        private SushiContext _sushiContext;

        public SushiRepository()
        {
            _sushiContext = new SushiContext();
        }
        public void Create(Sushi item)
        {
            if (item != null)
            {
                _sushiContext.Sushi.Add(item);
                _sushiContext.SaveChanges();
            }
        }

        public void Delete(int? id)
        { 
            var tempSushi = _sushiContext.Sushi.Find(id);
            if (tempSushi != null)
            {
                _sushiContext.Sushi.Remove(tempSushi);
                _sushiContext.SaveChanges();
            }
        }

        public Sushi Find(int? id) => _sushiContext.Sushi.Find(id);
        public IEnumerable<Sushi> GetAll() => _sushiContext.Sushi;

        public void Update(Sushi item)
        {
            var tempSushi = _sushiContext.Sushi.Find(item.Id);
            tempSushi.Name = item.Name;
            tempSushi.Description = item.Description;
            tempSushi.MainPhoto = item.MainPhoto;
            tempSushi.Photo2 = item.Photo2;
            tempSushi.Price = item.Price;
            _sushiContext.SaveChanges();
        }
    }
}
