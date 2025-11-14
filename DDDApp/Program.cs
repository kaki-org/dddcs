// FullNameクラスのLastNameプロパティを利用する

using Microsoft.Identity.Client;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using static Program;

// テストコード（コメントアウト）
/*
var fullName = new FullName("teruo", "kakikubo", "");
Console.WriteLine(fullName.LastName); // kakikuboが表示される

// 確実に姓を表示できる
fullName = new FullName("john", "smith", "thomas");
Console.WriteLine(fullName.LastName); // smithが表示される

// 数字の変更
var num = 0;
num = 1;
// 文字の変更
var c = '0';
c = 'b';
// 文字列の変更
var greet = "こんにちは";
Console.WriteLine(greet); // こんにちは　が表示される
greet = "hello";
Console.WriteLine(greet); // hello が表示される
// 値オブジェクトの変更
var fullName2 = new FullName("teruo", "kakikubo", "");
Console.WriteLine(fullName2); // kakikuboが表示される

// 同じ種類の値同士の比較
Console.WriteLine(0 == 0); // True
Console.WriteLine(0 == 1); // False
Console.WriteLine('a' == 'a'); // True
Console.WriteLine('a' == 'b'); // False
Console.WriteLine("hello" == "hello"); // True
Console.WriteLine("hello" == "こんにちは"); // False
// 値オブジェクト同士の比較
var nameA = new FullName("John", "Smith", "Thomas");
var nameB = new FullName("John", "Smith", "Thomas");
// 別個のインスタンス同士の比較
Console.WriteLine("nameAとnameBのEqualsの比較" + nameA.Equals(nameB)); // Trueになる
// 演算子のオーバーライド機能を活用することも選択肢に入る
var compareResult2 = nameA == nameB;
Console.WriteLine(compareResult2);

// 通貨クラスを作成して加算処理を実施する
var myMoney = new Money(1000, "JPY");
var allowance = new Money(3000, "JPY");
Console.WriteLine(myMoney.Add(allowance));
// プリミティブな値同士の計算
var myMoneyP = 1000m;
var allowanceP = 3000m;
Console.WriteLine(myMoneyP + allowanceP);
// 異なる通貨単位では例外を送出
// var usd = new Money(10, "USD");
// Console.WriteLine(usd.Add(allowance));

// 製品番号を格納して出力してみる
var modelNumber = new ModelNumber("a20421", "100", "1");
Console.WriteLine(modelNumber);

// ユーザ名をルールに則って設定する
var validUserName= new UserName("kakikubo");
Console.WriteLine(validUserName);
// var invalidUserName = new UserName("tk"); // Exceptionを吐く


// リポジトリをProgramクラスに引き渡す
// var userRepository = new UserRepository();
// var program = new Program(userRepository);
// program.CreateUser("kakikubo");

// ユーザ作成処理をテストする
// var userRepository = new InMemoryUserRepository();
// var program = new Program(userRepository);
// program.CreateUser("kkkb");

// データを取り出して確認
// var head = userRepository.Store.Values.First();
// Assert.AreEqual("kkkb", head.Name.Value);

// EntiryFrameworkを利用したリポジトリをつかったテスト
var options = new DbContextOptionsBuilder<MyDbContext>()
    .UseInMemoryDatabase(databaseName: "TestDatabase")
    .Options;
var myContext = new MyDbContext(options);
var userRepository = new EFUserRepository(myContext);
var program = new Program(userRepository);
program.CreateUser("kakikubo");

// データを取り出して確認
var head = myContext.Users.First();
Assert.AreEqual("kakikubo", head.Name);

var userService = new UserService(userRepository);
var userApplicationService = new UserApplicationService(userRepository, userService);
// ユーザ名変更だけを行うように
var updateNameCommand = new UserUpdateCommand(head.Id)
{
    Name = "kkkb"
};
userApplicationService.Update(updateNameCommand);

// メールアドレス変更だけを行うように
var updateMailAddressCommand = new UserUpdateCommand(head.Id)
{
    MailAddress = "kakikubo@example.com"
};
userApplicationService.Update(updateMailAddressCommand);


// ユーザ登録処理でインターフェースを利用
var userRegisterService = new UserRegisterService(userRepository, userService);
var command = new UserRegisterCommand("teruo");
userRegisterService.Handle(command);

// clientを利用する
var client = new Client(userRegisterService);
client.Register("kkkb2");

// わざとExceptionを起こすExceptionUserRegisterServiceを利用する
var exceptionUserRegisterServcie = new ExceptionUserRegisterService();
var exceptionClient = new Client(exceptionUserRegisterServcie);
// exceptionClient.Register("kkkb3"); // throwされる
*/

// Mainメソッドを呼び出してユーザー入力を開始
Program.Main(args);

// ユーザ作成処理
partial class Program
{
    private IUserRepository userRepository;
    private static ServiceProvider serviceProvider;

    public static void Main(string[] args)
    {
        StartUp();

        while (true)
        {
            Console.WriteLine("Input user name");
            Console.Write(">");
            var input = Console.ReadLine();

            // null check for input
            if (string.IsNullOrEmpty(input))
            {
                Console.WriteLine("Invalid input. Please try again.");
                continue;
            }

            var userRegisterService = serviceProvider.GetService<UserRegisterService>();
            var command = new UserRegisterCommand(input);
            userRegisterService.Handle(command);

            Console.WriteLine("--------------------------------------------");
            Console.WriteLine("user created:");
            Console.WriteLine("--------------------------------------------");
            Console.WriteLine("user name:");
            Console.WriteLine("- " + input);
            Console.WriteLine("--------------------------------------------");

            Console.WriteLine("continue? (y/n)");
            Console.Write(">");
            var yesOrNo = Console.ReadLine();
            if (yesOrNo == "n")
            {
                break;
            }
        }
    }

    public static void StartUp()
    {
        // IoC Container
        var serviceCollection = new ServiceCollection();
        // 依存関係の登録を行う(以下コメントにて補足)
        // IUserRepositoryが要求されたらInMemoryUserRepositoryを生成して引き渡す(生成したインスタンスはその後使い回される)
        serviceCollection.AddSingleton<IUserRepository, InMemoryUserRepository>(); // UserRepositoryに差し替え
        // serviceCollection.AddTransient<IUserRepository, UserRepository>();
        // UserRegisterServiceがUserServiceから要求されるので登録しておく
        serviceCollection.AddTransient<UserService>();
        // UserRegisterServiceが要求されたら都度UserServiceを生成して引き渡す
        serviceCollection.AddTransient<UserRegisterService>();
        // UserApplicationServiceが要求されたら都度UserApplicationServiceを生成して引き渡す
        serviceCollection.AddTransient<UserApplicationService>();
        // 依存解決を行うプロバイダの生成
        // プログラムはserviceProviderに依存の解決を依頼する
        serviceProvider = serviceCollection.BuildServiceProvider();
    }

    public Program(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }

    public void CreateUser(string userName)
    {
        var user = new User(
            new UserId(Guid.NewGuid().ToString()),
            new UserName(userName),
            new MailAddress("program@example.com")
        );

        var userService = new UserService(userRepository);
        if (userService.Exists(user))
        {
            throw new Exception($"{userName}は既に存在しています");
        }

        userRepository.Save(user);
    }
}

public class Client
{
    private IUserRegisterService userRegisterService;
    public Client(IUserRegisterService userRegisterService)
    {
        this.userRegisterService = userRegisterService;
    }

    public void Register(string name)
    {
        var command = new UserRegisterCommand(name);
        userRegisterService.Handle(command);
    }
}