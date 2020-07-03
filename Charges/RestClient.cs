using System;
using System.IO;
using System.Net;

namespace Charges
{
    public enum httpVerb
    {
        GET,
        POST,
        PUT,
        DELETE,
        PATCH
    }

    class RestClient
    {
        internal byte[] formData { get; set; }

        public string endPoint { get; set; }
        public httpVerb httpMethod { get; set; }
        public HttpRequestHeader header { get; set; }
        public string osvcAuth { get; set; }
        public string osvcContext { get; set; }
        public string ebsAuthToken { get; set; }
        public string tokenUser { get; set; }
        public string tokenSource { get; set; }
        public string jsonBody { get; set; }
        public string ebsBearerToken { get; set; }

        public RestClient()
        {
            endPoint = string.Empty;
            httpMethod = httpVerb.GET;
        }

        /// <summary>
        /// Builds up the actual web request to send via HTTPWebRequest
        /// </summary>
        /// <returns>Request Response string</returns>
        public string makeRequest()
        {
            string strResponseValue = string.Empty;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(endPoint);

            request.Method = httpMethod.ToString();

            // OSVC Deets
            if (osvcAuth != null)
            {
                request.Headers.Add("Authorization", "Session " + osvcAuth);
            }
            if (osvcContext != null)
            {
                request.Headers.Add("OSvC-CREST-Application-Context", osvcContext);
            }

            // EBS Bearer Token 
            if (ebsBearerToken != null)
            {
                request.Headers.Add("Authorization", "Bearer " + ebsBearerToken);
            }

            // EBS Deets
            if (ebsAuthToken != null)
            {
                request.Headers.Add("Authorization", "Basic " + ebsAuthToken);
            }
            if (tokenUser != null)
            {
                request.Headers.Add("x_user_name", tokenUser);
            }
            if (tokenSource != null)
            {
                request.Headers.Add("x_source_system", tokenSource);
            }

            // Process JSON payload if exists
            if (jsonBody != null)
            {

                using (var streamWriter = new System.IO.StreamWriter(request.GetRequestStream()))
                {
                    streamWriter.Write(jsonBody);
                    streamWriter.Flush();
                }
            }

            // Process formdata if it exists
            if (formData != null)
            {
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = formData.Length;
                using (var stream = request.GetRequestStream())
                {
                    stream.Write(formData, 0, formData.Length);
                    stream.Close();
                }
            }

            try
            {
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        if (response.StatusCode != HttpStatusCode.Created)
                        {
                            throw new ApplicationException("Bad response from server: " + response.StatusCode.ToString());
                        }
                    }

                    // Process the response stream...JSON expected
                    using (Stream responseStream = response.GetResponseStream())
                    {
                        if (responseStream != null)
                        {
                            using (StreamReader reader = new StreamReader(responseStream))
                            {
                                strResponseValue = reader.ReadToEnd();
                            }
                        }
                    }
                }
            }
            catch (WebException e)
            {
                throw new ApplicationException("Bad response from server: " + e);
            }



            return strResponseValue;
        }
    }
}
