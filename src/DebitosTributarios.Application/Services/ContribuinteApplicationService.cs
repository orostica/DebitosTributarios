using DebitosTributarios.Domain.DTOs;
using DebitosTributarios.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DebitosTributarios.Application.Services
{
    public class ContribuinteApplicationService : IContribuinteApplicationService
    {
        private readonly IContribuinteRepository contribuinteRepository;

        public ContribuinteApplicationService(IContribuinteRepository contribuinteRepository)
        {
            this.contribuinteRepository = contribuinteRepository;
        }

        public async Task NovoContribuinte(ContribuinteNovoDto contribuinte)
        {
            var hasContruibuinte = await contribuinteRepository.ContribuinteExiste(contribuinte.CpfCnpj);

            if(hasContruibuinte) throw new Exception("Contribuinte já existe");

            await contribuinteRepository.NovoContribuinte(contribuinte);
        }
    }
}
