using Microsoft.Extensions.DependencyInjection;

namespace SnsDomain
{
    class Program
    {
        static void Main(string[] args)
        {
            // DIコンテナの設定
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            var serviceProvider = serviceCollection.BuildServiceProvider();

            Console.WriteLine("Hello World from SnsDomain!");
            Console.WriteLine("SNSアプリケーションの開発を開始します。");
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            // 今後、サービスの登録をここに追加します
        }
    }
}