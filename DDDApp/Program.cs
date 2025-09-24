// FullNameクラスのLastNameプロパティを利用する
var fullName = new FullName("teruo", "kakikubo");
Console.WriteLine(fullName.LastName); // kakikuboが表示される

// 確実に姓を表示できる
fullName = new FullName("john", "smith");
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
var fullName2 = new FullName("teruo", "kakikubo");
Console.WriteLine(fullName2); // kakiが表示される


// 氏名を表現するFullNameクラス
class FullName
{
    public FullName(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName =  lastName;
    }
    
    public string FirstName { get; }
    public string LastName { get; }
}