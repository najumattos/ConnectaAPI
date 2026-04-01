using AutoMapper;
using Connectamente.API.DTOs;
using Connectamente.API.DTOs.UsersDTOs;
using Connectamente.API.Models;
using Connectamente.API.Usuario;

namespace Connectamente.API.Domain;

public class ConfigurationProfile : Profile
{
    public ConfigurationProfile()
    {
        //verificar isso aquiiiiidadnsadbab
        // Mapeamento nos dois sentidos (Entidade <-> DTO)
        CreateMap<ConsultaModel, ConsultaDto>().ReverseMap();
        CreateMap<UsuarioModel, UserDto>().ReverseMap();
        CreateMap<PsicologoDto, PsicologoDto>().ReverseMap();
        CreateMap<PacienteDto, PacienteDto>().ReverseMap();
        CreateMap<ProntuarioDto, ProntuarioDto>().ReverseMap();

    }
}