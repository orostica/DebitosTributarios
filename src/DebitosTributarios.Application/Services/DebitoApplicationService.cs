using DebitosTributarios.Domain.DTOs;
using DebitosTributarios.Domain.Entities;
using DebitosTributarios.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace DebitosTributarios.Application.Services;

public class DebitoApplicationService : IDebitoApplicationService
{
    private readonly IDebitoRepository _debitoRepository;
    private readonly IContribuinteRepository _contribuinteRepository;
    private readonly ILogger<DebitoApplicationService> _logger;

    public DebitoApplicationService(
        IDebitoRepository debitoRepository,
        IContribuinteRepository contribuinteRepository,
        ILogger<DebitoApplicationService> logger)
    {
        _debitoRepository = debitoRepository;
        _contribuinteRepository = contribuinteRepository;
        _logger = logger;
    }

    public async Task<int> NovoDebitoAsync(DebitoNovoDto dto)
    {
        _logger.LogInformation("Iniciando cadastro de débito para contribuinte {ContribuinteId}.", dto.ContribuinteId);

        // Regra: o contribuinte deve existir para que o débito seja vinculado
        var contribuinte = await _contribuinteRepository.ObterPorIdAsync(dto.ContribuinteId);
        if (contribuinte is null)
        {
            _logger.LogWarning("Contribuinte {ContribuinteId} não encontrado ao cadastrar débito.", dto.ContribuinteId);
            throw new InvalidOperationException("Contribuinte não encontrado.");
        }

        // Criação via factory method valida valor > 0 e data de vencimento obrigatória
        var debito = Debito.Criar(dto.Valor, dto.DataVencimento, dto.ContribuinteId);

        await _debitoRepository.NovoDebitoAsync(debito);

        _logger.LogInformation("Débito {Id} cadastrado para contribuinte {ContribuinteId}.", debito.Id, dto.ContribuinteId);

        return debito.Id;
    }
}
