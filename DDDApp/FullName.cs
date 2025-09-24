using System.Text.RegularExpressions;

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
