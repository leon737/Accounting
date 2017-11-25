namespace Cash.Domain.Requests
{
    public class UpdateAccountInfoRequest
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public int Code { get; set; }

        public bool Locked { get; set; }
    }
}
