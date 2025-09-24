public class UserName
{
    private readonly string value;

    public UserName(string value)
    {
        if (value == null) throw new ArgumentNullException(nameof(value));
        if (value.Length < 3) throw new ArgumentOutOfRangeException("ユーザ名は3文字以上です", nameof(value));
        this.value = value;
    }

    public override string ToString()
    {
        return value.ToString();
    }
}