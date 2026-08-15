using System;
using System.Collections.Generic;

namespace Networking.Mirror.Integration
{
    public class MirrorServerHandlersProxy : IMirrorServerHandlersProxy
    {
        private readonly IReadOnlyList<IMirrorServerHandler> handlers;

        public MirrorServerHandlersProxy(List<IMirrorServerHandler> handlers)
        {
            this.handlers = handlers ?? throw new ArgumentNullException(nameof(handlers));
        }

        public void RegisterHandlers()
        {
            foreach (IMirrorServerHandler handler in handlers)
            {
                handler.RegisterHandler();
            }
        }

        public void UnregisterHandlers()
        {
            foreach (IMirrorServerHandler handler in handlers)
            {
                handler.UnregisterHandler();
            }
        }
    }
}
