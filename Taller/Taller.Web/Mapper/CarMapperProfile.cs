using AutoMapper;
using Taller.Dto.Request;
using Taller.Dto.Response;
using Taller.Web.Models;

namespace Taller.Web.Mapper
{
    public class CarMapperProfile : Profile
    {
        public CarMapperProfile()
        {
            CreateMap<CarResponse, CarViewModel>();
        }
    }
}
