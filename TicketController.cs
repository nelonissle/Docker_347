using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using YourNamespace.Models;

public class TicketController : Controller
{
    private readonly IConfiguration _configuration;

    public TicketController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [HttpGet]
    public IActionResult CreateTicket()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CreateTicket(Ticket ticket)
    {
        var connectionString = _configuration.GetConnectionString("DefaultConnection");

        using (var connection = new SqlConnection(connectionString))
        {
            var command = new SqlCommand("INSERT INTO tickets (email, name, phone, topic, captcha) VALUES (@Email, @Name, @Phone, @Topic, @Captcha)", connection);
            command.Parameters.AddWithValue("@Email", ticket.Email);
            command.Parameters.AddWithValue("@Name", ticket.Name);
            command.Parameters.AddWithValue("@Phone", ticket.Phone);
            command.Parameters.AddWithValue("@Topic", ticket.Topic);
            command.Parameters.AddWithValue("@Captcha", ticket.Captcha);

            await connection.OpenAsync();
            await command.ExecuteNonQueryAsync();
        }

        return RedirectToAction("TicketCreated"); // Leitet weiter zu einer Bestätigungsseite
    }

    public IActionResult TicketCreated()
    {
        return View();
    }
}
