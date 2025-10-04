

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

var isRunning = true;

while (isRunning)
{
    Console.Clear();
    Console.WriteLine("*****Car Manager***** ");
    Console.WriteLine("1. List all cars");
    Console.WriteLine("2. Add new car");
    Console.WriteLine("3. Update car");
    Console.WriteLine("4. Delete car by ID");
    Console.WriteLine("5. Help");
    Console.WriteLine("0. Exit");
    Console.WriteLine("********************");
    Console.Write("Choose option: ");

    string choice = Console.ReadLine()!;

    switch (choice)
    {
        case "1":
            command = new Command { Text = Command.Get };
            var json = JsonSerializer.Serialize(command);
            bw.Write(json);
            responce = br.ReadString();
            var list  = JsonSerializer.Deserialize<List<Car>>(responce);
            foreach (var car in list!)
            {
                Console.WriteLine($"ID: {car.Id}, Model: {car.Model}, Year: {car.Year}");
            }
            Console.ReadLine();
            Console.Clear();
            break;
        case "2":
            Console.Write("Enter car model: ");
            string carModel = Console.ReadLine()!;

            Console.Write("Enter car year: ");
            int carYear = int.Parse(Console.ReadLine()!);

            command = new Command { Carss = new Car { Model = carModel, Year = carYear }, Text = Command.Post };
            var jsonPost = JsonSerializer.Serialize(command);
            bw.Write(jsonPost);
            responce = br.ReadString();
            Console.WriteLine(responce);
            Console.ReadLine();
            Console.Clear();
            break;
        case "3":

            Console.Write("Enter car ID to update: ");
            int carId = int.Parse(Console.ReadLine()!);

            Console.Write("Enter car model: ");
            string carNewModel = Console.ReadLine()!;

            Console.Write("Enter car year: ");
            int carNewYear = int.Parse(Console.ReadLine()!);

            command = new Command { Carss = new Car { Id = carId, Model = carNewModel, Year = carNewYear }, Text = Command.Put };
            var jsonPut = JsonSerializer.Serialize(command);
            bw.Write(jsonPut);
            responce = br.ReadString();
            Console.WriteLine(responce);
            Console.ReadLine();
            Console.Clear();
            break;
        case "4":
            Console.Write("Enter car ID to delete: ");
            int carDeleteId = int.Parse(Console.ReadLine()!);
            command = new Command { Carss = new Car { Id = carDeleteId }, Text = Command.Delete };
            var jsonDelete = JsonSerializer.Serialize(command);
            bw.Write(jsonDelete);
            responce = br.ReadString();
            Console.WriteLine(responce);
            Console.ReadLine();
            Console.Clear();
            break;

        case "5":
            Console.WriteLine();
            Console.WriteLine("GET - to get all cars");
            Console.WriteLine("POST - to add a new car");
            Console.WriteLine("PUT - to update a car");
            Console.WriteLine("DELETE - to delete a car");
            Console.ReadLine();
            Console.Clear();
            break;
        case "0":
            isRunning = false;
            client.Close();
            Console.WriteLine("Client disconnected.");
            break;

    }

}