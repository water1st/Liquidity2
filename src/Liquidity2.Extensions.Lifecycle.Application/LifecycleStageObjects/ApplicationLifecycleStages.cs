﻿namespace Liquidity2.Extensions.Lifecycle.Application
{
    public class ApplicationLifecycleStages
    {
        /// <summary>
        /// 第一阶段
        /// 总线、线程池等核心服务
        /// </summary>
        public const int APPLICATION = 0x100000;

        /// <summary>
        /// 第二阶段
        /// UI核心（初始化信息呈现服务、系统错误呈现服务等）
        /// </summary>
        public const int UI_CORE = 0x200000;

        /// <summary>
        /// 第三阶段
        /// 身份认证
        /// </summary>
        public const int AUTHORIZATION = 0x300000;

        /// <summary>
        /// 第四阶段
        /// 本地持久化（系统配置、业务配置）
        /// </summary>
        public const int LOCAL_STORAGE = 0x400000;

        /// <summary>
        /// 第五阶段
        /// 网络持久化（系统配置、业务配置、长连接创建）
        /// </summary>
        public const int NETWORK = 0x500000;

        /// <summary>
        /// 第六阶段
        /// 业务服务
        /// </summary>
        public const int SERVICE = 0x600000;

        /// <summary>
        /// 第七阶段
        /// 业务呈现服务
        /// </summary>
        public const int UI_SERVICE = 0x700000;
    }
}
