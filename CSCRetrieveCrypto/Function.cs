using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Amazon.Lambda.Core;
using Amazon.Lambda.APIGatewayEvents;

using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using Amazon.DynamoDBv2.DocumentModel;
using Newtonsoft.Json;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace CSCRetrieveCrypto
{
    [Serializable]
    public class Coin
    {
        public static string id { get; set; }
        public string rank { get; set; }
        public string symbol { get; set; }
        public string supply { get; set; }
        public double priceUsd { get; set; }

        public Coin(string i, string r, string sy, string su, double pri)
        {
            id = i;
            rank = r;
            symbol = sy;
            supply = su;
            priceUsd = pri;
        }

        public Coin()
        {
        }
    }
    public class Function
    {
        private static AmazonDynamoDBClient client = new AmazonDynamoDBClient();
        private static string tableName = "FinalAssignment";

        public async Task<string> FunctionHandler(APIGatewayProxyRequest input, ILambdaContext context)
        {
            string coinSymbol = "";
            string testVariable = null;

            Table table = Table.LoadTable(client, tableName);

            Dictionary<string, string> queryStrings = (Dictionary<string, string>)input.QueryStringParameters;

            if (queryStrings != null)
            {
                queryStrings.TryGetValue("coinName", out coinSymbol);
            }
            else
            {
                if(testVariable != null)
                {
                    coinSymbol = testVariable;
                }    
                coinSymbol = "bitcoin";
            }

            GetItemResponse response = await client.GetItemAsync(tableName, new Dictionary<string, AttributeValue>
                {
                    {"id", new AttributeValue{S=coinSymbol} }
                });

            Document doc = Document.FromAttributeMap(response.Item);

            Coin coin = JsonConvert.DeserializeObject<Coin>(doc.ToJson());

            return JsonConvert.SerializeObject(coin);
        }
    }
}
