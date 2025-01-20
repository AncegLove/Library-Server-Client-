using System;
using System.Text;
using System.Text.Json;
internal class Program
{
    private static async Task Main(string[] args)
    {
        HttpClient client = new HttpClient();
        client.BaseAddress = new Uri("http://localhost:5227");
        Console.WriteLine("Press enter to start");
        Console.ReadLine();
        while (true)
        {
            ShowMenu();
            switch (GetIntegerInput())
            {
                case 1:
                    await ShowAll(client);
                    break;
                case 2:
                    Console.WriteLine("Etner Book id");
                    await GetByID(client, GetIntegerInput());
                    break;
                case 3:
                    var bookToAdd = GetBookInfo();
                    await AddNewBook(client, bookToAdd);
                    break;
                case 4:
                    Console.WriteLine("Enter id you want to update");
                    var idToUpdate = GetIntegerInput();
                    var bookToUpdate = GetBookInfo();
                    await UpdateBook(client, bookToUpdate, idToUpdate);
                    break;
                case 5:
                    Console.WriteLine("Enter id of book you want to delete");                    
                    await Delete(client, GetIntegerInput());
                    break;
                case 6:
                    Console.WriteLine("Enter id of book you want to take");
                    await Take(client, GetIntegerInput(), new { isAviable = true });
                    break;
                case 7:
                    Console.WriteLine("Enter id of book ypu want to return");
                    await Return(client, 2, GetIntegerInput());
                    break;
                case 8:
                    return;
                default:
                    Console.WriteLine("Choose one of variants");
                    break;
            }
            Console.ReadLine();
            Console.Clear();
        }
    }
    private static object GetBookInfo()
    {       
        Console.WriteLine("Enter book title: ");
        string title = Console.ReadLine();
        Console.WriteLine("Enter author name: ");
        string author = Console.ReadLine();
        Console.WriteLine("Enter the year: ");
        int year = GetIntegerInput();
        Console.WriteLine("Enter count of genres");
        int genrescount = GetIntegerInput();
        string[] genres = new string[genrescount];
        Console.WriteLine("Enter the genres");
        for (int i = 0; i < genrescount; i++)
            genres[i] = (Console.ReadLine());
        var newPerson = new
        {
            Title = title,
            Author = author,
            Year = year,
            Genres = genres
        };
        return newPerson;
    }
    private static int GetIntegerInput()
    {
        int input;
        while(!int.TryParse(Console.ReadLine(), out input))        
            Console.WriteLine("Enter correct sybmol");
       return input;
    }
    private static void ShowMenu()
    {
        Console.WriteLine("Library\n1.Get all books\n2.Get one book\n3.Add one book\n4.Update book\n5.Delete book\n6.Take book\n7.Return book\n8.Exit");
    }
    private static async Task Return(HttpClient client, int id, object name)
    {
        var content = new StringContent(JsonSerializer.Serialize(name));
        var response = await client.PutAsync($"api/books/{id}/return", content);
        Console.WriteLine($"RETURN BOOK WITH ID: {id}, {await response.Content.ReadAsStringAsync()}");
    }
    private static async Task Take(HttpClient client, int id, object name)
    {
        var content = new StringContent(JsonSerializer.Serialize(name));
        var response = await client.PutAsync($"api/books/{id}/checkout", content);
        Console.WriteLine($"TAKE BOOK WITH ID: {id}, {await response.Content.ReadAsStringAsync()}");       
    }
    private static async Task Delete(HttpClient client, int id)
    {
        var response = await client.DeleteAsync($"api/books/{id}");
        Console.WriteLine($"DELETE WITH {id}: {await response.Content.ReadAsStringAsync()}");
    }
    private static async Task UpdateBook(HttpClient client, object name, int id)
    {
        var content = new StringContent(JsonSerializer.Serialize(name), Encoding.UTF8, "application/json");
        var response = await client.PutAsync($"api/books/{id}", content);
        Console.WriteLine($"UPDATE BOOK WITH {id}: {await response.Content.ReadAsStringAsync()}");
    }
    private static async Task AddNewBook(HttpClient client, object name)
    {
        var adNewBookContent = new StringContent(JsonSerializer.Serialize(name), Encoding.UTF8, "application/json");
        var adNewBookresponse = await client.PostAsync($"api/books", adNewBookContent);
        Console.WriteLine($"ADD NEW BOOK: {await adNewBookresponse.Content.ReadAsStringAsync()}");
    }
    private static async Task GetByID(HttpClient client, int id)
    {
        var getByIdResponse = await client.GetAsync($"api/books/{id}");
        Console.WriteLine($"GET BY ID {id} : {await getByIdResponse.Content.ReadAsStringAsync()}");
    }
    private static async Task ShowAll(HttpClient client)
    {
        var getAllResponse = await client.GetAsync("api/books");
        Console.WriteLine($"Get All: {await getAllResponse.Content.ReadAsStringAsync()}");
    }
}