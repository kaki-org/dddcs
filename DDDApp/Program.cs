// FullNameクラスのLastNameプロパティを利用する
var teruo = new FirstName("teruo");
var kakikubo = new LastName("kakikubo");
var fullName = new FullName(teruo, kakikubo, "");
Console.WriteLine(fullName.LastName); // kakikuboが表示される

// 確実に姓を表示できる
fullName = new FullName(new FirstName("john"), new LastName("smith"), "thomas");
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
var fullName2 = new FullName(new FirstName("teruo"), new LastName("kakikubo"), "");
Console.WriteLine(fullName2); // kakiが表示される

// 同じ種類の値同士の比較
Console.WriteLine(0 == 0); // True
Console.WriteLine(0 == 1); // False
Console.WriteLine('a' == 'a'); // True
Console.WriteLine('a' == 'b'); // False
Console.WriteLine("hello" == "hello"); // True
Console.WriteLine("hello" == "こんにちは"); // False
// 値オブジェクト同士の比較
var nameA = new FullName(new FirstName("John"), new LastName("Smith"), "Thomas");
var nameB = new FullName(new FirstName("John"), new LastName("Smith"), "Thomas");
// 別個のインスタンス同士の比較
Console.WriteLine("nameAとnameBのEqualsの比較" + nameA.Equals(nameB)); // Trueになる
// 演算子のオーバーライド機能を活用することも選択肢に入る
var compareResult2 = nameA == nameB;
Console.WriteLine(compareResult2);


// 氏名を表現するFullNameクラス
class FullName : IEquatable<FullName>
{
    private readonly FirstName firstName;
    private readonly LastName lastName;
    public string MiddleName { get; }
    public LastName LastName => lastName;

    public FullName(FirstName firstName, LastName lastName, string middleName)
    {
        this.firstName = firstName;
        this.lastName = lastName;
        MiddleName = middleName;
    }

    public bool Equals(FullName other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return this.firstName.Equals(other.firstName) && lastName.Equals(other.lastName) &&
               string.Equals(MiddleName, other.MiddleName);
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

class FirstName
{
    private readonly string value;

    public FirstName(string value)
    {
        if (string.IsNullOrEmpty(value)) throw new ArgumentException("1文字以上である必要があります。", nameof(value));
        this.value = value;
    }

    public override string ToString() => value;

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((FirstName)obj);
    }

    protected bool Equals(FirstName other)
    {
        return string.Equals(value, other.value);
    }

    public override int GetHashCode()
    {
        return value != null ? value.GetHashCode() : 0;
    }
}

class LastName
{
    private readonly string value;

    public LastName(string value)
    {
        if (string.IsNullOrEmpty(value)) throw new ArgumentException("1文字以上である必要があります", nameof(value));
        this.value = value;
    }

    public override string ToString() => value;

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((LastName)obj);
    }

    protected bool Equals(LastName other)
    {
        return string.Equals(value, other.value);
    }

    public override int GetHashCode()
    {
        return value != null ? value.GetHashCode() : 0;
    }
}