using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

Socket serverSocket = new Socket(AddressFamily.InterNetwork,
    SocketType.Dgram,
    ProtocolType.Udp);

IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, 5005);
serverSocket.Bind(endPoint);

Console.WriteLine("UDP server is started on port 5005...");

byte[] buffer = new byte[1024];

while (true)
{
    EndPoint remoteEndPoint = new IPEndPoint(IPAddress.Any, 0);
    
    int receivedBytes = serverSocket.ReceiveFrom(buffer, ref remoteEndPoint);

    string message = Encoding.UTF8.GetString(buffer, 0, receivedBytes);

    Console.WriteLine($"Received from {remoteEndPoint}: {message}");
    
    string response = "Server receive: " + message;
    byte[] responseBytes = Encoding.UTF8.GetBytes(response);

    serverSocket.SendTo(responseBytes, remoteEndPoint);
}