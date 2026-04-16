using DebitosTributarios.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace DebitosTributarios.Domain.Interfaces
{
    public interface IDebitoApplicationService
    {
        Task<int> NovoDebitoAsync(DebitoNovoDto dto);
    }
}
