using Liquidity2.Extensions.WindowPostions.Client;
using System;

namespace Liquidity2.Extensions.WindowPostions
{
    internal class WindowPostionMapper : IWindowPostionMapper
    {
        public WindowPostion Map(WindowPostionPersistentObject persistentObject)
        {
            return new WindowPostion
            {
                Height = persistentObject.Height,
                Width = persistentObject.Width,
                Top = persistentObject.Top,
                Left = persistentObject.Left,
                Id = Guid.Parse(persistentObject.Id)
            };
        }

        public WindowPostionPersistentObject Map(WindowPostion postion)
        {
            return new WindowPostionPersistentObject
            {
                Height = postion.Height,
                Width = postion.Width,
                Left = postion.Left,
                Top = postion.Top,
                Id = postion.Id.ToString()
            };
        }
    }
}
