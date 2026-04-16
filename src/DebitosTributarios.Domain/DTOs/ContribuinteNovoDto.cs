using System;
using System.Collections.Generic;
using System.Text;

namespace DebitosTributarios.Domain.DTOs
{
    public record ContribuinteNovoDto
    {
        public int Nome { get; set; }
        public string CpfCnpj { get; set; }
    }
}
