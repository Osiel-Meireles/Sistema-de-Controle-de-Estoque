using System;
using System.Collections.Generic;
using System.Globalization;

namespace ExerciciosCsharp
{
    internal class Program
    {
        public class Produto
        {
            public string Nome { get; set; }
            public decimal Preco { get; set; }
            public int Quantidade { get; set; }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Bem-vindo ao assistente de cadastro de produtos!");

            var produtos = new List<Produto>();
            int opcao;

            do
            {
                Console.WriteLine("\nSelecione uma opção:");
                Console.WriteLine("1 - Adicionar produto");
                Console.WriteLine("2 - Exibir produtos");
                Console.WriteLine("3 - Editar produto");
                Console.WriteLine("4 - Excluir produto");
                Console.WriteLine("0 - Sair");
                Console.Write("Opção: ");

                if (!int.TryParse(Console.ReadLine(), out opcao))
                {
                    Console.WriteLine("Por favor, digite uma opção válida.");
                    continue;
                }

                switch (opcao)
                {
                    case 1:
                        AdicionarProduto(produtos);
                        break;

                    case 2:
                        ExibirProdutos(produtos);
                        break;

                    case 3:
                        EditarProduto(produtos);
                        break;

                    case 4:
                        ExcluirProduto(produtos);
                        break;

                    case 0:
                        Console.WriteLine("Saindo do programa...");
                        break;

                    default:
                        Console.WriteLine("Opção inválida. Tente novamente.");
                        break;
                }
            } while (opcao != 0);
        }

        static void AdicionarProduto(List<Produto> produtos)
        {
            Console.Write("Digite o nome do produto: ");
            string nome = Console.ReadLine();

            Console.Write("Digite o preço do produto: ");
            if (!decimal.TryParse(Console.ReadLine(), NumberStyles.Currency, CultureInfo.InvariantCulture, out decimal preco))
            {
                Console.WriteLine("Preço inválido.");
                return;
            }

            Console.Write("Digite a quantidade do produto: ");
            if (!int.TryParse(Console.ReadLine(), out int quantidade))
            {
                Console.WriteLine("Quantidade inválida.");
                return;
            }

            produtos.Add(new Produto { Nome = nome, Preco = preco, Quantidade = quantidade });
            Console.WriteLine("Produto adicionado com sucesso!");
        }

        static void ExibirProdutos(List<Produto> produtos)
        {
            if (produtos.Count == 0)
            {
                Console.WriteLine("Nenhum produto cadastrado.");
                return;
            }

            Console.WriteLine("\nProdutos cadastrados:");
            for (int i = 0; i < produtos.Count; i++)
            {
                var produto = produtos[i];
                decimal valorTotal = produto.Preco * produto.Quantidade;

                Console.WriteLine($"{i + 1}. Nome: {produto.Nome}");
                Console.WriteLine($"   Preço Unitário: {produto.Preco:C}");
                Console.WriteLine($"   Quantidade em Estoque: {produto.Quantidade}");
                Console.WriteLine($"   Valor Total no Estoque: {valorTotal:C}");
                Console.WriteLine("----------------------------------------");
            }
        }


        static void EditarProduto(List<Produto> produtos)
        {
            if (produtos.Count == 0)
            {
                Console.WriteLine("Nenhum produto cadastrado para editar.");
                return;
            }

            ExibirProdutos(produtos);
            Console.Write("Digite o número do produto que deseja editar: ");
            if (!int.TryParse(Console.ReadLine(), out int index) || index < 1 || index > produtos.Count)
            {
                Console.WriteLine("Produto inválido.");
                return;
            }

            var produto = produtos[index - 1];

            Console.Write("Digite o novo nome do produto (ou Enter para manter o atual): ");
            string nome = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(nome)) produto.Nome = nome;

            Console.Write("Digite o novo preço do produto (ou Enter para manter o atual): ");
            string precoInput = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(precoInput) && decimal.TryParse(precoInput, NumberStyles.Currency, CultureInfo.InvariantCulture, out decimal preco))
            {
                produto.Preco = preco;
            }

            Console.Write("Digite a nova quantidade do produto (ou Enter para manter o atual): ");
            string quantidadeInput = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(quantidadeInput) && int.TryParse(quantidadeInput, out int quantidade))
            {
                produto.Quantidade = quantidade;
            }

            Console.WriteLine("Produto editado com sucesso!");
        }

        static void ExcluirProduto(List<Produto> produtos)
        {
            if (produtos.Count == 0)
            {
                Console.WriteLine("Nenhum produto cadastrado para excluir.");
                return;
            }

            ExibirProdutos(produtos);
            Console.Write("Digite o número do produto que deseja excluir: ");
            if (!int.TryParse(Console.ReadLine(), out int index) || index < 1 || index > produtos.Count)
            {
                Console.WriteLine("Produto inválido.");
                return;
            }

            produtos.RemoveAt(index - 1);
            Console.WriteLine("Produto excluído com sucesso!");
        }
    }
}
