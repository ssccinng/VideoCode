// See https://aka.ms/new-console-template for more information
using System.Buffers;
using System.IO.Pipelines;
using System.Net;
using System.Net.Sockets;
using System.Text;

Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

socket.Bind(new IPEndPoint(IPAddress.Loopback, 35353));

socket.Listen(10);

while (true)
{
    var client = socket.Accept();

    ProcessClient(client);


}

async Task ProcessClient(Socket socket)
{
    var stream = new NetworkStream(socket);
    PipeReader reader = PipeReader.Create(stream);

    while (true)
    {
        ReadResult result = await reader.ReadAsync();

        ReadOnlySequence<byte> buffer = result.Buffer;

        while (TryReadline(ref buffer, out var line))
        {
            Console.WriteLine($"收到数据: {Encoding.UTF8.GetString(line)}");

            //foreach (var seg in line)
            //{
                
            //}
        }

        reader.AdvanceTo(buffer.Start, buffer.End);

        if (result.IsCompleted)
        {
            break;
        }
    }

    await reader.CompleteAsync();
}

bool TryReadline(ref ReadOnlySequence<byte> buffer, out ReadOnlySequence<byte> line)
{
    var position = buffer.PositionOf((byte)'#');

    if (position == null)
    {
        line = default;
        return false;
    }

    line = buffer.Slice(0, position.Value);
    buffer = buffer.Slice(buffer.GetPosition(1, position.Value));

    return true;
}