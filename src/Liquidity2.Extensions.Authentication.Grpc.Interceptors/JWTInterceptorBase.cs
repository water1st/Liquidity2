using Grpc.Core;
using Grpc.Core.Interceptors;
using Liquidity2.Extensions.Blocker;
using System.Threading.Tasks;

namespace Liquidity2.Extensions.Authentication.Grpc.Interceptors
{
    public abstract class JWTInterceptorBase : Interceptor
    {
        protected abstract IBlocker Blocker { get; }

        public override AsyncUnaryCall<TResponse> AsyncUnaryCall<TRequest, TResponse>(TRequest request, ClientInterceptorContext<TRequest, TResponse> context, AsyncUnaryCallContinuation<TRequest, TResponse> continuation)
        {
            var token = Blocker.Block(GetAccessToken());

            var headers = new Metadata();
            headers.Add("Authorization", token);

            var newContext = new ClientInterceptorContext<TRequest, TResponse>(context.Method, context.Host, context.Options.WithHeaders(headers));

            return base.AsyncUnaryCall(request, newContext, continuation);
        }

        public override AsyncServerStreamingCall<TResponse> AsyncServerStreamingCall<TRequest, TResponse>(TRequest request, ClientInterceptorContext<TRequest, TResponse> context, AsyncServerStreamingCallContinuation<TRequest, TResponse> continuation)
        {
            var token = Blocker.Block(GetAccessToken());

            var headers = new Metadata();
            headers.Add("Authorization", token);

            var newContext = new ClientInterceptorContext<TRequest, TResponse>(context.Method, context.Host, context.Options.WithHeaders(headers));

            return base.AsyncServerStreamingCall(request, newContext, continuation);
        }

        protected abstract Task<JWT> GetAccessToken();
    }
}
