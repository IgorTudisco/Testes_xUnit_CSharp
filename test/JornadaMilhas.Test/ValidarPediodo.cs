using JornadaMilhasV1.Modelos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace JornadaMilhas.Test;

public class ValidarPediodo
{

    [Theory]
    [InlineData("2024-01-15", "2024-02-25")]
    [InlineData("2024-01-15", "2024-01-15")]
    public void RetornaPediodoEhValido(string dataInicial, string dataFinal)
    {
        Periodo periodo = new Periodo(DateTime.Parse(dataInicial), DateTime.Parse(dataFinal));

        var resultado = true;

        Assert.Equal(resultado, periodo.EhValido);
    }

    [Fact]
    public void RetornaMensagemDeErroQuandoPediodoEhInvalido()
    {
        Periodo periodo = new Periodo(new DateTime(2026, 02, 15), new DateTime(2024, 01, 20));

        var resultado = "Data de ida não pode ser maior que a data de volta";

        Assert.Contains(resultado, periodo.Erros.Sumario);
    }
}
