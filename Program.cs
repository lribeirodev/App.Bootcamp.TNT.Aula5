using System.Collections;

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

public class Padaria
{  
    // Atributos
    private static ArrayList historicoPedido = new ArrayList();
    private static ArrayList produtosCliente = new ArrayList();
    private const string nomePadaria = "Padaria do seu Zé";
    private static string[] rawProdutos = {"Pão Francês","Sonho Doce de Leite","Cafézinho","Lanche"};

    
    // Ponto de Entrada da Aplicação
    public static void Main(string[] args)
    {  
        // Chama Função Principal
        run(nomePadaria);
    }

    // Roda o Sistema
    private static void run(string nomePadaria)
    {        
        String[] menuPrincipal = { "Novo Pedido", "Cancelar Pedido", "Historico de Vendas", "Sair do Sistema" };

        Console.WriteLine($"\nBem vindo ao Sistema da {nomePadaria} V1.0 beta");
        Console.WriteLine("Selecione uma Opção: ");

        // Retorna a Opção Selecionada
        int op = imprimirMenu(menuPrincipal);

        switch(op)
        {
            case 0: novoPedido(); break;
            case 1: 
                int opCancel = imprimirMenu(Utils<Pedido>.gerarArrayString(historicoPedido));
                Pedido pedido = (historicoPedido[opCancel] as Pedido)!;
                historicoPedido.RemoveAt(opCancel);
                Console.WriteLine($"Pedido Nº{pedido.getPedidoNumero()} Cancelado com Sucesso!");
                break;
            case 2: 
                string[] pedidos = Utils<Pedido>.gerarArrayString(historicoPedido);
                Console.WriteLine(pedidos.Length);
                foreach(string p in pedidos){
                    Console.WriteLine(p);
                }
                break;
            case 3: 
                Console.WriteLine("Obrigado por Utilizar nosso Sistema, Código feito por Lucas Ribeiro");
                Environment.Exit(0); 
                break;
        }

        run(nomePadaria);
               
    }

    // Funcões Auxiliares;
    private static int imprimirMenu(string[] menu){
        
        Console.WriteLine("");
        int cont = 1;
        foreach (string op in menu)
        {
            Console.WriteLine($"({cont}) {op}");
            cont++;
        }

        Console.WriteLine("Aguardando entrada: ");
        try{
            // Entrada do valor
            var x = Console.ReadLine();
            int op;

            // Sanitização e Checagem de Erros;

            // A String é Vazia se sim Retorne um erro
            if(String.IsNullOrEmpty(x)){
                throw new Exception("A entrada foi nula");
            }

            // É possivel converter o tipo para integer, se não retorne um erro
            bool success = int.TryParse(x,out op);
            if(!success) throw new Exception("O valor inserido não é um número");

            // O valor inserido é maior ou menor que o disponivel no menu
            if(op <= 0 || op > menu.Length) throw new Exception("O Valor inserido é maior ou menor que o disponivel no menu");

            return op-1;
        } catch(Exception e){
            Console.WriteLine("Atenção: ");
            Console.WriteLine($"Mensagem de Erro: {e.Message}\n");
            return imprimirMenu(menu);
        }
    }

    // Funções Principais
    private static void novoPedido(){
        
        Console.WriteLine("Menu Geração de Pedido: ");

        string[] menuPedido = {"Inserir Produto", "Listar Produto","Alterar Produto", "Excluir Produto", "Finalizar Pedido"};

        int op = imprimirMenu(menuPedido);

        switch(op)
        {
            case 0:
                produtosCliente.Add(inserirProduto());
                novoPedido();
                break;
            case 1:
                Console.WriteLine("Pedido atual: ");    
                foreach(Produto p in produtosCliente){
                    Console.WriteLine(p.ToString());
                }
                Console.WriteLine("");
                novoPedido();
                break;
            case 2:
                alterarProduto();
                break;
            case 3:
                excluirProduto();
                break;
            case 4:
                Pedido pedido = new Pedido((produtosCliente.ToArray(typeof(Produto)) as Produto[])!);
                
                historicoPedido.Add(pedido);
                
                Console.WriteLine(pedido.ToString());
                produtosCliente.Clear();
                run(nomePadaria);
                break;
        }
    }

    private static Produto inserirProduto(){

        Console.WriteLine("Escolha o Produto a ser inserido: ");
        int p = imprimirMenu(rawProdutos);
        
        Console.WriteLine("Digite a Quantidade: ");
        var x = Console.ReadLine();
        int qtd;
        
        try{
            bool success = int.TryParse(x, out qtd);
            if(!success) throw new Exception("Quantidade Inválida");
            return new Produto(rawProdutos[p],qtd);
        } catch (Exception e){
            Console.WriteLine($"Atenção: \nErro: {e.Message}");
            return inserirProduto();
        }

    }

    private static void alterarProduto(){
        
        Console.WriteLine("Selecione o produto a ser alterado: ");

        // gerarString de Produtos
        int op = imprimirMenu(Utils<Produto>.gerarArrayString(produtosCliente));
        
        Console.WriteLine("Selecione o novo produto: ");
        Produto p = inserirProduto();
        produtosCliente.RemoveAt(op);
        produtosCliente.Insert(op, p);

        Console.WriteLine("Produto alterado com sucesso");
        novoPedido();
    }

    private static void excluirProduto(){
        Console.WriteLine("Selecione o produto a ser excluido do pedido: ");
        // gerarString de Produtos
        int op = imprimirMenu(Utils<Produto>.gerarArrayString(produtosCliente));

        produtosCliente.RemoveAt(op);
        Console.WriteLine("Produto excluido com sucesso");
        novoPedido();
    }

    
}