using DebitosTributarios.Domain.DTOs;
using DebitosTributarios.Domain.Entities;
using DebitosTributarios.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace DebitosTributarios.Application.Services
{
    public class ContribuinteApplicationService : IContribuinteApplicationService
    {
        private readonly IContribuinteRepository _contribuinteRepository;
        private readonly IDebitoRepository _debitoRepository;
        private readonly ILogger<ContribuinteApplicationService> _logger;

        public ContribuinteApplicationService(
            IContribuinteRepository contribuinteRepository,
            IDebitoRepository debitoRepository,
            ILogger<ContribuinteApplicationService> logger)
        {
            _contribuinteRepository = contribuinteRepository;
            _debitoRepository = debitoRepository;
            _logger = logger;
        }

        public async Task<int> NovoContribuinteAsync(ContribuinteNovoDto dto)
        {
            _logger.LogInformation("Iniciando cadastro de contribuinte com CPF/CNPJ {CpfCnpj}.", dto.CpfCnpj);

            // Regra do CPF/CNPJ deve ser único e verifica antes de persistir
            var existe = await _contribuinteRepository.CpfCnpjExisteAsync(dto.CpfCnpj);
            if (existe)
            {
                _logger.LogWarning("CPF/CNPJ {CpfCnpj} já cadastrado.", dto.CpfCnpj);
                throw new InvalidOperationException("CPF/CNPJ já cadastrado.");
            }

            // Criação via factory method 
            var contribuinte = Contribuinte.Criar(dto.Nome, dto.CpfCnpj);

            await _contribuinteRepository.NovoContribuinteAsync(contribuinte);

            _logger.LogInformation("Contribuinte {Id} cadastrado com sucesso.", contribuinte.Id);

            return contribuinte.Id;
        }
        public async Task<ContribuinteDto?> ObterContribuinteAsync(int id)
        {
            _logger.LogInformation("Buscando resumo do contribuinte {Id}.", id);

            var contribuinte = await _contribuinteRepository.ObterPorIdAsync(id);
            if (contribuinte is null)
            {
                _logger.LogWarning("Contribuinte {Id} não encontrado.", id);
                return null;
            }

            // Busca todos os débitos do contribuinte para calcular os totais
            var debitos = await _debitoRepository.ObterPorContribuinteIdAsync(id);

            // Débitos em aberto: sem data de pagamento
            var totalEmAberto = debitos.Where(d => d.EstaAberto).Sum(d => d.Valor);

            // Débitos vencidos: em aberto e com vencimento no passado
            var debitosVencidos = debitos.Where(d => d.EstaVencido).ToList();

            return new ContribuinteDto(
                Id: contribuinte.Id,
                Nome: contribuinte.Nome,
                TotalDebitos: debitos.Count,
                TotalEmAberto: totalEmAberto,
                QuantidadeDebitosVencidos: debitosVencidos.Count,
                TotalVencido: debitosVencidos.Sum(d => d.Valor)
            );
        }
    }
}
