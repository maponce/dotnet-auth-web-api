
namespace AuthWebAPI.Helpers
{
    internal class OAuthProviderController
    {
        public static OAuthProvider Microsoft
        {
            get
            {
                return new OAuthProvider(OAuthProviderType.Microsoft);
            }
        }

        public static OAuthProvider Twitter
        {
            get
            {
                return new OAuthProvider(OAuthProviderType.Twitter);
            }
        }

        public static OAuthProvider Facebook
        {
            get
            {
                return new OAuthProvider(OAuthProviderType.Facebook);
            }
        }

        public static OAuthProvider Google
        {
            get
            {
                return new OAuthProvider(OAuthProviderType.Google);
            }
        }
    }
}