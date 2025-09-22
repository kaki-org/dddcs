// FullNameクラスのLastNameプロパティを利用する
var fullName = new FullName("teruo", "kakikubo");
Console.WriteLine(fullName.LastName); // kakikuboが表示される

// 確実に姓を表示できる
fullName = new FullName("john", "smith");
Console.WriteLine(fullName.LastName); // smithが表示される


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