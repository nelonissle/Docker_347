public class Ticket
{
    public int Id { get; set; }
    public string Email { get; set; }
    public string Name { get; set; }
    public string Phone { get; set; }
    public string Topic { get; set; }
    public string Captcha { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}
