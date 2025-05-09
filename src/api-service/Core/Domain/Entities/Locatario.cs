using System.Text.Json.Serialization;
using Domain.Enums;

namespace Domain.Entities;

public class Locatario
{
    public Locatario(int id, string nome, string email, string cpf, DateOnly dataNascimento, string numeroCnh, DateOnly dataEmissao, DateOnly dataValidade, ECategoriaCnh categoriaCnh, string fotoCnh)
    {
        Id = id;
        Nome = nome;
        Email = email;
        Cpf = cpf;
        DataNascimento = dataNascimento;
        NumeroCnh = numeroCnh;
        DataEmissao = dataEmissao;
        DataValidade = dataValidade;
        CategoriaCnh = categoriaCnh;
        FotoCnh = fotoCnh;
    }

    public Locatario(string nome, string email, string cpf, DateOnly dataNascimento, string numeroCnh, DateOnly dataEmissao, DateOnly dataValidade, ECategoriaCnh categoriaCnh, string fotoCnh)
    {
        Nome = nome;
        Email = email;
        Cpf = cpf;
        DataNascimento = dataNascimento;
        NumeroCnh = numeroCnh;
        DataEmissao = dataEmissao;
        DataValidade = dataValidade;
        CategoriaCnh = categoriaCnh;
        FotoCnh = fotoCnh;
    }

    public int Id { get; private set; }
    public string Nome { get; private set; } = "";
    public string Email { get; private set; } = "";
    public string Cpf { get; private set; } = "";
    public DateOnly DataNascimento { get; private set; }
    public string NumeroCnh { get; private set; } = "";
    public DateOnly DataEmissao { get; private set; }
    public DateOnly DataValidade { get; private set; }
    public ECategoriaCnh CategoriaCnh { get; private set; }
    public string FotoCnh { get; private set; } = "";
    [JsonIgnore]
    public Locacao Locacao { get; set; }

    public void AtualizaFotoCnh(string novaFotoCnh)
    {
        this.FotoCnh = novaFotoCnh;
    }

}