// FullNameクラスのLastNameプロパティを利用する
using System.Text.RegularExpressions;

var teruo = "teruo";
var kakikubo = "kakikubo";
var fullName = new FullName(teruo, kakikubo, "");
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


// 氏名を表現するFullNameクラス
class FullName : IEquatable<FullName>
{
    private readonly string firstName;
    private readonly string lastName;
    private readonly string middleName;

    public string FirstName => firstName;
    public string LastName => lastName;
    public string MiddleName => middleName;

    public FullName(string firstName, string lastName, string middleName)
    {
        if (firstName == null) throw new ArgumentNullException(nameof(firstName));
        if (lastName == null) throw new ArgumentNullException(nameof(lastName));
        if (!ValidateName(firstName)) throw new ArgumentException("許可されていない文字が使われています。", nameof(firstName));
        if (!ValidateName(lastName)) throw new ArgumentException("許可されていない文字が使われています。", nameof(lastName));
        this.firstName = firstName;
        this.lastName = lastName;
        this.middleName = middleName;
    }

    private bool ValidateName(string value)
    {
        // アルファベットに限定する
        return Regex.IsMatch(value, @"^[a-zA-Z]+$");
    }

    public bool Equals(FullName other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return firstName.Equals(other.firstName) && lastName.Equals(other.lastName) &&
               string.Equals(middleName, other.middleName);
    }

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((FullName)obj);
    }

    // C#ではEqualsをoverrideする際にGetHashCodeをoverrideするルールがある
    public override int GetHashCode()
    {
        unchecked
        {
            return ((firstName != null ? firstName.GetHashCode() : 0) * 397) ^
                   (lastName != null ? lastName.GetHashCode() : 0);
        }
    }
}