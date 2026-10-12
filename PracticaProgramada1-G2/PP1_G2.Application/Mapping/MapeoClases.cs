using AutoMapper;
using PP1_G2.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using PP1_G2.Domain.Entidades;

namespace PP1_G2.Application.Mapping
{
    public class MapeoClases : Profile
    {
        public MapeoClases()
        {
            CreateMap<Cliente, ClienteDto>()
                .ForMember(destino => destino.Telefonos,
                    opciones => opciones.MapFrom(origen => origen.Telefonos.Select(telefono => telefono.Numero)));
            CreateMap<Cliente, ClienteDetalleDto>().IncludeBase<Cliente, ClienteDto>();
            CreateMap<Direcciones, DireccionesDto>().ReverseMap();
        }
    }
}
