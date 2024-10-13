using System;
using System.Net.Sockets;
using System.Text;

namespace CurrencyExchangeClient
{
    class Program
    {
        static void Main()
        {
            while (true)
            {
                Console.WriteLine("Введите валютные пары (только USD=>EUR или EUR=>USD) или 'exit' для выхода:");
                string? input = Console.ReadLine();
                if (input.ToLower() == "exit")
                {
                    break;
                }

                string response = RequestExchangeRate(input);
                Console.WriteLine(response);
            }
        }

        private static string RequestExchangeRate(string request)
        {
            try
            {
                using (TcpClient client = new TcpClient("127.0.0.1", 5500))
                using (NetworkStream stream = client.GetStream())
                {
                    byte[] data = Encoding.UTF8.GetBytes(request);
                    stream.Write(data, 0, data.Length);

                    byte[] buffer = new byte[1024];
                    int bytesRead = stream.Read(buffer, 0, buffer.Length);
                    string response = Encoding.UTF8.GetString(buffer, 0, bytesRead);

                    return response;
                }
            }
            catch (Exception ex)
            {
                return $"Ошибка подключения к серверу: {ex.Message}";
            }
        }
    }
}
