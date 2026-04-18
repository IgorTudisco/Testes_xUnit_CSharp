namespace JornadaMilhas.Test.Integracao;

// Definindo a coleção de testes para compartilhar o contexto entre os testes
[CollectionDefinition(nameof(ContextoCollection))]
public class ContextoCollection : ICollectionFixture<ContextoFixture>
{
}
