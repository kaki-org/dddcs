public class MailAddress
{
    public MailAddress(string value)
    {
        if (value == null) throw new ArgumentNullException(nameof(value));
        if (value.Length < 3) throw new ArgumentOutOfRangeException("メールアドレスは3文字以上です", nameof(value));
        if (value.Length > 20) throw new ArgumentOutOfRangeException("メールアドレスは20文字以下です", nameof(value));

        Value = value;
    }

    public string Value { get; }

}