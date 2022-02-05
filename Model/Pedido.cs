public class Pedido{

    private Random r = new Random();
    private int numPedido {get; set;}
    private Produto[] produto {get; set;}

    public Pedido(Produto[] produto)
    {   
        this.produto = produto;
        this.numPedido = r.Next(9000) * produto.Length;
    }

    public int getPedidoNumero(){
        return this.numPedido;
    }

    // Criando Array que Imprime os Produtos
    public override string ToString()
    {  
        string listaProdutos = "";
        foreach(Produto item in this.produto){
            listaProdutos += item.ToString() + "\n";
        }
        return $"\n=============\nPedido Nº [{this.numPedido}]: \nLista de Produtos\n{listaProdutos} \n=============\n";
    }

}