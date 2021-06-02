using Plugable.io;
using Plugable.io.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using uhttpsharp;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace WebProt.Provider.Plugin.Console
{
    public class WebExtension : WebSocketBehavior, IPlugable, IProtocolPlugin
    {
        public void Initialize(string[] args, dynamic parent) 
        {
            AppDomain.CurrentDomain.AssemblyResolve += ResolveAssembly;
        }

        public void Initialize(string[] args, PluginsManager parent, dynamic server)
        {
            if (server != null)
            {
                Type type = ((object)server).GetType();
                if (type != null)
                {
                    if (type.Name == "Router")
                    {

                    }
                    else
                    {
                        AppDomain.CurrentDomain.AssemblyResolve += ResolveAssembly;
                        server.AddWebSocketService<WebExtension>("/console");
                    }
                }
            }
        }

        public Dictionary<string, Func<IHttpContext, Dictionary<string, string>, Func<Task>, Task>> GetRoutes()
        {
            var list = new Dictionary<string, Func<IHttpContext, Dictionary<string, string>, Func<Task>, Task>>();

            return list;
        }

        protected override void OnMessage(MessageEventArgs e)
        {
            if (e.IsText)
            {

            }
            else if (e.IsBinary)
            {

            }

            /*foreach (IWebSocketSession session in Sessions.Sessions)
            {
                if (session.ID == ID) continue;
                else
                {
                    if (e.IsText) session.Context.WebSocket.Send(e.Data);
                    else if (e.IsBinary)
                    {

                    }
                }
            }*/
        }

        #region getVersion
        public string getVersion()
        {
            return GetType().Assembly.GetName().Version.ToString();
        }
        #endregion

        #region getName
        public string getName()
        {
            return GetType().Assembly.GetName().Name;
        }
        #endregion

        #region ResolveAssembly
        public Assembly ResolveAssembly(object sender, ResolveEventArgs args)
        {
            var assembly = (args.Name.Contains(","))
                    ? args.Name.Substring(0, args.Name.IndexOf(','))
                    : args.Name;

            var directory = Path.Combine(Environment.CurrentDirectory, "extensions");
            var plugin = getName() + "_" + getVersion() + ".zip";

            return Path.Combine(directory, plugin).GetAssemblyFromPlugin(assembly);
        }
        #endregion
    }
}
