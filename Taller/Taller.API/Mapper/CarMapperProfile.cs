using AutoMapper;
using Taller.Domain;
using Taller.Dto.Request;
using Taller.Dto.Response;

namespace Taller.API.Mapper
{
    public class CarMapperProfile : Profile
    {
        public CarMapperProfile()
        {
            CreateMap<Car, CarResponse>();
            CreateMap<CreateCarRequest, Car>();
        }
    }
}
