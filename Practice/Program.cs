

using System.Net;
using System.Net.Sockets;
using System.Text.Json;

var ip = IPAddress.Parse("127.0.0.1");
var port = 27001;

var client = new TcpClient();
client.Connect(ip, port);

var stream = client.GetStream();
var bw = new BinaryWriter(stream);
var br = new BinaryReader(stream);

Command command = null!;
string responce = null!;
string str = null!;

while (true)
{
    Console.WriteLine("Write Command name or HELP: ");
    str = Console.ReadLine()!.ToUpper();
    if (str == "HELP")
    {
        Console.WriteLine();
        Console.WriteLine("GET - to get all cars");
        Console.WriteLine("POST <Model> <Year> - to add a new car");
        Console.WriteLine("PUT <Id> <Model> <Year> - to update a car");
        Console.WriteLine("DELETE <Id> - to delete a car");
        Console.ReadLine();
        Console.Clear();
    }

    var input = str.Split(' ');
    switch (input[0])
    {
        case Command.Get:
            command = new Command { Text = input[0] };
            var json = JsonSerializer.Serialize(command);
            bw.Write(json);
            responce = br.ReadString();
            Console.WriteLine(responce);
            Console.ReadLine();
            Console.Clear();
            break;
        case Command.Post:
            command = new Command { Text = input[0], Carss = new List<Car> { new Car { Model = input[1], Year = int.Parse(input[2]) } } };
            var jsonPost = JsonSerializer.Serialize(command);
            bw.Write(jsonPost);
            responce = br.ReadString();
            Console.WriteLine(responce);
            Console.ReadLine();
            Console.Clear();
            break;
        case Command.Put:
            command = new Command { Text = input[0], Carss = new List<Car> { new Car { Id = int.Parse(input[1]), Model = input[2], Year = int.Parse(input[3]) } } };
            var jsonPut = JsonSerializer.Serialize(command);
            bw.Write(jsonPut);
            responce = br.ReadString();
            Console.WriteLine(responce);
            Console.ReadLine();
            Console.Clear();
            break;
        case Command.Delete:
            command = new Command { Text = input[0], Carss = new List<Car> { new Car { Id = int.Parse(input[1]) } } };
            var jsonDelete = JsonSerializer.Serialize(command);
            bw.Write(jsonDelete);
            responce = br.ReadString();
            Console.WriteLine(responce);
            Console.ReadLine();
            Console.Clear();
            break;
    }
}