using System;
using System.Net.Http;
using System.Text;

namespace QnaMakerQuickstart
{

    class Program
    {

        const string endpointVar = "https://qnabotchallenge.azurewebsites.net";
        const string endpointKeyVar = "51612763-7b2a-4813-b561-522f7b9c149b";
        const string kbIdVar = "44386ba2-fc74-4c3d-9f56-5f1296b5a9cd";

        // Your QnA Maker resource endpoint.
        // From Publish Page: HOST
        // Example: https://YOUR-RESOURCE-NAME.azurewebsites.net/
        static readonly string endpoint = endpointVar;
        // Authorization endpoint key
        // From Publish Page
        // Note this is not the same as your QnA Maker subscription key.
        static readonly string endpointKey = endpointKeyVar;
        static readonly string kbId = kbIdVar;


        /// <summary>
        /// Static constuctor. Verifies that we found the subscription key and
        /// endpoint in the environment variables.
        /// </summary>
        static Program()
        {

            if (null == endpointKey)
            {
                throw new Exception("Please set/export the environment variable: " + endpointKeyVar);
            }
            if (null == endpoint)
            {
                throw new Exception("Please set/export the environment variable: " + endpointVar);
            }
            if (null == kbId)
            {
                throw new Exception("Please set/export the environment variable: " + kbIdVar);
            }
        }

        static void Main(string[] args)
        {

            var uri = endpoint + "/qnamaker/knowledgebases/" + kbId + "/generateAnswer";

            // JSON format for passing question to service
            string question = @"{'question': 'How do I cancel my hotel reservation?','top': 1}";

            // Create http client
            using (var client = new HttpClient())
            using (var request = new HttpRequestMessage())
            {
                // POST method
                request.Method = HttpMethod.Post;

                // Add host + service to get full URI
                request.RequestUri = new Uri(uri);

                // set question
                request.Content = new StringContent(question, Encoding.UTF8, "application/json");

                // set authorization
                request.Headers.Add("Authorization", "EndpointKey " + endpointKey);

                // Send request to Azure service, get response
                var response = client.SendAsync(request).Result;
                var jsonResponse = response.Content.ReadAsStringAsync().Result;

                // Output JSON response
                Console.WriteLine(jsonResponse);

                Console.WriteLine("Press any key to continue.");
                Console.ReadKey();
            }
        }


    }
}
