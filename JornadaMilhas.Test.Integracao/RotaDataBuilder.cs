using Bogus;
using JornadaMilhasV1.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JornadaMilhas.Test.Integracao;

public class RotaDataBuilder : Faker<Rota>
{
    public string Origem { get; set; }
    public string Destino { get; set; }

    public RotaDataBuilder()
    {
        CustomInstantiator(f =>
        {
            var origem = Origem ?? f.Address.City();
            var destino = Destino ?? f.Address.City();
            return new Rota(origem, destino);
        });
    }

    public Rota Build() => Generate();
}
