// FullNameクラスのLastNameプロパティを利用する

using Microsoft.Identity.Client;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.EntityFrameworkCore;

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
// ユーザ作成処理
partial class Program
{
    private IUserRepository userRepository;

    public Program(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }
    
    public void CreateUser(string userName)
    {
        var user = new User(
            new UserId(Guid.NewGuid().ToString()), 
            new UserName(userName)
        );
        
        var userService = new UserService(userRepository);
        if (userService.Exists(user))
        {
            throw new Exception($"{userName}は既に存在しています");
        }
        
        userRepository.Save(user);
    }
}

