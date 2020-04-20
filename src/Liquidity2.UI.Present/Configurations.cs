using System;
using System.Collections.Generic;
using System.Text;

namespace Liquidity2.UI
{
    public sealed class Configurations
    {
        #region Debug环境配置
#if DEBUG

        #region GRPC配置
        /// <summary>
        /// 交易服务GRPC地址
        /// 直连地址：47.56.95.192:54000
        /// 转发地址：139.196.169.16:60000
        /// </summary>
        public static readonly Uri TRADE_GRPC_SERVICE_ADDRESS = new Uri("http://139.196.169.16:60000");
        /// <summary>
        /// 行情服务GRPC地址
        /// 直连地址：47.56.95.192:50000
        /// 转发地址：139.196.169.16:50000
        /// </summary>
        public static readonly Uri MARKET_GRPC_SERVICE_ADDRESS = new Uri("http://139.196.169.16:50000");
        /// <summary>
        /// 资金管理
        /// 直连地址：47.56.95.192:53000
        /// 转发地址：139.196.169.16:65000
        /// </summary>
        public static readonly Uri FUNDS_GRPC_SERVICE_ADDRESS = new Uri("http://139.196.169.16:65000");
        #endregion


        #region 身份认值服务配置
        public const string CLIENT_SECRET = "abb21609-f9a4-2b28-6622-ec410abe648b";
        public const string CLIENT_SCOPE = "profile openid trades_api offline_access";
        public const string CLIENT_CLIENT_ID = "trades_grpc_client";
        public static readonly Uri ACCESSTOKEN_ENDPOINT = new Uri("http://47.56.95.192:20000/connect/token");
        #endregion

#endif
        #endregion

        #region Release环境配置

#if RELEASE
        #region GRPC配置
        /// <summary>
        /// 交易服务GRPC地址
        /// 直连地址：47.56.95.192:54000
        /// 转发地址：139.196.169.16:60000
        /// </summary>
        public static readonly Uri TRADE_GRPC_SERVICE_ADDRESS = new Uri("http://139.196.169.16:60000");

        /// <summary>
        /// 行情服务GRPC地址
        /// 直连地址：47.56.95.192:50000
        /// 转发地址：139.196.169.16:50000
        /// </summary>
        public static readonly Uri MARKET_GRPC_SERVICE_ADDRESS = new Uri("http://139.196.169.16:50000");
        /// <summary>
        /// 资金管理
        /// 直连地址：47.56.95.192:53000
        /// 转发地址：139.196.169.16:65000
        /// </summary>
        public static readonly Uri FUNDS_GRPC_SERVICE_ADDRESS = new Uri("http://139.196.169.16:65000");
        #endregion


        #region 身份认值服务配置
        public const string CLIENT_SECRET = "abb21609-f9a4-2b28-6622-ec410abe648b";
        public const string CLIENT_SCOPE = "profile openid trades_api offline_access";
        public const string CLIENT_CLIENT_ID = "trades_grpc_client";
        public static readonly Uri ACCESSTOKEN_ENDPOINT = new Uri("http://47.56.95.192:20000/connect/token");
        #endregion



#endif

        #endregion
    }
}
