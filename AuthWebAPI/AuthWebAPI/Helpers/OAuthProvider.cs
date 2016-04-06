using System.Configuration;

namespace AuthWebAPI.Helpers
{
    internal class OAuthProvider
    {
        public string Key { get; private set; }
        public string Secret { get; private set; }
        public bool IsEnabled { get; private set; }
        public OAuthProviderType Type { get; private set; }

        public OAuthProvider(OAuthProviderType type)
        {
            this.Type = type;
            this.IsEnabled = IsProviderEnabled();

            if (this.IsEnabled)
            {
                this.Key = GetProviderKey();
                this.Secret = GetProviderSecret();

                if (string.IsNullOrEmpty(this.Key) || string.IsNullOrEmpty(this.Secret))
                    this.IsEnabled = false;
            }
        }

        private bool IsProviderEnabled()
        {
            bool parsed = false;
            bool isEnabled = false;

            switch (this.Type)
            {
                case OAuthProviderType.Microsoft:
                    parsed = bool.TryParse(ConfigurationManager.AppSettings["Microsoft.Auth.Enabled"], out isEnabled);
                    break;
                case OAuthProviderType.Twitter:
                    parsed = bool.TryParse(ConfigurationManager.AppSettings["Twitter.Auth.Enabled"], out isEnabled);
                    break;
                case OAuthProviderType.Facebook:
                    parsed = bool.TryParse(ConfigurationManager.AppSettings["Facebook.Auth.Enabled"], out isEnabled);
                    break;
                case OAuthProviderType.Google:
                    parsed = bool.TryParse(ConfigurationManager.AppSettings["Google.Auth.Enabled"], out isEnabled);
                    break;

            }

            if (parsed)
                return isEnabled;
            else
                return false;
        }

        private string GetProviderKey()
        {
            switch (this.Type)
            {
                case OAuthProviderType.Microsoft:
                    return ConfigurationManager.AppSettings["Microsoft.Auth.ClientId"];
                case OAuthProviderType.Twitter:
                    return ConfigurationManager.AppSettings["Twitter.Auth.ConsumerKey"];
                case OAuthProviderType.Facebook:
                    return ConfigurationManager.AppSettings["Facebook.Auth.AppId"];
                case OAuthProviderType.Google:
                    return ConfigurationManager.AppSettings["Google.Auth.ClientId"];
            }

            return string.Empty;
        }

        private string GetProviderSecret()
        {
            switch (this.Type)
            {
                case OAuthProviderType.Microsoft:
                    return ConfigurationManager.AppSettings["Microsoft.Auth.ClientSecret"];
                case OAuthProviderType.Twitter:
                    return ConfigurationManager.AppSettings["Twitter.Auth.ConsumerSecret"];
                case OAuthProviderType.Facebook:
                    return ConfigurationManager.AppSettings["Facebook.Auth.AppSecret"];
                case OAuthProviderType.Google:
                    return ConfigurationManager.AppSettings["Google.Auth.ClientSecret"];
            }

            return string.Empty;
        }
    }

    internal enum OAuthProviderType
    {
        Microsoft,
        Twitter,
        Facebook,
        Google
    }
}