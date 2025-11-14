namespace SnsApplication.Circles
{
    class CanNotRegisterCirclesException : Exception
    {
        public CanNotRegisterCirclesException(Circle circle, string message) : base(message)
        {
            Id = circle?.Id.Value;
        }

        public string Id;
    }
}
