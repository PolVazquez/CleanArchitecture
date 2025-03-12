using BusinessComponent;
using Microsoft.Extensions.DependencyInjection;
using RepositoryComponent;
using System.ComponentModel.DataAnnotations;

var container = new ServiceCollection()
        .AddSingleton<IRepository, BeerRepository>()
        .AddTransient<BeerManager>()
        .BuildServiceProvider();

var beerManager = container.GetService<BeerManager>() ?? throw new Exception();

beerManager.Add("Delirium Red");
beerManager.Add("London Porter");
Console.WriteLine(beerManager.Get());

// clase provisional
//public class DefaultRepository : IRepository
//{
//    public void Add(string name)
//    { }

//    public string Get()
//        => "algo";
//}