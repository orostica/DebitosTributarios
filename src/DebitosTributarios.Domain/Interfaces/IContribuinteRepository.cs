using DebitosTributarios.Domain.DTOs;
using DebitosTributarios.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DebitosTributarios.Domain.Interfaces
{
    public interface IContribuinteRepository
    {
        Task NovoContribuinte(ContribuinteNovoDto contribuinte);
        Task<bool> ContribuinteExiste(string cpf);
    }
}
