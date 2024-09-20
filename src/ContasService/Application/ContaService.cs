using ContasService.Application.DTOs;
using ContasService.Domain.Entities;
using ContasService.Domain.Exceptions;
using ContasService.Domain.Repositories;

namespace ContasService.Application.Services;

public class ContaService : IContaService
{
    private readonly HttpClient _httpClient;
    private readonly IContaRepository _contaRepository;

    public ContaService(HttpClient httpClient, IContaRepository contaRepository)
    {
        _httpClient = httpClient;
        _contaRepository = contaRepository;
    }

    public async Task<ContaBancaria> CriarContaAsync(ContaCriacaoDto contaCriacaoDto)
    {
        var clienteExiste = await VerificarSeClienteExiste(contaCriacaoDto.CpfCliente);
        if (!clienteExiste)
        {
            throw new OperacaoInvalidaException($"Cliente com CPF {contaCriacaoDto.CpfCliente} não encontrado.");
        }

        ContaBancaria novaConta;

        if (contaCriacaoDto.TipoConta == TipoConta.Corrente)
        {
            novaConta = new ContaCorrente(
                contaCriacaoDto.NumeroConta,
                contaCriacaoDto.NumeroAgencia,
                contaCriacaoDto.Limite ?? 0 // Se o limite for nulo, assume-se 0.
            );
        }
        else if (contaCriacaoDto.TipoConta == TipoConta.Poupanca)
        {
            novaConta = new ContaPoupanca(
                contaCriacaoDto.NumeroConta,
                contaCriacaoDto.NumeroAgencia
            );
        }
        else
        {
            throw new ArgumentException("Tipo de conta inválido.", nameof(contaCriacaoDto.TipoConta));
        }

        return await _contaRepository.CriarContaAsync(novaConta);
    }

    private async Task<bool> VerificarSeClienteExiste(string cpf)
    {
        var response = await _httpClient.GetAsync($"http://localhost:5099/api/v1/Clientes/{cpf}");
        return response.IsSuccessStatusCode;
    }

    public async Task<ContaBancaria> ObterPorNumeroContaAgenciaECpfAsync(string numeroConta, string numeroAgencia, string cpfCliente)
    {
        var clienteExiste = await VerificarSeClienteExiste(cpfCliente);
        if (!clienteExiste)
        {
            throw new OperacaoInvalidaException($"Cliente com CPF {cpfCliente} não encontrado.");
        }
        return await _contaRepository.ObterPorNumeroContaEAgenciaAsync(numeroConta, numeroAgencia);
    }

    public async Task<ContaBancaria> SacarAsync(string numeroConta, string numeroAgencia, decimal valor)
    {
        return await _contaRepository.SacarAsync(numeroConta, numeroAgencia, valor);
    }

    public async Task<ContaBancaria> DepositarAsync(string numeroConta, string numeroAgencia, decimal valor)
    {
        return await _contaRepository.DepositarAsync(numeroConta, numeroAgencia, valor);
    }

    public async Task<ContaCorrente> CalcularJurosAsync(string numeroConta, string numeroAgencia, decimal taxa)
    {
        return await _contaRepository.CalcularJurosAsync(numeroConta, numeroAgencia, taxa);
    }
}

