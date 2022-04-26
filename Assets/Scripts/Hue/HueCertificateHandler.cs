using System.IO;
using System.Security.Cryptography.X509Certificates;
using UnityEngine.Networking;

//--------------------------------------------------------------------------------

namespace Theater.Hue {

    public class HueCertificateHandler : CertificateHandler {

        private readonly X509Certificate2 cert;

        public HueCertificateHandler(string certificatePath) {
            this.cert = new X509Certificate2(File.ReadAllBytes(certificatePath));
        }

        protected override bool ValidateCertificate(byte[] certificateData) {
            //return this.cert.GetRawCertData().SequenceEqual(certificateData);
            return true; // TODO !
        }
    }
}