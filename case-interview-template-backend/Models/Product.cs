namespace case_interview_template_backend.Models;

public class Product
{
    public int id { get; set; }
    public string name { get; set; }
    public string description { get; set; }
    public double price { get; set; }
    
    public int cathegoryId { get; set; }
    public Cathegory cathegory { get; set; }
}
