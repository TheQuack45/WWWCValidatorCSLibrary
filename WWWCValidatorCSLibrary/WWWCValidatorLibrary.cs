using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Xml;

namespace WWWCValidatorCSLibrary
{
    class WWWCValidatorLibrary
    {
        public const string UNICODE_ENCODING = "UNICODE_ENCODING";
        public const string ASCII_ENCODING = "ASCII_ENCODING";
        public const string UTF7_ENCODING = "UTF7_ENCODING";
        public const string UTF8_ENCODING = "UTF8_ENCODING";
        public const string UTF32_ENCODING = "UTF32_ENCODING";

        public const string VALIDATOR_URL = "https://validator.w3.org/nu/";
        public const string VALIDATOR_URL_GET_ROOT = "?out=";
        public const string VALIDATOR_URL_GET_TYPE = "xml";

        public const string REQUEST_METHOD = "POST";
        public const string CONTENT_TYPE = "text/html";

        public static ValidationResponse ValidateByUpload(string validationData, string encodingType = "UNICODE_ENCODING")
        {
            #region Data encoding
            byte[] encodedValidationDataByteArr;
            if (encodingType == UNICODE_ENCODING)
            {
                // Use Unicode encoding
                UnicodeEncoding encodedValidationData = new UnicodeEncoding();
                encodedValidationDataByteArr = encodedValidationData.GetBytes(validationData);
            }
            else if (encodingType == ASCII_ENCODING)
            {
                // Use ASCII (7-bit) encoding
                ASCIIEncoding encodedValidationData = new ASCIIEncoding();
                encodedValidationDataByteArr = encodedValidationData.GetBytes(validationData);
            }
            else if (encodingType == UTF7_ENCODING)
            {
                // Use UTF-7 encoding
                UTF7Encoding encodedValidationData = new UTF7Encoding();
                encodedValidationDataByteArr = encodedValidationData.GetBytes(validationData);
            }
            else if (encodingType == UTF8_ENCODING)
            {
                // Use UTF-8 encoding
                UTF8Encoding encodedValidationData = new UTF8Encoding();
                encodedValidationDataByteArr = encodedValidationData.GetBytes(validationData);
            }
            else if (encodingType == UTF32_ENCODING)
            {
                // Use UTF-32 encoding
                UTF32Encoding encodedValidationData = new UTF32Encoding();
                encodedValidationDataByteArr = encodedValidationData.GetBytes(validationData);
            }
            else
            {
                throw new InvalidEncodingException("An invalid encoding type was specified. Type: " + encodingType);
            }
            #endregion

            #region Request formation
            HttpWebRequest uploadRequest = HttpWebRequest.CreateHttp(VALIDATOR_URL + VALIDATOR_URL_GET_ROOT + VALIDATOR_URL_GET_TYPE);
            uploadRequest.Method = REQUEST_METHOD;
            uploadRequest.ContentType = CONTENT_TYPE;
            uploadRequest.ContentLength = encodedValidationDataByteArr.Length;
            Stream uploadRequestStream = uploadRequest.GetRequestStream();
            uploadRequestStream.Write(encodedValidationDataByteArr, 0, encodedValidationDataByteArr.Length);
            #endregion

            // Make request
            // Stores response object in `uploadRequestResponse`
            HttpWebResponse uploadRequestResponse = (HttpWebResponse)uploadRequest.GetResponse();

            // Get response data
            Stream uploadRequestResponseStream = uploadRequestResponse.GetResponseStream();
            StreamReader uploadRequestResponseStreamReader = new StreamReader(uploadRequestResponseStream);
            string uploadRequestResponseString = uploadRequestResponseStreamReader.ReadToEnd();

            ValidationResponse response = ParseValidationResponse(uploadRequestResponseString);
        }

        private static ValidationResponse ParseValidationResponse(string responseString)
        {
            XmlDocument validationResponseXml = new XmlDocument();
            validationResponseXml.LoadXml(responseString);
            XmlNode validationResponseXmlMessage = validationResponseXml["message"];
        }
    }
}
