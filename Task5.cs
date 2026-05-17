using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
socket.ReceiveTimeout = 2000;

IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Loopback, 5005);

while (true)
{
    Console.Write("Enter message: ");
    string message = Console.ReadLine();

    byte[] data = Encoding.UTF8.GetBytes(message);
    socket.SendTo(data, serverEndPoint);

    try
    {
        byte[] buffer = new byte[1024];
        EndPoint remoteEndPoint = new IPEndPoint(IPAddress.Any, 0);

        int bytes = socket.ReceiveFrom(buffer, ref remoteEndPoint);
        string response = Encoding.UTF8.GetString(buffer, 0, bytes);

        Console.WriteLine("Server response: " + response);
    }
    catch (SocketException)
    {
        Console.WriteLine("Timeout: no response from server (2s)");
    }
}
