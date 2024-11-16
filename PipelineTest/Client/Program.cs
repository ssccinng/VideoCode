// See https://aka.ms/new-console-template for more information
using System.Net.Sockets;

Socket socket = new Socket(SocketType.Stream, ProtocolType.Tcp);

socket.Connect("127.0.0.1", 35353);

var stream = new NetworkStream(socket);
Console.OpenStandardInput().CopyTo(stream);