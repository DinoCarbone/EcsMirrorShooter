using System;
using System.Collections.Generic;

namespace Networking.Mirror.Integration
{
    public class MirrorHandlersProxy :
        IMirrorServerHandlersProxy,
        IMirrorClientsHandlersProxy
    {
        private readonly IReadOnlyList<IMirrorServerHandler> serverHandlers;
        private readonly IReadOnlyList<IMirrorClientHandler> clientHandlers;

        public MirrorHandlersProxy(
            List<IMirrorServerHandler> serverHandlers,
            List<IMirrorClientHandler> clientHandlers)
        {
            this.serverHandlers = serverHandlers ??
                throw new ArgumentNullException(nameof(serverHandlers));
            this.clientHandlers = clientHandlers ??
                throw new ArgumentNullException(nameof(clientHandlers));
        }

        public void RegisterServerHandlers()
        {
            foreach (IMirrorServerHandler handler in serverHandlers)
            {
                handler.RegisterServerHandler();
            }
        }

        public void UnregisterServerHandlers()
        {
            foreach (IMirrorServerHandler handler in serverHandlers)
            {
                handler.UnregisterServerHandler();
            }
        }

        public void RegisterClientHandlers()
        {
            foreach (IMirrorClientHandler handler in clientHandlers)
            {
                handler.RegisterClientHandler();
            }
        }

        public void UnregisterClientHandlers()
        {
            foreach (IMirrorClientHandler handler in clientHandlers)
            {
                handler.UnregisterClientHandler();
            }
        }
    }
}
