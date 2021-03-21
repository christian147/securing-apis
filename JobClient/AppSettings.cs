namespace JobClient
{
    public  class AppSettings
    {
        public const string Authority = "http://localhost:5001";
        public const string ClientId = "client.clientcredentials.selfcontained";
        public const string ClientSecret = "secret";
        public const string GrantType = "client_credentials";
        public const string Scope = "migration";
    }
}
