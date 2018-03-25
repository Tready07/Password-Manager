using System;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Networking
{
    public static class SocketExtensions
    {
        public static Task<int> ReceiveAsync(this Socket socket, byte[] buffer, int offset, int size, SocketFlags flags)
        {
            var tcs = new TaskCompletionSource<int>(socket);
            socket.BeginReceive(buffer, offset, size, flags, iar =>
            {
                var t = (TaskCompletionSource<int>)iar.AsyncState;
                var s = (Socket)t.Task.AsyncState;
                try
                {
                    t.TrySetResult(s.EndReceive(iar));
                }
                catch (Exception exc)
                {
                    t.TrySetException(exc);
                }
            }, tcs);
            return tcs.Task;
        }

        public static Task<int> ReceiveAsync(this Socket socket, byte[] buffer)
        {
            return socket.ReceiveAsync(buffer, 0, buffer.Length, SocketFlags.None);
        }
    }
}
