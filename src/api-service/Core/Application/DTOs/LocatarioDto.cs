using Domain.Entities;
using Domain.Enums;

namespace Application.DTOs;

public record NovoLocatarioDto(
    string Nome,
    string Email,
    string Cpf,
    DateOnly DataNascimento,
    string NumeroCnh,
    DateOnly DataEmissao,
    DateOnly DataValidade,
    ECategoriaCnh CategoriaCnh,
    string FotoCnh);

public record AtualizaFotoCnhLocatarioDto(
    string NovaFotoCnh
    );

public static class LocatarioDtoAdapter
{
    public static Locatario ConverterLocatarioDtoParEntidade(this NovoLocatarioDto novoLocatario)
    {
        return new Locatario(novoLocatario.Nome, novoLocatario.Email, novoLocatario.Cpf,novoLocatario.DataNascimento,novoLocatario.NumeroCnh,novoLocatario.DataEmissao,novoLocatario.DataValidade,novoLocatario.CategoriaCnh,novoLocatario.FotoCnh);
    }
}