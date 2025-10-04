
using System.Net.Sockets;
using System.Net;
using System.Text.Json;

using Microsoft.EntityFrameworkCore;
using TCPServerrr;

var ip = IPAddress.Parse("127.0.0.1");
var port = 27001;

using DBContext context = new DBContext();


var listener = new TcpListener(ip,port);
listener.Start();

List<Car> GetMethod()
{
    return context.Cars.ToList();
}

string PostMethod(Car car)
{
    context.Cars.Add(car);
    context.SaveChanges();
    return "Car added";
}

string PutMethod(Car car)
{
    var carToUpdate = context.Cars.Find(car.Id);
    if (carToUpdate == null)
    {
        return "Car not found";
    }
    carToUpdate.Model = car.Model;
    carToUpdate.Year = car.Year;
    context.SaveChanges();
    return "Car updated";
}

string DeleteMethod(int id)
{
    var carToDelete = context.Cars.Find(id);
    if (carToDelete == null)
    {
        return "Car not found";
    }
    context.Cars.Remove(carToDelete);
    context.SaveChanges();
    return "Car deleted";
}

while (true)
{
    var client = listener.AcceptTcpClient();
    var stream = client.GetStream();
    var bw = new BinaryWriter(stream);
    var br = new BinaryReader(stream);

    while (true)
    {
        var input = br.ReadString();
        Console.WriteLine(input);
        var command = JsonSerializer.Deserialize<Command>(input);
        switch (command.Text)
        {
            case Command.Get:
                var cars = GetMethod();
                var json = JsonSerializer.Serialize(cars);
                bw.Write(json);
                break;
            case Command.Post:
                var postResponce = PostMethod(command.Carss!);
                bw.Write(postResponce);
                break;
            case Command.Put:
                var putResponce = PutMethod(command.Carss!);
                bw.Write(putResponce);
                break;
            case Command.Delete:
                var deleteResponce = DeleteMethod(command.Carss!.Id);
                bw.Write(deleteResponce);
                break;

        }
    }
}