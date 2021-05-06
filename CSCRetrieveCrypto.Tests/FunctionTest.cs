using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Xunit;
using Amazon.Lambda.Core;
using Amazon.Lambda.TestUtilities;
using Amazon.Lambda.APIGatewayEvents;

using CSCRetrieveCrypto;

namespace CSCRetrieveCrypto.Tests
{
    public class FunctionTest
    {
        [Fact]
        public void TestToUpperFunction()
        {

            // Invoke the lambda function and confirm the string was upper cased.
            var function = new Function();
            var context = new TestLambdaContext();
            var request = new APIGatewayProxyRequest();
            var returnValue = function.FunctionHandler(request, context);
            Coin coin = new Coin("bitcoin", "1", "BTC", "500000000", 4600000.00);

            //having trouble getting the return value casted
            
            Assert.Equal("bitcoin", Coin.id);
        }
    }
}
