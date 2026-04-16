using DebitosTributarios.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace DebitosTributarios.Domain.Interfaces
{
    public interface IContribuinteApplicationService
    {
        Task NovoContribuinte(ContribuinteNovoDto contribuinte);
    }
}
