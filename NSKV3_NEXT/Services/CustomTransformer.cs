using Yarp.ReverseProxy.Forwarder;

namespace NSKV3_NEXT.Services
{
    public class CustomTransformer : HttpTransformer
    {
        public override ValueTask TransformRequestAsync(HttpContext httpContext, HttpRequestMessage proxyRequest, string destinationPrefix)
        {
            // Вы можете добавить свою логику для модификации запроса здесь
            return base.TransformRequestAsync(httpContext, proxyRequest, destinationPrefix);
        }

        public override ValueTask<bool> TransformResponseAsync(HttpContext httpContext, HttpResponseMessage proxyResponse)
        {
            if (httpContext == null) throw new ArgumentNullException(nameof(httpContext));
            if (proxyResponse == null) throw new ArgumentNullException(nameof(proxyResponse));

            // Если вы модифицируете proxyResponse здесь, верните true.
            // Например, для изменения заголовков ответа или статусного кода.
            // Если не производится никаких изменений, верните false.

            return new ValueTask<bool>(result: false);
        }

        public override ValueTask TransformResponseTrailersAsync(HttpContext httpContext, HttpResponseMessage proxyResponse)
        {
            // Модификация трейлеров ответа, если используются
            return base.TransformResponseTrailersAsync(httpContext, proxyResponse);
        }
    }
}
