using Bogus;
using JornadaMilhasV1.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JornadaMilhas.Test.Integracao;

// Seguindo o padrão Data Builder para criar objetos do tipo Periodo com dados personalizados para os testes de integração, garantindo flexibilidade na criação de períodos com datas específicas ou aleatórias conforme necessário.
public class PeriodoDataBuilder : Faker<Periodo>
{
    public DateTime? DataInicial { get; set; }
    public DateTime? DataFinal { get; set; }

    public PeriodoDataBuilder()
    {
        CustomInstantiator(f =>
        {
            DateTime dataInicio = DataInicial ?? f.Date.Soon();
            DateTime dataFinal = DataFinal ?? f.Date.Soon().AddDays(30);
            return new Periodo(dataInicio, dataFinal);
        });
    }

    public Periodo Build() => Generate();

}
