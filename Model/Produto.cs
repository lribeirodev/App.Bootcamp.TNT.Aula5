public class Produto
{
    // Atributos
    private string nome { get; set; }
    private int qtd {get;set;}
    // Construtor
    public Produto(string nome, int qtd)
    {
        this.nome = nome;
        this.qtd = qtd;
    }

    // Funções Auxiliares
    public override string ToString()
    {
        return $"qtd: {qtd} produto: {nome}";
    }
}