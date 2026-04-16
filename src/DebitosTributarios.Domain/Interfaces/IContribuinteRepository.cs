using DebitosTributarios.Domain.DTOs;
using DebitosTributarios.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DebitosTributarios.Domain.Interfaces
{
    public interface IContribuinteRepository
    {
        Task NovoContribuinteAsync(ContribuinteNovoDto contribuinte);
        Task<bool> CpfCnpjExisteAsync(string cpfCnpj);
        Task<Contribuinte?> ObterPorIdAsync(int id);
    }
}
