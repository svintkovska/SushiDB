using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using DAL.Models;
using DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class SushiService : IService<SushiDTO>
    {
        private readonly SushiRepository _sushiRepository;
        public SushiService()
        {
            _sushiRepository = new SushiRepository();
        }
        public void Create(SushiDTO item)
        {
            if (item != null)
            {
                _sushiRepository.Create(TranslateSushiDTOToSushi(item));
            }
        }

        public void Delete(int? id)
        {
            if (id != null)
            {
                _sushiRepository.Delete(id);
            }
        }

        public SushiDTO Find(int? id)
        {
            if (id != null)
                return TranslateSushiToSushiDTO(_sushiRepository.Find(id));
            return null;
        }

        public IList<SushiDTO> GetAll()
        {
            var list = new List<SushiDTO>();
            foreach (var s in _sushiRepository.GetAll())
            {
                list.Add(TranslateSushiToSushiDTO(s));
            }
            return list;
        }

        public void Update(SushiDTO item)
        {
            if (item != null)
            {
                _sushiRepository.Update(TranslateSushiDTOToSushi(item));
            }
        }

        private Sushi TranslateSushiDTOToSushi(SushiDTO sushiDTO)
        {
            var translateObj = new MapperConfiguration(map => map.CreateMap<SushiDTO, Sushi>()).CreateMapper();
            if (sushiDTO != null)
                return new Sushi() { Id = sushiDTO.Id, 
                    Name = sushiDTO.Name, 
                    Description = sushiDTO.Description, 
                    MainPhoto = sushiDTO.MainPhoto, 
                    Photo2 = sushiDTO.Photo2, 
                    Photo3 = sushiDTO.Photo3, 
                    Price = sushiDTO.Price };
            return null;
        }
        private SushiDTO TranslateSushiToSushiDTO(Sushi sushi)
        {
            var translateObj = new MapperConfiguration(map => map.CreateMap<Sushi, SushiDTO>()).CreateMapper();

            if (sushi != null)
                return new SushiDTO() { Id = sushi.Id,
                    Name = sushi.Name,
                    Description = sushi.Description,
                    MainPhoto = sushi.MainPhoto,
                    Photo2 = sushi.Photo2,
                    Photo3 = sushi.Photo3,
                    Price = sushi.Price
                };
            return null;
        }
    }
}
