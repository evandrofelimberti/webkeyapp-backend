using AutoMapper;
using WebAppKey.DTO;
using WebAppKey.Models;

namespace WebAppKey.Profiles
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<Usuario, UsuarioDTO>().ReverseMap();
            CreateMap<Lavoura, LavouraDTO>().ReverseMap();
        }
    }
}