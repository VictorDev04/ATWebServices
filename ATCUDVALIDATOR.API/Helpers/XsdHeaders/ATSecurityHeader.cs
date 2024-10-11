using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel.Channels;
using System.Text;
using System.Xml;

namespace ATCUDVALIDATOR.API.Helpers.XsdHeaders
{
    public class ATSecurityHeader(string systemUser, string systemPassword, X509Certificate2 certificate) : MessageHeader
    {
        public string User = systemUser;
        public string Password = systemPassword;
        public X509Certificate2 Certificate = certificate;

        public override string Name
        {
            get
            {
                return "Security";
            }
        }

        public override string Namespace
        {
            get
            {
                return "http://schemas.xmlsoap.org/ws/2002/12/secext";
            }
        }

        protected override void OnWriteHeaderContents(XmlDictionaryWriter writer, MessageVersion messageVersion)
        {
            WriteHeader(writer);
        }

        private void WriteHeader(XmlDictionaryWriter writer)
        {
            string AtPublicKey = Certificate.GetRSAPublicKey().ToXmlString(false);
            var keySession = GenerateSymmetricKey();
            var rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(AtPublicKey);  // Importar a chave pública
            // 3. Criptografar a chave de sessão usando a chave pública RSA do servidor
            byte[] encryptedSessionKey = rsa.Encrypt(keySession, false); // CRSA,KpubSA(Ks)
            // 4. Codificar o valor criptografado em Base64
            string nonce = Convert.ToBase64String(encryptedSessionKey);  // Base64(CRSA,KpubSA(Ks))
            // date in ISO 8601 compatible format
            string createdDate = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.ffZ");

            writer.WriteRaw(string.Format("<UsernameToken>" +
                "<Username>{0}</Username>" +
                "<Nonce>{1}</Nonce>" +
                "<Password>{2}</Password>" +
                "<Created>{3}</Created>" +
                "</UsernameToken>", User, nonce, EncryptWithAesEcb(keySession, Password), EncryptWithAesEcb(keySession, createdDate)));
        }

        private static byte[] GenerateSymmetricKey()
        {
            using (Aes aes = Aes.Create())
            {
                aes.KeySize = 128; // Exemplo de chave AES de 128 bits
                aes.GenerateKey();
                return aes.Key; // Retorna a chave gerada (Ks)
            }
        }

        private static string EncryptWithAesEcb(byte[] key, string data)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = key;
                aes.Mode = CipherMode.ECB;  // Modo ECB
                aes.Padding = PaddingMode.PKCS7;  // PKCS5Padding equivale a PKCS7 no .NET

                // Converter o timestamp para bytes
                byte[] dataBytes = Encoding.UTF8.GetBytes(data);

                // Criar o objeto de criptografia
                using (ICryptoTransform encryptor = aes.CreateEncryptor())
                {
                    return Convert.ToBase64String(encryptor.TransformFinalBlock(dataBytes, 0, dataBytes.Length));
                }
            }
        }
    }
}
