using AutoMapper;
using Entities.Models;
using Shared.DataTransferObjects;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace AeroVendas.ULF.Services
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //CreateMap<LogAeroVendas, LogAeroVendasDto>()
            //.ForMember(c => c.Id,
            //opt => opt.MapFrom(x =>  x.Id))
            //.ForMember(c => c.Nome,
            //opt => opt.MapFrom(x => x.Nome))
            //.ForMember(c => c.Login,
            //opt => opt.MapFrom(x => x.AeroVendasId))
            //.ForMember(c => c.AeroVendasId,
            //opt => opt.MapFrom(x => x.Login))
            //.ForMember(c => c.NomeAeroVendas,
            //opt => opt.MapFrom(x => x.NomeAeroVendas))
            //.ForMember(c => c.DataLeitura,
            //opt => opt.MapFrom(x => x.DataLeitura));

            //CreateMap<LogAeroVendasForCreationDto, LogAeroVendas>();

            CreateMap<ViewContratoSemAeroVendas,ViewAeroVendasDto>();
            CreateMap<MensagemHtml, MensagemHtmlDto>();

			CreateMap<AeroSolicitacaoEmail, AeroSolicitacaoEmailDto>();
			CreateMap<AeroSolicitacaoEmailForCreationDto, AeroSolicitacaoEmail>();
			CreateMap<AeroSolicitacaoEmailForUpdateDto, AeroSolicitacaoEmailForUpdateDto>();

			CreateMap<AeroEnvioEmail, AeroEnvioEmailDto>();
			CreateMap<AeroEnvioEmailForCreationDto, AeroEnvioEmail>();
			CreateMap<AeroEnvioEmailForUpdateDto, AeroEnvioEmail>().ReverseMap();

			CreateMap<UserForRegistrationDto, User>();
			CreateMap<MensagemForUpdateDto, MensagemHtml>();
			CreateMap<MensagemHtmlForCreationDto, MensagemHtml>();
			CreateMap<Arquivo, FileUploadModel>();
			CreateMap<FileUploadModel, Arquivo>();
			CreateMap<Arquivo, ArquivoDto>(); 

		}

	}
}
