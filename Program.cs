using System;
using System.Collections.Generic;

public class Estoque
{
    static List<Produto> estoque = new List<Produto>();

    static void Main()
    {
        while (true)
        {
            Console.WriteLine("Sistema de Gerenciamento de Estoque");
            Console.WriteLine("1. Adicionar Produto");
            Console.WriteLine("2. Atualizar Produto");
            Console.WriteLine("3. Remover Produto");
            Console.WriteLine("4. Buscar Produto");
            Console.WriteLine("5. Calcular Valor Total do Estoque");
            Console.WriteLine("6. Gerar Relatório de Produtos a Vencer");
            Console.WriteLine("7. Sair");

            Console.Write("Escolha uma opção: ");
            int escolha = int.Parse(Console.ReadLine());

            switch (escolha)
            {
                case 1:
                    AdicionarProduto();
                    break;
                case 2:
                    AtualizarProduto();
                    break;
                case 3:
                    RemoverProduto();
                    break;
                case 4:
                    BuscarProduto();
                    break;
                case 5:
                    CalcularValorTotal();
                    break;
                case 6:
                    GerarRelatorioProdutosAVencer();
                    break;
                case 7:
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Opção inválida. Tente novamente.");
                    break;
            }
        }
    }

    static void AdicionarProduto()
    {
        Console.Write("Nome do Produto: ");
        string nome = Console.ReadLine();

        Console.Write("Código de Barras: ");
        string codigoBarras = Console.ReadLine();

        Console.Write("Quantidade Disponível: ");
        int quantidade = int.Parse(Console.ReadLine());

        Console.Write("Preço Unitário: ");
        double preco = double.Parse(Console.ReadLine());

        Console.Write("Data de Validade (dd/mm/yyyy): ");
        DateTime dataValidade = DateTime.ParseExact(Console.ReadLine(), "dd/MM/yyyy", null);

        Produto novoProduto = new Produto(nome, codigoBarras, quantidade, preco, dataValidade);
        estoque.Add(novoProduto);

        Console.WriteLine("Produto adicionado com sucesso!");
    }

    static void AtualizarProduto()
    {
        Console.Write("Digite o Código de Barras do Produto a ser atualizado: ");
        string codigoBarras = Console.ReadLine();

        Produto produto = estoque.Find(p => p.CodigoBarras == codigoBarras);

        if (produto != null)
        {
            Console.Write("Nova Quantidade Disponível: ");
            int quantidade = int.Parse(Console.ReadLine());

            produto.Quantidade = quantidade;
            Console.WriteLine("Produto atualizado com sucesso!");
        }
        else
        {
            Console.WriteLine("Produto não encontrado.");
        }
    }

    static void RemoverProduto()
    {
        Console.Write("Digite o Código de Barras do Produto a ser removido: ");
        string codigoBarras = Console.ReadLine();

        Produto produto = estoque.Find(p => p.CodigoBarras == codigoBarras);

        if (produto != null)
        {
            estoque.Remove(produto);
            Console.WriteLine("Produto removido com sucesso!");
        }
        else
        {
            Console.WriteLine("Produto não encontrado.");
        }
    }

    static void BuscarProduto()
    {
        Console.WriteLine("Opções de busca:");
        Console.WriteLine("1. Por Nome");
        Console.WriteLine("2. Por Código de Barras");
        Console.WriteLine("3. Por Data de Validade");
        Console.Write("Escolha uma opção: ");

        int escolha = int.Parse(Console.ReadLine());

        switch (escolha)
        {
            case 1:
                Console.Write("Digite o nome do produto: ");
                string nome = Console.ReadLine();
                var produtosPorNome = estoque.FindAll(p => p.Nome.ToLower().Contains(nome.ToLower()));
                MostrarProdutosEncontrados(produtosPorNome);
                break;
            case 2:
                Console.Write("Digite o código de barras do produto: ");
                string codigoBarras = Console.ReadLine();
                var produtoPorCodigoBarras = estoque.Find(p => p.CodigoBarras == codigoBarras);
                if (produtoPorCodigoBarras != null)
                {
                    Console.WriteLine("Produto encontrado:");
                    Console.WriteLine(produtoPorCodigoBarras);
                }
                else
                {
                    Console.WriteLine("Produto não encontrado.");
                }
                break;
            case 3:
                Console.Write("Digite a data de validade (dd/mm/yyyy): ");
                DateTime dataValidade = DateTime.ParseExact(Console.ReadLine(), "dd/MM/yyyy", null);
                var produtosPorDataValidade = estoque.FindAll(p => p.DataValidade.Date == dataValidade.Date);
                MostrarProdutosEncontrados(produtosPorDataValidade);
                break;
            default:
                Console.WriteLine("Opção inválida.");
                break;
        }
    }

    static void MostrarProdutosEncontrados(List<Produto> produtos)
    {
        if (produtos.Count > 0)
        {
            Console.WriteLine("Produtos encontrados:");
            foreach (var produto in produtos)
            {
                Console.WriteLine(produto);
            }
        }
        else
        {
            Console.WriteLine("Nenhum produto encontrado.");
        }
    }

    static void CalcularValorTotal()
    {
        double valorTotal = 0;
        foreach (var produto in estoque)
        {
            valorTotal += produto.Quantidade * produto.Preco;
        }
        Console.WriteLine($"Valor Total do Estoque: R$ {valorTotal:F2}");
    }

    static void GerarRelatorioProdutosAVencer()
    {
        Console.Write("Digite o número de dias para vencimento: ");
        int diasParaVencer = int.Parse(Console.ReadLine());
        DateTime dataAtual = DateTime.Now;
        DateTime dataVencimentoLimite = dataAtual.AddDays(diasParaVencer);

        var produtosAVencer = estoque.FindAll(p => p.DataValidade <= dataVencimentoLimite);
        MostrarProdutosEncontrados(produtosAVencer);
    }
}

class Produto
{
    public string Nome { get; set; }
    public string CodigoBarras { get; set; }
    public int Quantidade { get; set; }
    public double Preco { get; set; }
    public DateTime DataValidade { get; set; }

    public Produto(string nome, string codigoBarras, int quantidade, double preco, DateTime dataValidade)
    {
        Nome = nome;
        CodigoBarras = codigoBarras;
        Quantidade = quantidade;
        Preco = preco;
        DataValidade = dataValidade;
    }

    public override string ToString()
    {
        return $"Nome: {Nome}, Código de Barras: {CodigoBarras}, Quantidade: {Quantidade}, Preço: {Preco:C}, Data de Validade: {DataValidade:dd/MM/yyyy}";
    }
}
