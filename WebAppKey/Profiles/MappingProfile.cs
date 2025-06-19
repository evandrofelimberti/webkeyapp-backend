using AutoMapper;
using WebAppKey.DTO;
using WebAppKey.Models;
using WebAppKey.Types;

namespace WebAppKey.Profiles
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<Usuario, UsuarioDTO>().ReverseMap();
            CreateMap<Lavoura, LavouraDTO>().ReverseMap();
            
            CreateMap<Unidade, UnidadeDto>().ReverseMap();
            CreateMap<CreateUnidadeInput, Unidade>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<UpdateUnidadeInput, Unidade>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());            
            CreateMap<UnidadeDto, UnidadeType>().ReverseMap();
            CreateMap<Unidade, UnidadeType>().ReverseMap();
            CreateMap<UpdateUnidadeInput, UnidadeDto>().ReverseMap();
            CreateMap<CreateUnidadeInput, UnidadeDto>().ReverseMap();
            
        }
    }
}